using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models.ViewModels;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthShark.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Dietician+","+SD.Role_Trainer)]
    public class EmployeeAssignmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeAssignmentController(IUnitOfWork unitOfWork, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
          
            return View();
        }

        [HttpGet]




        public IActionResult GetAll()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var User = _db.ApplicationUsers.Find(userId);
                  
            
                List<UserAssignmentVM> allObj = new List<UserAssignmentVM>();
                if (User.Role == SD.Role_Trainer)
                {
                        allObj = _db.UserAssignmentVMs.Where(x => x.TrainerId == userId).ToList();
                    foreach (var obj in allObj)
                    {
                        obj.Dietician = _db.ApplicationUsers.Find(obj.DieticianId);
                        obj.User = _db.ApplicationUsers.Find(obj.UserId);
                        obj.Diet = (obj.DietId == null ? null : _unitOfWork.Diet.Get(obj.DietId.GetValueOrDefault()));
                        obj.WorkOut = (obj.WorkoutId == null ? null : _unitOfWork.WorkOut.Get(obj.WorkoutId.GetValueOrDefault()));
                        obj.BodyType = (obj.BodyTypeId == null ? null : _unitOfWork.BodyType.Get(obj.BodyTypeId.GetValueOrDefault()));
                    }

                 }
                else if (User.Role == SD.Role_Dietician)
                {
                    allObj = _db.UserAssignmentVMs.Where(x => x.DieticianId == userId).ToList();
                    foreach (var obj in allObj)
                    {
                        obj.Dietician = _db.ApplicationUsers.Find(obj.DieticianId);
                        obj.User = _db.ApplicationUsers.Find(obj.UserId);
                        obj.Diet = (obj.DietId == null ? null : _unitOfWork.Diet.Get(obj.DietId.GetValueOrDefault()));
                        obj.WorkOut = (obj.WorkoutId == null ? null : _unitOfWork.WorkOut.Get(obj.WorkoutId.GetValueOrDefault()));
                        obj.BodyType = (obj.BodyTypeId == null ? null : _unitOfWork.BodyType.Get(obj.BodyTypeId.GetValueOrDefault()));
                    }
                 }

                return Json(new { data = allObj });

        }
        [HttpGet]

        public IActionResult Update(int? Id)
        {
            EmployeeAssignment vm = new EmployeeAssignment();
            if(Id is null)
            {
                return NotFound();
            }


            vm.UserAssignmentVM = _db.UserAssignmentVMs.Find(Id);
            vm.UserVM = _db.UserVMs.Find(vm.UserAssignmentVM.UserId);

            if(vm is null)
            {
                return NotFound();
            }

            return View(vm);


        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(UserAssignmentVM userAssignment)
        {
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();
            if (ModelState.IsValid)
            {
                if (userAssignment.Id != 0)
                {
                    _unitOfWork.UserAssignmentVM.Update(userAssignment);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                  
                    employeeAssignment.UserAssignmentVM = _db.UserAssignmentVMs.Find(userAssignment.Id);
                    employeeAssignment.UserVM = _db.UserVMs.Find(employeeAssignment.UserAssignmentVM.UserId);
                    
                }

            }
            return View(employeeAssignment);
        }

       
    }
}
