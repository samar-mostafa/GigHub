using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.Repositiories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}
