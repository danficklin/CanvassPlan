using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Site
{
    public class SiteEdit
    {
        [Required]
        public int SiteId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double DropDistance { get; set; }
        [Required]
        public string DropAddress { get; set; }
    }
}
