using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserEdit
    {
        [Required]
        public int CanvasserId { get; set; }
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
