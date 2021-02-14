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

                        //obj.WorkOut = (obj.WorkoutId == null ? new WorkOutType() : _unitOfWork.WorkOut.Get(obj.WorkoutId.GetValueOrDefault()));
                        //obj.BodyType = (obj.BodyTypeId == null ? new BodyType() : _unitOfWork.BodyType.Get(obj.BodyTypeId.GetValueOrDefault()));
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
            EmployeeAssignment vm = new EmployeeAssignment();
            if(Id is null)
            {
                return NotFound();
            }


            vm.UserAssignmentVM = _db.UserAssignmentVMs.Find(Id);

            UserVM userVM = new UserVM();

            List<UserVM> userVms = _db.UserVMs.Where(x => x.User.Id == vm.UserAssignmentVM.UserId).ToList();

            userVM = userVms[0];

            vm.UserAssignmentVM.UserPlan = _db.UserPlans.Where(x => x.Id == vm.UserAssignmentVM.UserPlanId).ToList()[0];

            vm.UserAssignmentVM.BodyType = (vm.UserAssignmentVM.BodyTypeId == null ? new BodyType() : _unitOfWork.BodyType.Get(vm.UserAssignmentVM.BodyTypeId.GetValueOrDefault()));

            vm.UserAssignmentVM.WorkOut = (vm.UserAssignmentVM.WorkOut == null ? new WorkOutType() : _unitOfWork.WorkOut.Get(vm.UserAssignmentVM.WorkoutId.GetValueOrDefault()));

            vm.UserAssignmentVM.Diet = (vm.UserAssignmentVM.Diet == null ? new Diet() : _unitOfWork.Diet.Get(vm.UserAssignmentVM.DietId.GetValueOrDefault()));
            
            

            vm.UserVM = userVM;
            vm.UserVM.User = _db.ApplicationUsers.Where(x => x.Id == userVM.UserId).ToList()[0];

            vm.diets = _unitOfWork.Diet.GetAll().ToList();
            vm.workOutTypes = _unitOfWork.WorkOut.GetAll().ToList();
            vm.bodyTypes = _unitOfWork.BodyType.GetAll().ToList();



            
            if(vm is null)
            {
                return NotFound();
            }

            return View(vm);


        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(EmployeeAssignment employeeAssignment)
        {
            UserAssignmentVM userAssignment = new UserAssignmentVM();
            userAssignment= employeeAssignment.UserAssignmentVM;
            if (ModelState.IsValid)
            {
                if (userAssignment.Id != 0)
                {
                    _unitOfWork.UserAssignmentVM.Update(userAssignment);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                

            }
            return View(employeeAssignment);
        }

       
    }
}
