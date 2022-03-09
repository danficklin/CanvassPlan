using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Car;
using System;
using System.Collections.Generic;

namespace CanvassPlan.Shared.Models.Team
{
    public class TeamDetail
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Inactive { get; set; }
        public ICollection<CanvasserListItem> Canvassers { get; set; }
        public ICollection<CarListItem> Cars { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
