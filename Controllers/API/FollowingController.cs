using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.DTO;
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
    public class FollowingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public FollowingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost]
        public IActionResult Follow(FollowingDTO dto)
        {
            var userId = _userManager.GetUserId(User);
            var exist = _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);

            if (exist)
                return BadRequest("Following Allready Exist");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete("{artistId}")]
        public IActionResult UnFollow(string artistId)
        {
            var userId = _userManager.GetUserId(User);
            var following = _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);
            _context.Followings.Remove(following);
            _context.SaveChanges();
            return Ok(new { value = "success" });
        }



    }
}
