using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Car
{
    public class CarCreate
    {
        [Required]
        public string Name { get; set; }
        public string Notes { get; set; }
        [Required]
        public int Seatbelts { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool Inactive { get; set; }
    }
}
