using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using HealthShark.Models.Models.ViewModels;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthShark.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin+","+SD.Role_Customer)]
    public class UserAssignmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserAssignmentController(IUnitOfWork unitOfWork, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor )
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
    
        }


        [HttpGet]


        public IActionResult Upsert(int? id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var selectedPlan = _unitOfWork.UserPlan.Get(id.GetValueOrDefault());
            UserAssignmentVM userAssignmentVM = new UserAssignmentVM();

            if ((selectedPlan!=null) && (userId !=null))
            {
                

                userAssignmentVM.UserId = userId;
                userAssignmentVM.UserPlanId = selectedPlan.Id;
                userAssignmentVM.UserPlan = selectedPlan;
                return View(userAssignmentVM);
            }
            else
            {
                return NotFound();
            }
     
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Upsert(UserAssignmentVM userAssignmentVM)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                userAssignmentVM.Paid = true;

                userAssignmentVM.UserId = userId;

                List<ApplicationUser> trainers = _db.ApplicationUsers.Where(x => x.Role == SD.Role_Trainer).ToList();
                int limit = trainers.Count();
                Random random = new Random();
                int randomTrainer = random.Next(0, limit);
                var trainer = trainers[randomTrainer];


                userAssignmentVM.TrainerId = trainer.Id;





                List<ApplicationUser> dieticians = _db.ApplicationUsers.Where(x => x.Role == SD.Role_Dietician).ToList();
                int limitDiet = dieticians.Count();
                int randomDietician = random.Next(0, limitDiet);
                var dietician = dieticians[randomDietician];
                userAssignmentVM.DieticianId = dietician.Id;


                _unitOfWork.UserAssignmentVM.Add(userAssignmentVM);
                _unitOfWork.Save();

                return RedirectToAction("Details", "Details", new { area = "Customer" });

            }
            return View(userAssignmentVM);
        }




    }
}
