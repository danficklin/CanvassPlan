using CanvassPlan.Shared.Models.Canvasser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Services.CanvasserServices
{
    public interface ICanvasserService
    {
        Task<IEnumerable<CanvasserListItem>> ListCanvassersAsync();
        Task<bool> AddCanvasserAsync(CanvasserCreate model);
        Task<CanvasserDetail> GetCanvasserByIdAsync(int canvasserId);
        Task<CanvasserDetail> GetCanvasserByNameAsync(string name);
        Task<bool> UpdateCanvasserAsync(CanvasserEdit model);
        Task<bool> DeleteCanvasserAsync(int canvasserId);
        void SetUserId(string userId);

    }
}
