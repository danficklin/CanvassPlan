using CanvassPlan.Shared.Models.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Services.TeamServices
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamListItem>> ListTeamAsync();
        Task<bool> AddTeamAsync(TeamCreate model);
        Task<TeamDetail> GetTeamByIdAsync(int teamId);
        Task<TeamDetail> GetTeamByNameAsync(string name);
        Task<bool> UpdateTeamAsync(TeamEdit model);
        Task<bool> DeleteTeamAsync(int teamId);
        void SetUserId(string userId);
        //Task<bool> GenerateTeamAsync(TeamGenerate model);

    }
}
