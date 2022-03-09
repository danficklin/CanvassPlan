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
                Notes = model.Notes,
                DateCreated = DateTimeOffset.Now,
                Inactive = false,
                OwnerId = _userId
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

        public async Task<bool> DeleteAllTeamsAsync()
        {
            var entity = await _ctx.Teams.Where(t => t.OwnerId == _userId).ToListAsync();
            _ctx.RemoveRange(entity);
            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<TeamDetail> GetTeamByIdAsync(int teamId)
        {
            var entity = await _ctx.Teams
                .Include(c => c.Canvassers)
                .Include(r => r.Cars)
                .FirstOrDefaultAsync(t => t.TeamId == teamId && t.OwnerId == _userId);
            if (entity is null) return null;
            var detail = new TeamDetail
            {
                TeamId = teamId,
                Name = entity.Name,
                Notes = entity.Notes,
                Inactive = entity.Inactive,
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

        public async Task<TeamDetail> GetTeamByNameAsync(string name)
        {
            var entity = await _ctx.Teams
                .Include(nameof(Canvasser))
                .Include(nameof(Car))
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower() && t.OwnerId == _userId);
            if (entity is null) return null;
            var detail = new TeamDetail
            {
                TeamId = entity.TeamId,
                Name = name,
                Notes = entity.Notes,
                Inactive = entity.Inactive,
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
                    Inactive = t.Inactive,
                });
            return await query.ToListAsync();
        }

        public async Task<bool> UpdateTeamAsync(TeamEdit model)
        {
            if (model == null) return false;
            var entity = await _ctx.Teams.FindAsync(model.TeamId);
            if (entity?.OwnerId != _userId) return false;
            entity.Name = model.Name;
            entity.Notes = model.Notes;
            entity.Inactive = model.Inactive;
            entity.DateModified = DateTimeOffset.Now;
            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<bool> ToggleTeamActiveAsync(int id)
        {
            if (id == default) return false;
            var entity = await _ctx.Teams.FindAsync(id);
            if (entity?.OwnerId != _userId) return false;
            entity.Inactive = !entity.Inactive;

            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<bool> ClearTeamsAsync()
        {
            var entity = await _ctx.Teams.Where(t => t.OwnerId == _userId).ToListAsync();
            foreach (Team t in entity) 
            { 
                t.Inactive = true; 
            }
            return await _ctx.SaveChangesAsync() == 1;
        }

        //public async Task<bool> GeneratePlanAsync()
        //{
        //    var canvasser = await _ctx.Canvassers.Where(v => v.OwnerId == _userId).ToListAsync();
        //    var car = await _ctx.Cars.Where(c => c.OwnerId == _userId).ToListAsync();
        //    var team = await _ctx.Teams.Where(t => t.OwnerId == _userId).ToListAsync();
        //    var site = await _ctx.Sites.Where(s => s.OwnerId == _userId).ToListAsync();

        //}
    }
}
