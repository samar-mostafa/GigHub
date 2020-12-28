using GigHub.Data;
using GigHub.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data.Repositiories;
using GigHub.Core.Repositiories;

namespace GigHub.Date
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IGenreRepository Genres { get; set; }
        public IFollowingRepository Followings { get; set; }
        public IAttendanceRepository Attendances { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Genres = new GenreRepository(_context);
            Followings = new FollowingRepository(_context);
            Attendances = new AttendanceRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
