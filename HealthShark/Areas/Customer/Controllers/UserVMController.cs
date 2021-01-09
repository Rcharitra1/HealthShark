using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using HealthShark.Models.Models.ViewModels;
using HealthShark.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public UserVMController(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;


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


       
    }
}
