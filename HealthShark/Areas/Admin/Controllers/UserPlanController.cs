using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthShark.Areas.Admin.Controllers
{
    [Area("Admin")]



    [Authorize(Roles = SD.Role_Admin)]

    public class UserPlanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _hostEnvironment;

        public UserPlanController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.UserPlan.GetAll();
            return Json(new { data = allObj });
        }

        public IActionResult Upsert(int ? id)
        {
            UserPlan plan = new UserPlan();
            if (id is null)
            {
                return View(plan);
            }

            plan = _unitOfWork.UserPlan.Get(id.GetValueOrDefault());

            if(plan is null)
            {
                return NotFound();
            }
            return View(plan);

        }


        [HttpDelete]


        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.UserPlan.Get(id);
            if (objFromDb != null)
            {
                if (objFromDb.ImageUrl != null)
                {
                    var webRoot = _hostEnvironment.WebRootPath;
                    var imagePath = Path.Join(webRoot, objFromDb.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    _unitOfWork.UserPlan.Remove(objFromDb);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Delete successful" });
                }
            }
            return Json(new { success = false, message = "Delete failed" });
        }

        [HttpPost]

        [AutoValidateAntiforgeryToken]

        public IActionResult Upsert(UserPlan plan)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"img/userplans");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (plan.ImageUrl != null)
                    {
                        //this means this is an edit and we need to remove the old image

                        var imagePath = Path.Combine(webRootPath, plan.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    plan.ImageUrl = @"\img\userplans\" + fileName + extension;
                }
                else
                {
                    //update when they do not change the image 
                    if (plan.Id != 0)
                    {
                        UserPlan objFromDb = _unitOfWork.UserPlan.Get(plan.Id);
                        plan.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                if (plan.Id == 0)
                {
                    _unitOfWork.UserPlan.Add(plan);
                }
                else
                {
                    _unitOfWork.UserPlan.Update(plan);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(
                    Index));
            }
            else
            {
                if (plan.Id != 0)
                {
                    plan= _unitOfWork.UserPlan.Get(plan.Id);
                }
            }
            return View(plan);
        }
    }
}
