using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserAddToSite
    {
        [Required]
        public int CanvasserId { get; set; }
        [Required]
        public int SiteId { get; set; }    
    }
}
