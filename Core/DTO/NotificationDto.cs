using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.DTO
{
    public class NotificationDto
    {
        public DateTime DateTime { get;  set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public NotificationType Type { get;  set; }
        public GigDto Gig { get;  set; }
    }
}
