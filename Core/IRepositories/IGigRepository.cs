using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.Repositiories
{
   public interface IGigRepository
    {
       void AddGig(Gig gig);
         IEnumerable<Gig> GetUpcomingGigs();
         Gig GetGig(int id);
         IEnumerable<Gig> GetGigsUserAttend(string userId);
         Gig GigWithAttendees(int gigId);
         IEnumerable<Gig> ArtistGigs(string userId);
        IEnumerable<Gig> SearchGigs(string query);
    }
}
