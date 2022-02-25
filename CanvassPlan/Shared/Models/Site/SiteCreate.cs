using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvassPlan.Shared.Models.Site
{
    public class SiteCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double DropDistance { get; set; }
        [Required]
        public string DropAddress { get; set; }
    }
}
