using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Team;
using System.Collections.Generic;

namespace CanvassPlan.Shared.Models.Car
{
    public class CarListItem
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public bool Inactive { get; set; }
        public ICollection<CanvasserListItem> Riders { get; set; }
        public ICollection<TeamListItem> Teams { get; set; }

    }
}
