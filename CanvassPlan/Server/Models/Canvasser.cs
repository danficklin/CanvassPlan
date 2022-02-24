using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanvassPlan.Server.Models
{
    public class Canvasser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        [Required]
        public bool IsDriver { get; set; }
        [Required]
        public bool IsLeader { get; set; }
        [Required]
        public bool IsTraining { get; set; }
        [Required]
        public bool IsAbsent { get; set; }
        [Required]
        public bool DroveYesterday { get; set; }
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public List<Team> PastTeams { get; set; }
        [ForeignKey(nameof(Car))]
        public int CardId { get; set; }
        public Car Car { get; set; }
        [ForeignKey(nameof(Site))]
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public List<Site> BackupSites { get; set; }
        public List<Site> PastSites { get; set; }
        public List<Canvasser> DoNotPair { get; set; }
        public List<Canvasser> DoPair { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
