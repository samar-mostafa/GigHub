using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancled { get; set; }
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
    }
}
