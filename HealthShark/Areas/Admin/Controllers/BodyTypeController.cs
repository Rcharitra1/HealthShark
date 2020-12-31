using HealthShark.DataAccess.Repository;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthShark.Areas.Admin
{
    [Area("Admin")]

    [Authorize(Roles=SD.Role_Admin)]
    public class BodyTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public BodyTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            return View();
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.BodyType.GetAll();
            return Json(new { data = allObj });
        }


        public IActionResult Upsert(int? id)
        {
            
            BodyType obj = new BodyType();
            if (id == null)
            {
                return View(obj);
            }
            obj = _unitOfWork.BodyType.Get(id.GetValueOrDefault());
            if(obj is null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]

        [AutoValidateAntiforgeryToken]


        public IActionResult Upsert(BodyType bodyType)
        {
            if (ModelState.IsValid)
            {
                if (bodyType != null)
                {
                    if (bodyType.Id != 0)
                    {
                        _unitOfWork.BodyType.Update(bodyType);

                    }
                    else
                    {
                        _unitOfWork.BodyType.Add(bodyType);

                    }
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }

                
            }
            return View(bodyType);



        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.BodyType.Get(id);
            if (obj != null)
            {
                _unitOfWork.BodyType.Remove(obj);
                return Json(new { success = true, message = "Delete successfull" });
            }

            return Json(new { success = false, message = "Delete Failed" });
        }


        
    }
}
