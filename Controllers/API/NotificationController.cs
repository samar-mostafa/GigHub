using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GigHub.Core.DTO;
using GigHub.Core.Models;
using GigHub.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public NotificationController(ApplicationDbContext context, 
                                       UserManager<ApplicationUser> userManager,
                                       IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = _userManager.GetUserId(User);
            var notifications = _context.UserNotifications.
                                Where(un => un.UserId == userId && !un.IsRead).
                                Include(un => un.Notification).
                                ThenInclude(n => n.Gig).
                                ThenInclude(g => g.Artist).
                                Select(un => un.Notification).ToList();

            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }
    }
}
