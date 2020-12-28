using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.ViewModels
{
    public class GigFormVM
    {
        public int Id { get; set; } 

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        //[ValidTime]
        public string Time { get; set; }

        [Required]
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action { get
            {
                return (Id != 0) ? "Update" : "Create";
            }  }

        public DateTime GetDateTime()
        {
            {
                return DateTime.Parse(string.Format("{0} {1}", Date, Time));
            }
        }

    }
}
