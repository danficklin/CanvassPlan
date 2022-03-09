using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Site
{
    public class SiteEdit
    {
        [Required]
        public int SiteId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Notes { get; set; }
        [Required]
        public string Area { get; set; }    
        public double DropDistance { get; set; }
        public string DropAddress { get; set; }
        public bool Inactive { get; set; }
    }
}
