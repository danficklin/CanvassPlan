using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserAddToTeam
    {
        [Required]
        public int CanvasserId { get; set; }
        [Required]
        public int TeamId { get; set; }    
    }
}
