using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
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



        

    }
}
