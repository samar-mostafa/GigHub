﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Core.Models
{
    public class Genre
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string  Name { get; set; }
    }
}
