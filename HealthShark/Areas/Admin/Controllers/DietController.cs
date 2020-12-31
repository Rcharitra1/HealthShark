using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthShark.Areas.Admin.Controllers
{

    [Area("Admin")]

    [Authorize(Roles=SD.Role_Admin+","+SD.Role_Dietician)]
    public class DietController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DietController(IUnitOfWork unitOfWork)
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
            var allObjs = _unitOfWork.Diet.GetAll();
            return Json(new { data = allObjs });
        }


        public IActionResult Upsert(int ? id)
        {
            Diet diet = new Diet();
            if(id is null)
            {
                return View(diet);
            }

            diet = _unitOfWork.Diet.Get(id.GetValueOrDefault());
            if(diet is null)
            {
                return NotFound();
            }

            return View(diet);
        }

        [HttpPost]

        [AutoValidateAntiforgeryToken]

        public IActionResult Upsert(Diet diet)
        {
            if (ModelState.IsValid)
            {
                if(diet.Id != 0)
                {
                    _unitOfWork.Diet.Update(diet);
                }
                else
                {
                    _unitOfWork.Diet.Add(diet);
                }

                _unitOfWork.Save();


                return RedirectToAction(nameof(Index));
            }

            return View(diet);
        }


        [HttpDelete]


        public IActionResult Delete (int id)
        {
            var objFromDb = _unitOfWork.Diet.Get(id);

            if (objFromDb != null)
            {
                _unitOfWork.Diet.Remove(objFromDb);

                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successfull" });
            }



            return Json(new { success = false, message = "Delete Unsuccessful" });
        }

    }
}
