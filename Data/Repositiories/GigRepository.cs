using GigHub.Core.Repositiories;
using GigHub.Data;
using GigHub.Core.Models;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Data.Repositiories
{
   
    public class GigRepository:IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public void AddGig(Gig gig)
        { 
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetUpcomingGigs()
        {
           return _context.Gigs.Include(g => g.Artist)
                               .Include(g => g.Genre).
                               Where(g => g.DateTime > DateTime.Now && !g.IsCancled);
        }
        public Gig GetGig(int id)
        {
           return _context.Gigs.Include(g => g.Artist)
                    .SingleOrDefault(g => g.Id == id );
        }
        public IEnumerable<Gig> GetGigsUserAttend(string userId)
        {
            return _context.Attendances.
                       Where(a => a.AttendeeId == userId).Include(e => e.Gig)
                       .ThenInclude(g => g.Artist)
                       .Include(e => e.Gig).ThenInclude(g => g.Genre).
                       Select(a => a.Gig).
                       ToList();
        }

        public Gig GigWithAttendees(int gigId)
        {
           return _context.Gigs.
                          Include(g => g.Attendances)
                          .ThenInclude(a => a.Attendee).
                          SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> ArtistGigs(string userId)
        {
            return _context.Gigs
                     .Where(g =>
                     g.ArtistId == userId &&
                     g.DateTime > DateTime.Now &&
                     !g.IsCancled)
                     .Include(g => g.Genre).ToList();
        }


        public IEnumerable<Gig> SearchGigs(string query)
        {
            var upcomingGigs = GetUpcomingGigs();
            return upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
        }
    }
}
