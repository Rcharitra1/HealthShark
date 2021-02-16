using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using HealthShark.Models.Models.ViewModels;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
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

        private static string userplanId;


        public UserAssignmentController(IUnitOfWork unitOfWork, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor )
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
    
        }

        [HttpGet]

        public IActionResult Upsert(int? id)
        {
            var userAssignmentDummyClass = new UserAssignmentUpsertVM();
            userAssignmentDummyClass.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var selectedPlan = _unitOfWork.UserPlan.Get(id.GetValueOrDefault());
            userAssignmentDummyClass.User = _db.ApplicationUsers.Find(userAssignmentDummyClass.UserId);

            userAssignmentDummyClass.Plan = selectedPlan;
            userAssignmentDummyClass.PlanId = id.GetValueOrDefault();



            if ((selectedPlan!=null) && (userAssignmentDummyClass.User !=null))
            {
                userplanId = id.ToString();

                return View(userAssignmentDummyClass);
            }
            else
            {
                return NotFound();
            }
     
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Upsert(string stripeToken )
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

     




            if (ModelState.IsValid)
            {

                UserAssignmentVM userAssignmentVM = new UserAssignmentVM();


                userAssignmentVM.UserId = userId;
                userAssignmentVM.UserPlanId = int.Parse(userplanId);
                userAssignmentVM.UserPlan = _db.UserPlans.Where(x => x.Id == userAssignmentVM.UserPlanId).FirstOrDefault();

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



                if (stripeToken == null)
                {

                }
                else
                {
                    var options = new ChargeCreateOptions
                    {
                        Amount = Convert.ToInt32(userAssignmentVM.UserPlan.Price * 100),
                        Currency = "cad",
                        Description = "OrderId: " + userAssignmentVM.Id,
                        Source = stripeToken

                    };

                    var service = new ChargeService();
                    Charge charge = service.Create(options);
                    if (charge.BalanceTransactionId == null)
                    {
                        var userAssignmentDummyClass = new UserAssignmentUpsertVM();
                        userAssignmentDummyClass.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                        var selectedPlan = _unitOfWork.UserPlan.Get(userAssignmentVM.UserPlanId.GetValueOrDefault());
                        userAssignmentDummyClass.User = _db.ApplicationUsers.Find(userAssignmentDummyClass.UserId);

                        userAssignmentDummyClass.Plan = selectedPlan;
                        userAssignmentDummyClass.PlanId = selectedPlan.Id;

                        return View(userAssignmentDummyClass);
                    }
                    else
                    {
                        if (charge.Status.ToLower() == "succeeded")
                        {
                            userAssignmentVM.Paid = true;
                            _unitOfWork.UserAssignmentVM.Add(userAssignmentVM);
                            _unitOfWork.Save();
                            return RedirectToAction("Index", "UserVM", new { area = "Customer" });
                        }
                    }

                }




            }
            return NotFound();
        }
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<UserAssignmentVM> allObj = _db.UserAssignmentVMs.Where(x => x.UserId == userId).ToList(); 
            foreach(var obj in allObj)
            {
                obj.Trainer = _db.ApplicationUsers.Find(obj.TrainerId);
                obj.Dietician = _db.ApplicationUsers.Find(obj.DieticianId);
                obj.User = _db.ApplicationUsers.Find(obj.UserId);
                obj.UserPlan = _db.UserPlans.Find(obj.UserPlanId);
            }
            return Json(new { data = allObj });

        }

        [HttpGet]
        public IActionResult GetOne(int? id)
        {
            var userAssignmentVM = _unitOfWork.UserAssignmentVM.Get(id.GetValueOrDefault());
           


            if (userAssignmentVM == null)
            {
                
                return NotFound();
            }
            userAssignmentVM.User = _db.ApplicationUsers.Find(userAssignmentVM.UserId);
            userAssignmentVM.Dietician = _db.ApplicationUsers.Find(userAssignmentVM.DieticianId);
            userAssignmentVM.Trainer = _db.ApplicationUsers.Find(userAssignmentVM.TrainerId);
            userAssignmentVM.UserPlan = _db.UserPlans.Find(userAssignmentVM.UserPlanId);
            userAssignmentVM.Diet = userAssignmentVM.DietId == null ? new Diet() : _unitOfWork.Diet.Get(userAssignmentVM.DietId.GetValueOrDefault());
            userAssignmentVM.WorkOut = userAssignmentVM.WorkoutId == null ? new WorkOutType() : _unitOfWork.WorkOut.Get(userAssignmentVM.WorkoutId.GetValueOrDefault());
            userAssignmentVM.BodyType = userAssignmentVM.BodyTypeId == null ? new BodyType() : _unitOfWork.BodyType.Get(userAssignmentVM.BodyTypeId.GetValueOrDefault());

            return View(userAssignmentVM);
        }

    }
}
