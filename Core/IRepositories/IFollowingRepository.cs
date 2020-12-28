using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.Repositiories
{
    public interface IFollowingRepository
    {
        IEnumerable<Following> GetFutureFollowings(string userId);
        IEnumerable<ApplicationUser> followees(string userId);

        Following GetFollowing(string FolloweeId, string userId);
    }
}
