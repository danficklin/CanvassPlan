﻿using CanvassPlan.Server.Data;
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
            var entity = new Canvasser
            {
                Name = model.Name,
                Phone = model.Phone,
                AltPhone = model.AltPhone,
                IsDriver = model.IsDriver,
                IsLeader = model.IsLeader,
                IsTraining = model.IsTraining,
                IsActive = true,
                OwnerId = _userId,
                DateCreated = DateTimeOffset.Now,
            };
            _ctx.Canvassers.Add(entity);
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
            var entity = await _ctx.Canvassers
                .Include(c => c.Cars)
                .Include(t => t.Teams)
                .Include(s => s.Sites)
                .FirstOrDefaultAsync(c => c.CanvasserId == canvasserId && c.OwnerId == _userId);
            if (entity is null) return null;
            var detail = new CanvasserDetail
            {
                CanvasserId = canvasserId,
                Name = entity.Name,
                Phone = entity.Phone,
                AltPhone = entity.AltPhone,
                IsDriver = entity.IsDriver,
                IsLeader = entity.IsLeader,
                IsTraining = entity.IsTraining,
                IsAbsent = entity.IsAbsent,
                IsActive = entity.IsActive,
                DroveYesterday = entity.DroveYesterday,
                Teams = entity.Teams.Select(t => new TeamListItem
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                }).ToList(),
                Cars = entity.Cars.Select(c => new CarListItem
                {
                    CarId = c.CarId,
                    Name = c.Name,
                }).ToList(),
                Sites = entity.Sites.Select(s => new SiteListItem
                {
                    SiteId = s.SiteId,
                    Name = s.Name,
                }).ToList(),
                DoNotPair = entity.DoNotPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DoPair = entity.DoPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            return detail;
        }

        public async Task<CanvasserDetail> GetCanvasserByNameAsync(string name)
        {
            var entity = await _ctx.Canvassers
                .Include(nameof(Car))
                .Include(nameof(Team))
                .Include(nameof(Site))
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower() && c.OwnerId == _userId);
            if (entity is null) return null;
            var detail = new CanvasserDetail
            {
                CanvasserId = entity.CanvasserId,
                Name = entity.Name,
                Phone = entity.Phone,
                AltPhone = entity.AltPhone,
                IsDriver = entity.IsDriver,
                IsLeader = entity.IsLeader,
                IsTraining = entity.IsTraining,
                IsAbsent = entity.IsAbsent,
                IsActive = entity.IsActive,
                DroveYesterday = entity.DroveYesterday,
                Teams = entity.Teams.Select(t => new TeamListItem
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                }).ToList(),
                Cars = entity.Cars.Select(c => new CarListItem
                {
                    CarId = c.CarId,
                    Name = c.Name,
                }).ToList(),
                Sites = entity.Sites.Select(s => new SiteListItem
                {
                    SiteId = s.SiteId,
                    Name = s.Name,
                }).ToList(),
                DoNotPair = entity.DoNotPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DoPair = entity.DoPair.Select(s => new CanvasserListItem
                {
                    CanvasserId = s.CanvasserId,
                    Name = s.Name,
                }).ToList(),
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            return detail;
        }

        public async Task<System.Collections.Generic.IEnumerable<CanvasserListItem>> ListCanvassersAsync()
        {
            var query = _ctx.Canvassers
                .Where(c => c.OwnerId == _userId)
                .Select(n => new CanvasserListItem
                {
                    CanvasserId = n.CanvasserId,
                    Name = n.Name,
                });
            return await query.ToListAsync();
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
            entity.IsActive = model.IsActive;
            entity.DateModified = DateTimeOffset.Now;

            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<bool> AddCanvasserToCarAsync(int canvasserId, CanvasserAddToCar model)
        {
            {
                var canvasser = await _ctx.Canvassers.FindAsync(model.CanvasserId);
                var car = await _ctx.Cars
                    .Include(c => c.Riders)
                    .FirstOrDefaultAsync(t => t.CarId == model.CarId);
                //if (car?.OwnerId != _userId) return false;
                if (model.CanvasserId == canvasserId)
                {
                    foreach (Car c in canvasser.Cars)
                    { canvasser.Cars.Remove(c); }
                    car.Riders.Add(canvasser);
                    var numberOfChanges = await _ctx.SaveChangesAsync();
                    return numberOfChanges == 1;
                }
                return false;
            }
        }

        public async Task<bool> AddCanvasserToTeamAsync(int canvasserId, CanvasserAddToTeam model)
        {
            var canvasser = await _ctx.Canvassers.FindAsync(model.CanvasserId);
            var team = await _ctx.Teams
                .Include(c => c.Canvassers)
                .FirstOrDefaultAsync(t => t.TeamId == model.TeamId);
            //if (team?.OwnerId != _userId) return false;
            if (model.CanvasserId == canvasserId)
            {
                foreach (Team t in canvasser.Teams)
                { canvasser.Teams.Remove(t); }
                team.Canvassers.Add(canvasser);
                var numberOfChanges = await _ctx.SaveChangesAsync();
                return numberOfChanges == 1;
            }
            return false;
        }

        public async Task<bool> AddCanvasserToSiteAsync(int canvasserId, CanvasserAddToSite model)
        {
            var canvasser = await _ctx.Canvassers.FindAsync(model.CanvasserId);
            var site = await _ctx.Sites
                .Include(c => c.Canvassers)
                .FirstOrDefaultAsync(t => t.SiteId == model.SiteId);
            //if (site?.OwnerId != _userId) return false;
            if (model.CanvasserId == canvasserId)
            {
                foreach (Site s in canvasser.Sites)
                { canvasser.Sites.Remove(s); }
                site.Canvassers.Add(canvasser);
                var numberOfChanges = await _ctx.SaveChangesAsync();
                return numberOfChanges == 1;
            }
            return false;
        }
    }
}
