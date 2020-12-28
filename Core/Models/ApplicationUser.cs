using GigHub.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.Models
{
    public class ApplicationUser:IdentityUser
    {

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Follwees = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
        }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public ICollection <Following> Followers { get; set; }
        public ICollection<Following> Follwees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));
        }
    }
}
