using GigHub.Data;
using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core.Repositiories;

namespace GigHub.Data.Repositiories
{
    public class AttendanceRepository:IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;          
        }

        public void AddAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public Attendance GetAttendance(int gigId , string userId)
        {
            return _context.Attendances.
                            SingleOrDefault(a => a.AttendeeId == userId && a.GigId == gigId);
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances.
                                Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).
                                ToList();
        }

        public void Delete(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }


    }
}
