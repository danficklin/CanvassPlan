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
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        [Required]
        public bool IsDriver { get; set; }
        [Required]
        public bool IsLeader { get; set; }
        [Required]
        public bool IsTraining { get; set; }
        [Required]
        public bool IsAbsent { get; set; }
        [Required]
        public bool DroveYesterday { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Canvasser> DoNotPair { get; set; }
        public virtual ICollection<Canvasser> DoPair { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
