﻿using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsVM
    {
        public Gig Gig  { get; set; }
        public bool IsFollowing { get; set; }

        public bool showActions { get; set; }
        public bool IsAttending { get; set; }
        public ILookup<string, Following> Followings { get; set; }
    }
}
