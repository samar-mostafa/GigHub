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

namespace GigHub.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GigsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsController(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);
            var gig = _context.Gigs.
                SingleOrDefault(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancled)
                return NotFound();
            
            gig.IsCancled = true;

            _context.Update(gig);
            _context.SaveChanges();
            return Ok();
        }
    }
}
