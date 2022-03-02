using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserAddToCarAsDriver
    {
        [Required]
        public int CanvasserId { get; set; }
        [Required]
        public int CarId { get; set; }    
    }
}
