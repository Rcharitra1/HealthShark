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

    [Authorize(Roles=SD.Role_Admin+","+SD.Role_Trainer)]
    public class WorkOutTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkOutTypeController(IUnitOfWork unitOfWork)
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
            var allObjs = _unitOfWork.WorkOut.GetAll();
            return Json(new { data = allObjs });
        }


        public IActionResult Upsert(int ? id)
        {
            WorkOutType workOut = new WorkOutType();
            if(id is null)
            {
                return View(workOut);
            }

            workOut = _unitOfWork.WorkOut.Get(id.GetValueOrDefault());
            if(workOut is null)
            {
                return NotFound();
            }

            return View(workOut);
        }

        [HttpPost]

        [AutoValidateAntiforgeryToken]

        public IActionResult Upsert(WorkOutType workOut)
        {
            if (ModelState.IsValid)
            {
                if(workOut.Id != 0)
                {
                    _unitOfWork.WorkOut.Update(workOut);
                }
                else
                {
                    _unitOfWork.WorkOut.Add(workOut);
                }

                _unitOfWork.Save();


                return RedirectToAction(nameof(Index));
            }

            return View(workOut);
        }


        [HttpDelete]


        public IActionResult Delete (int id)
        {
            var objFromDb = _unitOfWork.WorkOut.Get(id);

            if (objFromDb != null)
            {
                _unitOfWork.WorkOut.Remove(objFromDb);

                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successfull" });
            }



            return Json(new { success = false, message = "Delete Unsuccessful" });
        }

    }
}
