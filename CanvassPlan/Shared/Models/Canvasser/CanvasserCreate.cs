using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public bool IsDriver { get; set; }
        public bool IsLeader { get; set; }
        public bool IsTraining { get; set; }
    }
}
