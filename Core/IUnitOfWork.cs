using GigHub.Core.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        IAttendanceRepository Attendances { get; }
        void Complete();
    }
}
