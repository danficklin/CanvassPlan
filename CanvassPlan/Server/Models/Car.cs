﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Server.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public string Name { get; set; }
        public string OwnerId { get; set; }
        [Required]
        public int Seatbelts { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public ICollection<Canvasser> Drivers { get; set; }
        public ICollection<Team> Teams { get; set; }    
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
