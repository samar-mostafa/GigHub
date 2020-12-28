using GigHub.Data;
using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core.Repositiories;

namespace GigHub.Data.Repositiories
{
    public class FollowingRepository:IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetFutureFollowings(string userId)
        {
            return _context.Followings.
                          Where(f => f.FollowerId == userId)
                          .ToList();
        }

        public IEnumerable<ApplicationUser> followees(string userId)
        {
            return _context.Followings
                           .Where(f => f.FollowerId == userId)
                           .Select(f => f.Followee).ToList();
        }

        public Following GetFollowing(string FolloweeId, string userId)
        {
            return _context.Followings.SingleOrDefault(f => f.FolloweeId == FolloweeId && f.FollowerId == userId);
        }
    }
}
