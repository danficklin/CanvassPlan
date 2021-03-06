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
        Task<bool> ToggleCanvasserAbsentAsync(int id);
        Task<bool> ToggleCanvasserActiveAsync(int id);
        Task<bool> ToggleDriverAsync(int id);
        Task<bool> DeleteCanvasserAsync(int canvasserId);
        Task<bool> AddCanvasserToCarAsync(int canvasserId, CanvasserAddToCar model);
        Task<bool> AddCanvasserToTeamAsync(int canvasserId, CanvasserAddToTeam model);
        Task<bool> AddCanvasserToSiteAsync(int canvasserId, CanvasserAddToSite model);
        void SetUserId(string userId);

    }
}
