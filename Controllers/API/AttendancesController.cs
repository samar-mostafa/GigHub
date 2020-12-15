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
   
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AttendancesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

       
        [HttpPost]
        public IActionResult Attend(AttendanceDTO dto)
        {
            var userId = _userManager.GetUserId(User);
            var exist = _context.Attendances.Any(a => 
                         a.GigId == dto.gigId && a.AttendeeId == userId);

            if (exist)
                return BadRequest("it is allready exist");

            var attendance = new Attendance
            {
                GigId = dto.gigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        
        }
    }
}
