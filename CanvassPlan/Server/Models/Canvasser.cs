using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanvassPlan.Server.Models
{
    public class Canvasser
    {
        [Key]
        public int CanvasserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Notes { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public bool IsDriver { get; set; }
        public bool IsLeader { get; set; }
        public bool IsTraining { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsActive { get; set; }
        public bool DroveYesterday { get; set; }
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
        public virtual ICollection<Site> Sites { get; set; } = new List<Site>();
        public virtual ICollection<Canvasser> DoNotPair { get; set; } = new List <Canvasser>();
        public virtual ICollection<Canvasser> DoPair { get; set; } = new List<Canvasser>();
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
