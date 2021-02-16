using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthShark.Models.Models;
using HealthShark.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthShark.Areas.Identity.Pages.Account
{
    public class AddAdminUserModel : PageModel
    {
     
        private readonly UserManager<IdentityUser> _userManager;
    
        private readonly RoleManager<IdentityRole> _roleManager;
  
        public AddAdminUserModel(
           UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager
           )
        {
            _userManager = userManager;

            _roleManager = roleManager;
        }
        public async Task<IActionResult> OnGet()
        {
            //Create roles for website and create admin

            if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                var userAdmin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "1112223333",
                    Name = "Admin R Charitra"

                };



                var resultUser = await _userManager.CreateAsync(userAdmin, "Admin@123");
                await _userManager.AddToRoleAsync(userAdmin, SD.Role_Admin);
            }

            if (!await _roleManager.RoleExistsAsync(SD.Role_Customer))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
            }
            if (!await _roleManager.RoleExistsAsync(SD.Role_Trainer))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Trainer));
            }
            if (!await _roleManager.RoleExistsAsync(SD.Role_Dietician))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Dietician));
            }

           

            return Page();

        }
    }
}
