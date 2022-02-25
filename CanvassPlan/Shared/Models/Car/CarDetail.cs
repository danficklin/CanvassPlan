using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Team;
using System;
using System.Collections.Generic;

namespace CanvassPlan.Shared.Models.Car
{
    public class CarDetail
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public int Seatbelts { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public ICollection<CanvasserListItem> Drivers { get; set; }
        public ICollection<TeamListItem> Teams { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
