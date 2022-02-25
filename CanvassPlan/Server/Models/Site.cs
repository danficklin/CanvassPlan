﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Server.Models
{
    public class Site
    {
        [Key]
        public int SiteId { get; set; }
        public string Name { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public int Drop { get; set; }
        public double DropDistance { get; set; }
        public string DropAddress { get; set; }
        public ICollection<Canvasser> Canvassers { get; set; }
        [Required]
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
