using System.ComponentModel.DataAnnotations;

namespace CanvassPlan.Shared.Models.Car
{
    public class CarEdit
    {
        [Required]
        public int CarId { get; set; }
        public string Name { get; set; }
        public int Seatbelts { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
