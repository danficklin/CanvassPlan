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
        Task<bool> ToggleTeamActiveAsync(int id);
        Task<bool> DeleteAllTeamsAsync();
        void SetUserId(string userId);
        Task<bool> ClearTeamsAsync();
        //Task<bool> GenerateTeamAsync(TeamGenerate model);

    }
}
