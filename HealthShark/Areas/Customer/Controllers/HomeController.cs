using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models;
using HealthShark.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HealthShark.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<UserPlan> plans = _unitOfWork.UserPlan.GetAll();
            List<UserPlan> activePlans= new List<UserPlan>();

            foreach(var plan in plans)
            {
                if (plan.Active == true)
                {
                    activePlans.Add(plan);
                }
            }
            return View(activePlans);
        }


        public IActionResult Details(int ? id)
        {
            UserPlan plan = new UserPlan();
            if(id is null)
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



        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
