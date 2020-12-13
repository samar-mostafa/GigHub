﻿using System;
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

        public HomeController(ILogger<HomeController> logger, 
                              ApplicationDbContext context,
                              SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var model = new GigsVM
            {
                UpcomingGigs = _context.Gigs.Include(g => g.Artist)
                               .Include(g => g.Genre).Where(g => g.DateTime > DateTime.Now),
                showActions = _signInManager.IsSignedIn(User)
            };
           
            return View(model);
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
