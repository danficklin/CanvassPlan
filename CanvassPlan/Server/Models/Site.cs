using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanvassPlan.Server.Models
{
    public class Site
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Drop { get; set; }
        public double Distance { get; set; }
        public string Area { get; set; }
        public Canvasser Canvasser { get; set; }
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [Required]
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
