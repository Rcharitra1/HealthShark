using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using HealthShark.Models.Models.ViewModels;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthShark.Areas.Customer.Controllers
{
    [Area("Customer")]

    [Authorize(Roles=SD.Role_Customer)]
    public class UserVMController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserVMController(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;


        }


 
        
        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserVM uservm = new UserVM();


            

           
           if (uservm.Id == 0)
           {
                uservm.UserId = userId;
              _unitOfWork.UserVM.Add(uservm);
              _unitOfWork.Save();

            }
            else
            {
                uservm = _unitOfWork.UserVM.GetFirstOrDefualt(u => u.UserId == userId, includeProperties : "User");
            }

            uservm = _unitOfWork.UserVM.GetFirstOrDefualt(u => u.UserId == userId ,includeProperties : "User");

           

            return View(uservm);   
        }





        [HttpGet]
        public IActionResult Update(int? id)
        {
            UserVM vm = new UserVM();

            if (id is null)
            {
                return View(vm);
            }

            vm = _unitOfWork.UserVM.GetFirstOrDefualt(u=> u.Id ==id.GetValueOrDefault(), includeProperties:"User");
            if(vm is null)
            {
                return NotFound();
            }

            return View(vm);

        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update (UserVM userVM)
        {
            if(ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(files.Count>0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"img/userdetails");
                    var extensions = Path.GetExtension(files[0].FileName);
                    if(userVM.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, userVM.ImageUrl.Trim('\\'));
                        if(System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extensions), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    userVM.ImageUrl = @"\img\userdetails\" + fileName + extensions;
                }
                else
                {
                    if(userVM.Id !=0)
                    {
                        UserVM objFromDb = _unitOfWork.UserVM.Get(userVM.Id);
                        userVM.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                _unitOfWork.UserVM.Update(userVM);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
               
            }
            else
            {
                return View(userVM);
            }
           
        }

        //In case I would like to have delete functionality

        //[HttpDelete]


        //public IActionResult Delete(int id)
        //{
        //    var objFromDb = _unitOfWork.UserVM.Get(id);
        //    if(objFromDb != null)
        //    {
        //        if(objFromDb.ImageUrl !=null)
        //        {
        //            var webRoot = _hostEnvironment.WebRootPath;
        //            var imagePath = Path.Join(webRoot, objFromDb.ImageUrl.TrimStart('\\'));
        //            if(System.IO.File.Exists(imagePath))
        //            {
        //                System.IO.File.Delete(imagePath);
        //            }
        //            _unitOfWork.UserVM.Remove(objFromDb);
        //            _unitOfWork.Save();
        //            return Json(new { success = true, message = "Delete Successfull" });
        //        }

        //    }
        //    return Json(new { success = false, message = "delete failed" });
        //}




    }
}
