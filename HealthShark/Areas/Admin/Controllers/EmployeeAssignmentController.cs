using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
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
            
                List<UserAssignmentVM> allObjs = new List<UserAssignmentVM>();
                if (User.Role == SD.Role_Trainer)
                {
                        allObjs = _db.UserAssignmentVMs.Where(x => x.TrainerId == userId).ToList();
                    foreach (var obj in allObjs)
                    {
                        obj.Dietician = _db.ApplicationUsers.Find(obj.DieticianId);
                        obj.User = _db.ApplicationUsers.Find(obj.UserId);
                        obj.Diet = (obj.DietId == null ? new Diet() : _unitOfWork.Diet.Get(obj.DietId.GetValueOrDefault()));

                    //empty object assignment to handle data unavailablity error for data tab

                    obj.WorkOut = (obj.WorkoutId == null ? new WorkOutType() : _unitOfWork.WorkOut.Get(obj.WorkoutId.GetValueOrDefault()));
                    obj.BodyType = (obj.BodyTypeId == null ? new BodyType() : _unitOfWork.BodyType.Get(obj.BodyTypeId.GetValueOrDefault()));
                    obj.UserPlan = (obj.UserPlanId == null) ? null : _unitOfWork.UserPlan.Get(obj.UserPlanId.GetValueOrDefault());
                    }

                 }
                else if (User.Role == SD.Role_Dietician)
                {
                    allObjs = _db.UserAssignmentVMs.Where(x => x.DieticianId == userId).ToList();
                    foreach (var obj in allObjs)
                    {
                        obj.Trainer = _db.ApplicationUsers.Find(obj.TrainerId);
                        obj.User = _db.ApplicationUsers.Find(obj.UserId);

                    //empty object assignment to handle data unavailablity error for data tab


                    obj.Diet = (obj.DietId == null ? new Diet() : _unitOfWork.Diet.Get(obj.DietId.GetValueOrDefault()));
                        obj.WorkOut = (obj.WorkoutId == null ? new WorkOutType() : _unitOfWork.WorkOut.Get(obj.WorkoutId.GetValueOrDefault()));
                        obj.BodyType = (obj.BodyTypeId == null ? new BodyType() : _unitOfWork.BodyType.Get(obj.BodyTypeId.GetValueOrDefault()));
                        obj.UserPlan = (obj.UserPlanId == null) ? null : _unitOfWork.UserPlan.Get(obj.UserPlanId.GetValueOrDefault());


                    
                    }
                }

                return Json(new { data = allObjs });

        }
        [HttpGet]

        public IActionResult Update(int? Id)
        {
  



            UserAssignmentVM userAssignment = new UserAssignmentVM();


            if(Id is null)
            {
                return NotFound();
            }

            userAssignment = _unitOfWork.UserAssignmentVM.Get(Id.GetValueOrDefault());

            if(userAssignment is null)
            {
                return NotFound();
            }

            userAssignment.workOuts = _unitOfWork.WorkOut.GetAll().ToList();
            userAssignment.WorkOut = (userAssignment.WorkoutId == null ? new WorkOutType() : _unitOfWork.WorkOut.Get(userAssignment.WorkoutId.GetValueOrDefault()));


            //To compensate for datatable not found error
            userAssignment.diets = _unitOfWork.Diet.GetAll().ToList();
            userAssignment.Diet = (userAssignment.DietId == null ? new Diet() : _unitOfWork.Diet.Get(userAssignment.DietId.GetValueOrDefault()));


            userAssignment.bodyTypes = _unitOfWork.BodyType.GetAll().ToList();
            userAssignment.BodyType = (userAssignment.BodyTypeId == null ? new BodyType() : _unitOfWork.BodyType.Get(userAssignment.BodyTypeId.GetValueOrDefault()));

            userAssignment.User = _db.ApplicationUsers.Where(x => x.Id == userAssignment.UserId).ToList()[0];
            userAssignment.userVM = _db.UserVMs.Where(x => x.User.Id == userAssignment.UserId).ToList()[0];

            userAssignment.UserPlan = _unitOfWork.UserPlan.Get(userAssignment.UserPlanId.GetValueOrDefault());

            



            return View(userAssignment);




        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update( int? Id , int? workOutId, int? bodyTypeId , int? dietId )
        {

            UserAssignmentVM userAssignment = _unitOfWork.UserAssignmentVM.Get(Id.GetValueOrDefault());

            if(workOutId != null)
            {
                userAssignment.WorkoutId = workOutId;
            }

            if(bodyTypeId !=null)
            {
                userAssignment.BodyTypeId = bodyTypeId;
            }
            
            if(dietId !=null)
            {
                userAssignment.DietId = dietId;
            }

            

            if (ModelState.IsValid)
            {
                if (userAssignment.Id != 0)
                {
                    
                    _unitOfWork.UserAssignmentVM.Update(userAssignment);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }


            }
            else
            {
                userAssignment.workOuts = _unitOfWork.WorkOut.GetAll().ToList();
                userAssignment.WorkOut = (userAssignment.WorkoutId == null ? new WorkOutType() : _unitOfWork.WorkOut.Get(userAssignment.WorkoutId.GetValueOrDefault()));


                //To compensate for datatable not found error
                userAssignment.diets = _unitOfWork.Diet.GetAll().ToList();
                userAssignment.Diet = (userAssignment.DietId == null ? new Diet() : _unitOfWork.Diet.Get(userAssignment.DietId.GetValueOrDefault()));


                userAssignment.bodyTypes = _unitOfWork.BodyType.GetAll().ToList();
                userAssignment.BodyType = (userAssignment.BodyTypeId == null ? new BodyType() : _unitOfWork.BodyType.Get(userAssignment.BodyTypeId.GetValueOrDefault()));

                userAssignment.User = _db.ApplicationUsers.Where(x => x.Id == userAssignment.UserId).ToList()[0];
                userAssignment.userVM = _db.UserVMs.Where(x => x.User.Id == userAssignment.UserId).ToList()[0];

                userAssignment.UserPlan = _unitOfWork.UserPlan.Get(userAssignment.UserPlanId.GetValueOrDefault());
            }
            return View(userAssignment);

        }

       
    }
}
