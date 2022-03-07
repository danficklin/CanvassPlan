using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserActiveToggle
    {
        [Required]
        public int CanvasserId { get; set; }
        public bool IsActive { get; set; }
    }
}
