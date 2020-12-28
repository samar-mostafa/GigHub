using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Data;
using GigHub.Data.Repositiories;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        public GigsController(
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new GigFormVM()
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading="Add A Gig"
             };
            return View("GigForm",viewModel);
        }


       [HttpPost]
        public IActionResult Create(GigFormVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var gig = new Gig()
                {
                    ArtistId = userId,
                    DateTime = model.GetDateTime(),
                    GenreId = model.Genre,
                    Venue = model.Venue
                };
                
                _unitOfWork.Gigs.AddGig(gig);
                _unitOfWork.Complete();

                return RedirectToAction("ArtistGigs", "Gigs");
            }

            model.Genres = _unitOfWork.Genres.GetGenres();
            return View("GigForm",model);
        }


        public IActionResult Attending()
        {
            var userId = _userManager.GetUserId(User);

            var model = new GigsVM
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttend(userId),
                showActions = _signInManager.IsSignedIn(User),
                Heading="Gigs I'm Going",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).
                                ToLookup(a => a.GigId)
             };

           return View("Gigs",model);
        }

        public IActionResult Following()
        {
            var userId = _userManager.GetUserId(User);
            var artists = _unitOfWork.Followings.followees(userId);
            return View(artists);
        }

        [Authorize]
        public IActionResult ArtistGigs()
        {
            var userId = _userManager.GetUserId(User);
            var gigs = _unitOfWork.Gigs.ArtistGigs(userId);
            return View(gigs);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig.ArtistId != _userManager.GetUserId(User))
                return NotFound();

            var model = new GigFormVM
            {
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genres = _unitOfWork.Genres.GetGenres(),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit A Gig"
            };

            return View("GigForm", model);
        }

        [Authorize]    
        public IActionResult Update(GigFormVM model)
        {
            if (ModelState.IsValid)
            {
                
                var gig = _unitOfWork.Gigs.GigWithAttendees(model.Id);

                if (gig == null)
                    return BadRequest("gig is null");
                if (gig.ArtistId != _userManager.GetUserId(User))
                    return BadRequest("the user not authorized");

                gig.Modify(model.GetDateTime(), model.Venue, model.Genre);


                _unitOfWork.Complete();

                return RedirectToAction("ArtistGigs", "Gigs");
            }

            model.Genres = _unitOfWork.Genres.GetGenres();
            return View("GigsForm", model);
        }

        [HttpPost]
       public IActionResult Search(GigsVM model)
        {
            return RedirectToAction("Index", "Home", new { query = model.SearchTerm });
        }

        public IActionResult GigDetails(int id)
        {
            var userId = _userManager.GetUserId(User);
            var gig = _unitOfWork.Gigs.GetGig(id);
           
             var followings =_unitOfWork.Followings.GetFutureFollowings(userId).ToLookup(f => f.FolloweeId);
          
            var model = new GigDetailsVM
            {
                Gig = gig,
                Followings = followings
            };
           
            if (_signInManager.IsSignedIn(User))
            {
                model.IsAttending =_unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;
                model.showActions = true;
            }

            return View(model);

        }
    }
}
