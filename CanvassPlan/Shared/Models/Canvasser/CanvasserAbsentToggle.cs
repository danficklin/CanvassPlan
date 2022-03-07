using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserAbsentToggle
    {
        [Required]
        public int CanvasserId { get; set; }
        public bool IsAbsent { get; set; }
    }
}
