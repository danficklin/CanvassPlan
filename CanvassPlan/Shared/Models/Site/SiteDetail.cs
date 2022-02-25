using CanvassPlan.Shared.Models.Canvasser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Site
{
    public class SiteDetail
    {
        public int SiteId { get; set; }
        public string Name { get; set; }
        public int Drop { get; set; }
        public double DropDistance { get; set; }
        public string DropAddress { get; set; }
        public ICollection<CanvasserListItem> Canvassers { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
