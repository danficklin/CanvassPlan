using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvassPlan.Shared.Models.Canvasser
{
    internal class CanvasserCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        [Required]
        public bool IsDriver { get; set; }
        [Required]
        public bool IsLeader { get; set; }
        [Required]
        public bool IsTraining { get; set; }
    }
}
