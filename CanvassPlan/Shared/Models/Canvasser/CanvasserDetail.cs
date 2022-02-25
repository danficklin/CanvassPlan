using System;
using System.Collections.Generic;

namespace CanvassPlan.Shared.Models.Canvasser
{
    internal class CanvasserDetail
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
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Canvasser> DoNotPair { get; set; }
        public virtual ICollection<Canvasser> DoPair { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
