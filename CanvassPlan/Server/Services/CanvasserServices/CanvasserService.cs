using CanvassPlan.Server.Data;
using CanvassPlan.Server.Models;
using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Car;
using CanvassPlan.Shared.Models.Site;
using CanvassPlan.Shared.Models.Team;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace CanvassPlan.Server.Services.CanvasserServices
{
    public class CanvasserService : ICanvasserService
    {
        private readonly ApplicationDbContext _ctx;
        public CanvasserService(ApplicationDbContext ctx) { _ctx = ctx; }
        private string _userId;

        public void SetUserId(string userId) => _userId = userId;

        public async Task<bool> AddCanvasserAsync(CanvasserCreate model)
        {
            var canvasserEntity = new Canvasser
            {
                Name = model.Name,
                Phone = model.Phone,
                AltPhone = model.AltPhone,  
                IsDriver = model.IsDriver,
                IsLeader = model.IsLeader,
                IsTraining = model.IsTraining,
                OwnerId = _userId,
                DateCreated = DateTimeOffset.Now,
            };
            _ctx.Canvassers.Add(canvasserEntity);
            var numberOfChanges = await _ctx.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteCanvasserAsync(int canvasserId)
        {
            var entity = await _ctx.Canvassers.FindAsync(canvasserId);
            if (entity?.OwnerId != _userId) return false;
            _ctx.Canvassers.Remove(entity);
            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<CanvasserDetail> GetCanvasserByIdAsync(int canvasserId)
        {
            var canvasserEntity = await _ctx.Canvassers
                .Include(c => c.Cars)
                .Include(t => t.Teams)
                .Include(s => s.Sites)
                .FirstOrDefaultAsync(c => c.CanvasserId == canvasserId && c.OwnerId == _userId);
            if (canvasserEntity is null) return null;
            var detail = new CanvasserDetail
            {
                CanvasserId = canvasserId,
                Name = canvasserEntity.Name,
                Phone = canvasserEntity.Phone,
                AltPhone = canvasserEntity.AltPhone,
                IsDriver = canvasserEntity.IsDriver,
                IsLeader = canvasserEntity.IsLeader,
                IsTraining = canvasserEntity.IsTraining,
                IsAbsent = canvasserEntity.IsAbsent,
                DroveYesterday = canvasserEntity.DroveYesterday,
                Teams = canvasserEntity.Teams.Select(t => new TeamListItem
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                }).ToList(),
                Cars = canvasserEntity.Cars.Select(c => new CarListItem
                {
                    CarId = c.CarId,
                    Name = c.Name,
                }).ToList(),
                Sites = canvasserEntity.Sites.Select(s => new SiteListItem
                {
                    SiteId = s.SiteId,
                    Name = s.Name,
                }).ToList(),
                DoNotPair = canvasserEntity.DoNotPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DoPair = canvasserEntity.DoPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DateCreated = canvasserEntity.DateCreated,
                DateModified = canvasserEntity.DateModified,
            };
            return detail;
        }

        public async Task<CanvasserDetail> GetCanvasserByNameAsync(string name)
        {
            var canvasserEntity = await _ctx.Canvassers
                .Include(nameof(Car))
                .Include(nameof(Team))
                .Include(nameof(Site))
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower() && c.OwnerId == _userId);
            if (canvasserEntity is null) return null;
            var detail = new CanvasserDetail
            {
                CanvasserId = canvasserEntity.CanvasserId,
                Name = canvasserEntity.Name,
                Phone = canvasserEntity.Phone,
                AltPhone = canvasserEntity.AltPhone,
                IsDriver = canvasserEntity.IsDriver,
                IsLeader = canvasserEntity.IsLeader,
                IsTraining = canvasserEntity.IsTraining,
                IsAbsent = canvasserEntity.IsAbsent,
                DroveYesterday = canvasserEntity.DroveYesterday,
                Teams = canvasserEntity.Teams.Select(t => new TeamListItem
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                }).ToList(),
                Cars = canvasserEntity.Cars.Select(c => new CarListItem
                {
                    CarId = c.CarId,
                    Name = c.Name,
                }).ToList(),
                Sites = canvasserEntity.Sites.Select(s => new SiteListItem
                {
                    SiteId = s.SiteId,
                    Name = s.Name,
                }).ToList(),
                DoNotPair = canvasserEntity.DoNotPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DoPair = canvasserEntity.DoPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DateCreated = canvasserEntity.DateCreated,
                DateModified = canvasserEntity.DateModified,
            };
            return detail;
        }

        public async Task<System.Collections.Generic.IEnumerable<CanvasserListItem>> ListCanvassersAsync()
        {
            var canvasserQuery = _ctx.Canvassers
                .Where(c => c.OwnerId == _userId)
                .Select(n => new CanvasserListItem
                {
                    CanvasserId = n.CanvasserId,
                    Name = n.Name,
                });
            return await canvasserQuery.ToListAsync();
        }

        public async Task<bool> UpdateCanvasserAsync(CanvasserEdit model)
        {
            if (model == null) return false;
            var entity = await _ctx.Canvassers.FindAsync(model.CanvasserId);
            if (entity?.OwnerId != _userId) return false;
            entity.Name = model.Name;
            entity.Phone = model.Phone;
            entity.AltPhone = model.AltPhone;
            entity.IsDriver = model.IsDriver;
            entity.IsLeader = model.IsLeader;
            entity.IsTraining = model.IsTraining;
            entity.IsAbsent = model.IsAbsent;
            entity.DateModified = DateTimeOffset.Now;

            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<bool> AddCanvasserToCarAsync(int canvasserId, CanvasserAddToCarAsDriver model)
        {
            var canvasser = await _ctx.Canvassers.FindAsync(model.CarId);
            var car = await _ctx.Cars
                .Include(d => d.Drivers)
                .FirstOrDefaultAsync(m => m.CarId == model.CarId);

            if (model.CarId == canvasserId)
            {
                car.Drivers.Add(canvasser);
                var numberOfChanges = await _ctx.SaveChangesAsync();
                return numberOfChanges == 1;
            }
            return false;
        }

        public async Task<bool> AddCanvasserToTeamAsync(int canvasserId, CanvasserAddToTeam model)
        {
            var canvasser = await _ctx.Canvassers.FindAsync(model.TeamId);
            var team = await _ctx.Teams
                .Include(c => c.Canvassers)
                .FirstOrDefaultAsync(m => m.TeamId == model.TeamId);

            if (model.TeamId == canvasserId)
            {
                team.Canvassers.Add(canvasser);
                var numberOfChanges = await _ctx.SaveChangesAsync();
                return numberOfChanges == 1;
            }
            return false;
        }
    }
}
