using System;
using System.Collections.Generic;
using System.Text;
using GigHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Attendance>().HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<Attendance>().HasOne(a => a.Gig).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Following>().HasKey(f => new { f.FollowerId, f.FolloweeId });


            builder.Entity<ApplicationUser>().HasMany(u=>u.Followers)
                .WithOne(f=>f.Followee).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>().HasMany(u => u.Follwees)
                .WithOne(f => f.Follower).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);



            
        }
    }
}
