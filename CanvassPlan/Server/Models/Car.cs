using System;
using System.Collections.Generic;

namespace CanvassPlan.Server.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seatbelts { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Canvasser Driver { get; set; }
        public List<Canvasser> PastDrivers { get; set; }
        public List<Canvasser> PastPassengers { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
