using CanvassPlan.Shared.Models.Car;
using CanvassPlan.Shared.Models.Site;
using CanvassPlan.Shared.Models.Team;
using System;
using System.Collections.Generic;

namespace CanvassPlan.Shared.Models.Canvasser
{
    public class CanvasserDetail
    {
        public int CanvasserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public bool IsDriver { get; set; }
        public bool IsLeader { get; set; }
        public bool IsTraining { get; set; }
        public bool IsAbsent { get; set; }
        public bool DroveYesterday { get; set; }
        public virtual ICollection<TeamListItem> Teams { get; set; }
        public virtual ICollection<CarListItem> Cars { get; set; }
        public virtual ICollection<SiteListItem> Sites { get; set; }
        public virtual ICollection<CanvasserListItem> DoNotPair { get; set; }
        public virtual ICollection<CanvasserListItem> DoPair { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
