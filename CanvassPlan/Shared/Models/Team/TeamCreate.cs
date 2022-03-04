using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Car;
using System;
using System.Collections.Generic;

namespace CanvassPlan.Shared.Models.Team
{
    public class TeamCreate
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
