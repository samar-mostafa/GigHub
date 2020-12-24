using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GigHub.Models;
using GigHub.Data;
using Microsoft.EntityFrameworkCore;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, 
                              ApplicationDbContext context,
                              SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index( string query = null)
        {
            var userId = _userManager.GetUserId(User);

            var upcomingGigs = _context.Gigs.Include(g => g.Artist)
                               .Include(g => g.Genre).
                               Where(g => g.DateTime > DateTime.Now && !g.IsCancled);


            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }
            var attendances = _context.Attendances.
                                Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).
                                ToList().
                                ToLookup(a => a.GigId);

            var followings = _context.Followings.
                           Where(f => f.FollowerId == userId)
                           .ToList().ToLookup(f => f.FolloweeId);

            var model = new GigsVM
            {
                UpcomingGigs = upcomingGigs,
                showActions = _signInManager.IsSignedIn(User),
                Heading = "UpComing Gigs",
                SearchTerm = query,
                Attendances = attendances,
                Followings =followings
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
