using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GigsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new GigFormVM()
            {
                Genres = _context.Genres.ToList()
             };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Create(GigFormVM model)
        {
            if (ModelState.IsValid)
            {
                var gig = new Gig()
                {                   
                    ArtistId = _userManager.GetUserId(User),
                    DateTime = model.GetDateTime(),
                    GenreId = model.Genre,
                    Venue = model.Venue                  
                };
            
                _context.Gigs.Add(gig);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            model.Genres = _context.Genres.ToList();
            return View(model);

        }
    }
}
