using CanvassPlan.Shared.Models.Car;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Services.CarServices
{
    public interface ICarService
    {
        Task<IEnumerable<CarListItem>> ListCarsAsync();
        Task<bool> AddCarAsync(CarCreate model);
        Task<CarDetail> GetCarByIdAsync(int carId);
        Task<CarDetail> GetCarByNameAsync(string name);
        Task<bool> UpdateCarAsync(CarEdit model);
        Task<bool> DeleteCarAsync(int carId);
        void SetUserId(string userId);
    }
}
