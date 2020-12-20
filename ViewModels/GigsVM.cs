using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.ViewModels
{
    public class GigsVM
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool showActions { get; set; }
        public string  Heading { get; set; }

        public string SearchTerm { get; set; }
    }
}
