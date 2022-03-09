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

namespace CanvassPlan.Server.Services.CarServices
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _ctx;
        public CarService(ApplicationDbContext ctx) { _ctx = ctx; }
        private string _userId;

        public void SetUserId(string userId) => _userId = userId;

        public async Task<bool> AddCarAsync(CarCreate model)
        {
            if (model == null) return false;
            var carEntity = new Car
            {
                Name = model.Name,
                Notes = model.Notes,
                Seatbelts = model.Seatbelts,
                Make = model.Make,
                Model = model.Model,
                Year = model.Year,
                Inactive = false,
                DateCreated = DateTimeOffset.Now,
                OwnerId = _userId
            };
            _ctx.Cars.Add(carEntity);
            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            var entity = await _ctx.Cars.FindAsync(carId);
            if (entity?.OwnerId != _userId) return false;
            _ctx.Cars.Remove(entity);
            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<CarDetail> GetCarByIdAsync(int carId)
        {
            var entity = await _ctx.Cars
                .Include(c => c.Riders)
                .Include(t => t.Teams)
                .FirstOrDefaultAsync(c => c.CarId == carId && c.OwnerId == _userId);
            if (entity is null) return null;
            var detail = new CarDetail
            {
                CarId = carId,
                Name = entity.Name,
                Notes = entity.Notes,
                Seatbelts = entity.Seatbelts,
                Make = entity.Make,
                Model = entity.Model,
                Year = entity.Year,
                Inactive = entity.Inactive,
                Riders = entity.Riders.Select(t => new CanvasserListItem
                {
                    CanvasserId = t.CanvasserId,
                    Name = t.Name
                }).ToList(),
                Teams = entity.Teams.Select(t => new TeamListItem
                {
                    TeamId = t.TeamId,
                }).ToList(),
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            return detail;
        }


        public async Task<CarDetail> GetCarByNameAsync(string name)
        {
            var entity = await _ctx.Cars
                .Include(nameof(Canvasser))
                .Include(nameof(Site))
                .Include(nameof(Team))
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower() && c.OwnerId == _userId);
            if (entity is null) return null;
            var detail = new CarDetail
            {
                CarId = entity.CarId,
                Name = name,
                Notes = entity.Notes,
                Seatbelts = entity.Seatbelts,
                Make = entity.Make,
                Model = entity.Model,
                Year = entity.Year,
                Inactive = entity.Inactive,
                Riders = entity.Riders.Select(t => new CanvasserListItem
                {
                    CanvasserId = t.CanvasserId,
                    Name = t.Name
                }).ToList(),
                Teams = entity.Teams.Select(t => new TeamListItem
                {
                    TeamId = t.TeamId,
                }).ToList(),
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            return detail;
        }

        public async Task<IEnumerable<CarListItem>> ListCarsAsync()
        {
            var carQuery = _ctx.Cars
                .Where(c => c.OwnerId == _userId)
                .Select(c => new CarListItem
                {
                    CarId = c.CarId,
                    Name = c.Name,
                    Inactive = c.Inactive,
                });
            return await carQuery.ToListAsync();

        }

        public async Task<bool> UpdateCarAsync(CarEdit model)
        {
            if (model == null) return false;
            var entity = await _ctx.Cars.FindAsync(model.CarId);
            if (entity?.OwnerId != _userId) return false;
            entity.Name = model.Name;
            entity.Notes = model.Notes;
            entity.Seatbelts = model.Seatbelts;
            entity.Make = model.Make;
            entity.Model = model.Model;
            entity.Year = model.Year;
            entity.Inactive = model.Inactive;
            entity.DateModified = DateTimeOffset.Now;

            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<bool> ToggleCarActiveAsync(int id)
        {
            if (id == default) return false;
            var entity = await _ctx.Cars.FindAsync(id);
            if (entity?.OwnerId != _userId) return false;
            entity.Inactive = !entity.Inactive;

            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<bool> AddCarToTeamAsync(int teamId, CarAddToTeam model)
        {
            var car = await _ctx.Cars.FindAsync(model.CarId);
            var team = await _ctx.Teams
                .Include(c => c.Cars)
                .Where(c => c.OwnerId == _userId)
                .FirstOrDefaultAsync(t => t.TeamId == model.TeamId);
            if (model.TeamId == teamId)
            {
                team.Cars.Add(car);
                var numberOfChanges = await _ctx.SaveChangesAsync();
                return numberOfChanges == 1;
                
            }
            return false;
        }
    }
}
