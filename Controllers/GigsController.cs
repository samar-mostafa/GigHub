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
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public GigsController(ApplicationDbContext context,
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new GigFormVM()
            {
                Genres = _context.Genres.ToList(),
                Heading="Add A Gig"
             };
            return View("GigForm",viewModel);
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

                return RedirectToAction("ArtistGigs", "Gigs");
            }

            model.Genres = _context.Genres.ToList();
            return View("GigForm",model);

        }


        public IActionResult Attending()
        {
            var userId = _userManager.GetUserId(User);
            var gigs = _context.Attendances.
                       Where(a => a.AttendeeId == userId).Include(e=>e.Gig)
                       .ThenInclude(g => g.Artist)
                       .Include(e => e.Gig).ThenInclude(g => g.Genre).
                       Select(a => a.Gig).
                       ToList();

           
            var model = new GigsVM
            {
                UpcomingGigs = gigs,
                showActions = _signInManager.IsSignedIn(User),
                Heading="Gigs I'm Going"
             };

           return View("Gigs",model);
        }

        public IActionResult Following()
        {
            var userId = _userManager.GetUserId(User);
            var artists = _context.Followings
                           .Where(f => f.FollowerId == userId)
                           .Select(f => f.Followee).ToList();
            return View(artists);

        }

        [Authorize]
        public IActionResult ArtistGigs()
        {
            var userId = _userManager.GetUserId(User);
            var gigs = _context.Gigs
                      .Where(g => 
                      g.ArtistId == userId && 
                      g.DateTime > DateTime.Now &&
                      !g.IsCancled)
                      .Include(g => g.Genre).ToList();

            return View(gigs);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var gig = _context.Gigs
                      .SingleOrDefault(g => g.Id == id && g.ArtistId == userId);
            var model = new GigFormVM
            {
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genres = _context.Genres.ToList(),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading="Edit A Gig"
            };

            return View("GigForm", model);
        }

        [Authorize]    
        public IActionResult Update(GigFormVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var gig = _context.Gigs.
                          Include(g=>g.Attendances)
                          .ThenInclude(a=>a.Attendee).
                          SingleOrDefault(g => g.Id == model.Id && g.ArtistId == userId);

                gig.Modify(model.GetDateTime(), model.Venue, model.Genre);
               
                _context.Gigs.Update(gig);
                _context.SaveChanges();

                return RedirectToAction("ArtistGigs", "Gigs");
            }

            model.Genres = _context.Genres.ToList();
            return View("GigsForm", model);
        }
    }
}
