using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Server.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public ICollection<Canvasser> Canvassers { get; set; } = new List<Canvasser>();   
        public ICollection<Car> Cars { get; set; } = new List<Car>();
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
