using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserEdit
    {
        [Required]
        public int CanvasserId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public bool IsDriver { get; set; }
        public bool IsLeader { get; set; }
        public bool IsTraining { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsActive { get; set; }
    }
}
