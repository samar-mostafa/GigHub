using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancled { get;private set; }
        [Required]
        public string  ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }


        public DateTime DateTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string Venue  { get; set; }

        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Attendance> Attendances { get;private set; }

      

        public Gig()
        {
            Attendances = new Collection<Attendance>();
         
        }

        public void Cancel()
        {
           IsCancled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        internal void Modify(DateTime dateTime, string venue, int genre)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);
            
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }

            DateTime = dateTime;
            GenreId = genre;
            Venue = venue;
            
        }
    }
}
