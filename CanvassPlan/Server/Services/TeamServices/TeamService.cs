using CanvassPlan.Server.Data;
using CanvassPlan.Server.Models;
using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Car;
using CanvassPlan.Shared.Models.Team;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Services.TeamServices
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _ctx;
        public TeamService(ApplicationDbContext ctx) { _ctx = ctx; }
        private string _userId;

        public void SetUserId(string userId) => _userId = userId;
        public async Task<bool> AddTeamAsync(TeamCreate model)
        {
            var entity = new Team
            {
                Name = model.Name,
            };
            _ctx.Teams.Add(entity);
            var numberOfChanges = await _ctx.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            var entity = await _ctx.Teams.FindAsync(teamId);
            if (entity?.OwnerId != _userId) return false;
            _ctx.Teams.Remove(entity);
            return await _ctx.SaveChangesAsync() == 1;   
        }

        public async Task<TeamDetail> GetTeamByIdAsync(int teamId)
        {
            var entity = await _ctx.Teams
                .Include(nameof(Canvasser))
                .Include(nameof(Car))
                .FirstOrDefaultAsync(t => t.TeamId == teamId && t.OwnerId == _userId);
            if (entity is null) return null;
            var detail = new TeamDetail
            {
                TeamId = teamId,
                Name = entity.Name,
                Canvassers = entity.Canvassers.Select(c => new CanvasserListItem
                {
                    CanvasserId = c.CanvasserId,
                    Name = c.Name,
                }).ToList(),
                Cars = entity.Cars.Select(c => new CarListItem
                {
                    CarId = c.CarId,
                    Name = c.Name,
                }).ToList(),
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            return detail;
        }

        public async Task<IEnumerable<TeamListItem>> ListTeamAsync()
        {
            var query = _ctx.Teams
                .Where(t => t.OwnerId == _userId)
                .Select(t => new TeamListItem
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                });
            return await query.ToListAsync();
        }

        public async Task<bool> UpdateTeamAsync(TeamEdit model)
        {
            if (model == null) return false;
            var entity = await _ctx.Teams.FindAsync(model.TeamId);
            if (entity?.OwnerId != _userId) return false;
            entity.Name = model.Name;
            entity.DateModified = DateTimeOffset.Now;
            return await _ctx.SaveChangesAsync() == 1;
        }

        //public async Task<bool> GenerateTeamAsync(TeamGenerate model)
        //{

        //}
    }
}
