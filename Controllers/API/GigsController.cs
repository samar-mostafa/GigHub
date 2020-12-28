using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Data;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public GigsController( UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }


        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);
            var gig = _unitOfWork.Gigs.GigWithAttendees(id);

            if (gig == null)
                return BadRequest("gig is null");

            if (gig.ArtistId != userId)
                return NotFound();

            if (gig.IsCancled)
                return NotFound();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok(new { value = "success" });
        }


        [HttpDelete("{gigId}")]
        
        public IActionResult Attendances(int gigId)
        {
            var userId = _userManager.GetUserId(User);
            var attendance = _unitOfWork.Attendances.GetAttendance(gigId, userId);
            if(attendance == null)
            {
                return NotFound();
            }
            _unitOfWork.Attendances.Delete(attendance);
            _unitOfWork.Complete();
            return Ok(gigId);
        }

       
    }
}
