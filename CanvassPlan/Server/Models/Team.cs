using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Server.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public ICollection<Canvasser> Canvassers { get; set; }
        public ICollection<Car> Cars { get; set; }  
        [Required]
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
