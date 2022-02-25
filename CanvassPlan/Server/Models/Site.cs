using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Server.Models
{
    public class Site
    {
        [Key]
        public int SiteId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Drop { get; set; }
        public double Distance { get; set; }
        public ICollection<Canvasser> Canvassers { get; set; }
        [Required]
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
