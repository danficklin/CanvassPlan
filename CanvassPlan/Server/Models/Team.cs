using System;
using System.Collections.Generic;

namespace CanvassPlan.Server.Models
{
    public class Team
    {
        public int Id { get; set; }
        public List<Canvasser> Canvassers { get; set; }
        public Canvasser Driver { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
