using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core;
using GigHub.Core.DTO;
using GigHub.Core.Models;
using GigHub.Core.Repositiories;
using GigHub.Data;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AttendancesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        { 
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

       
        [HttpPost]
        public IActionResult Attend(AttendanceDTO dto)
        {
            var userId = _userManager.GetUserId(User);

            if (_unitOfWork.Attendances.GetAttendance(dto.gigId, userId) != null)
                return BadRequest("it is allready exist");

            var attendance = new Attendance
            {
                GigId = dto.gigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.AddAttendance(attendance);
            _unitOfWork.Complete();

            return Ok();
        
        }
    }
}
