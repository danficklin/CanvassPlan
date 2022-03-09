
using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Car;
using System.Collections.Generic;

namespace CanvassPlan.Shared.Models.Team
{
    public class TeamListItem
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public bool Inactive { get; set; }
        public ICollection<CanvasserListItem> Canvassers { get; set; } = new List<CanvasserListItem>(); 
        public ICollection<CarListItem> Cars { get; set; } = new List<CarListItem>(); 
    }
}
