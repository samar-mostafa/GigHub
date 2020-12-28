using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string  OriginalVenue { get; set; }
        public NotificationType Type { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification() { }

        private Notification(Gig gig ,NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");
            
            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;
        }


        public static Notification GigAdded(Gig gig)
        {
            return new Notification(gig, NotificationType.addGig);
        }

        public static Notification GigUpdated(Gig gig , DateTime originalDateTime , string originalVenue)
        {
            var notification = new Notification(gig, NotificationType.updateGig);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;
            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.cancelGig);
        }
    }
}
