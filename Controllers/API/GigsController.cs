using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers.API
{
    [Route("api/[Action]/[controller]")]
    [ApiController]
    [Authorize]
    public class GigsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);
            var gig = _context.Gigs.
                      Include(g => g.Attendances).ThenInclude(a => a.Attendee).
                SingleOrDefault(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancled)
                return NotFound();

            gig.Cancel();

            _context.Update(gig);
            _context.SaveChanges();

            return Ok(new { value = "success" });
        }


        [HttpDelete("{gigId}")]
        
        public IActionResult Attendances(int gigId)
        {
            var userId = _userManager.GetUserId(User);
            var attendance = _context.Attendances.
                             SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
            if(attendance == null)
            {
                return NotFound();
            }
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return Ok(gigId);
        }

       
    }
}
