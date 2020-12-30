using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
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
    public class PackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _hostEnvironment;

        public PackageController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
            var allObj = _unitOfWork.Package.GetAll();
            return Json(new { data = allObj });
        }

        public IActionResult Upsert(int ? id)
        {
            Package package = new Package();
            if (id is null)
            {
                return View(package);
            }

            package = _unitOfWork.Package.Get(id.GetValueOrDefault());

            if(package is null)
            {
                return NotFound();
            }
            return View(package);

        }


        [HttpDelete]


        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Package.Get(id);
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
                    _unitOfWork.Package.Remove(objFromDb);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Delete successful" });
                }
            }
            return Json(new { success = false, message = "Delete failed" });
        }

        [HttpPost]

        [AutoValidateAntiforgeryToken]

        public IActionResult Upsert (Package package)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"img\packages");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if(package.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, package.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    package.ImageUrl = @"\img\packages\" + fileName + extenstion;
                }
                else
                {
                    if (package.Id != 0)
                    {
                        Package objFromDb = _unitOfWork.Package.Get(package.Id);
                        objFromDb.ImageUrl = package.ImageUrl;
                    }
                }
                if (package.Id == 0)
                {
                    _unitOfWork.Package.Add(package);
                }
                else
                {

                    _unitOfWork.Package.Update(package);
                }

                _unitOfWork.Save();
                    
                
                return RedirectToAction(nameof(Index));
                
            }
            

            return View(package);
        }
    }
}
