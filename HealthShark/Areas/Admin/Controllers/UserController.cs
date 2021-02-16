using HealthShark.Data;
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

    [Authorize(Roles=SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObjFromDb = _db.ApplicationUsers.ToList();

            var userRole = _db.UserRoles.ToList();

            var roles = _db.Roles.ToList();
            foreach(var user in allObjFromDb)
            {
                var roleID = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleID.ToString()).Name;
                if (user.Role == null)
                {
                    user.Role = "Not assigned";
                }

            }
            return Json(new { data = allObjFromDb });
        }

        //[HttpDelete]

        //public IActionResult Delete(string userId)
        //{
        //    var user = _db.ApplicationUsers.Where(x => x.Id == userId).FirstOrDefault();
        //    var userRole = _db.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
        //    if(user!=null)
        //    {

        //        _db.UserRoles.Remove(userRole);
        //        _db.SaveChanges();
        //        _db.ApplicationUsers.Remove(user);
        //        _db.SaveChanges();
        //        return Json(new { success = true, message = "Delete SuccessFull" });
        //    }

        //    return Json(new { success = false, message = "Delete failed" });
        //}

        [HttpPost]


        public IActionResult ConfirmUser([FromBody] string userId)
        {
            var user = _db.ApplicationUsers.Where(x => x.Id == userId).FirstOrDefault();


            if (user != null)
            {
                if (user.EmailConfirmed == false)
                {
                    user.EmailConfirmed = true;

                    _db.ApplicationUsers.Update(user);
                    _db.SaveChanges();
                    return Json(new { success = true, message = "user confirmed" });
                }

                return Json(new { success = true, message = "user already confirmed" });
            }

            return Json(new { success = false, message = "confirmation failed" });



        }



        

    }
}
