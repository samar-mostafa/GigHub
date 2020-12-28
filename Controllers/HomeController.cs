using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GigHub.Data;
using Microsoft.EntityFrameworkCore;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using GigHub.Data.Repositiories;
using GigHub.Core.Models;
using GigHub.Core;
using GigHub.Core.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,
                              SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                               IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
       
        public IActionResult Index( string query = null)
        {
            var userId = _userManager.GetUserId(User);

            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = _unitOfWork.Gigs.SearchGigs(query);
            }
            
            var model = new GigsVM
            {
                UpcomingGigs = upcomingGigs,
                showActions = _signInManager.IsSignedIn(User),
                Heading = "UpComing Gigs",
                SearchTerm = query,
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).
                                ToLookup(a => a.GigId)
        };
           
            return View("Gigs",model);
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
