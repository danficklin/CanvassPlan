using CanvassPlan.Server.Data;
using CanvassPlan.Server.Models;
using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Site;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Services.SiteServices
{
    public class SiteService : ISiteService
    {
        private readonly ApplicationDbContext _ctx;
        public SiteService(ApplicationDbContext ctx) { _ctx = ctx; }
        private string _userId;

        public void SetUserId(string userId) => _userId = userId;   
        
        public async Task<bool> AddSiteAsync(SiteCreate model)
        {
            var entity = new Site
            {
                Name = model.Name,
                Area = model.Area,
                DropDistance = model.DropDistance,
                DropAddress = model.DropAddress,
                DateCreated = DateTimeOffset.Now,
                OwnerId = _userId
            };
            _ctx.Sites.Add(entity);
            var numberOfChanges = await _ctx.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteSiteAsync(int siteId)
        {
            var entity = await _ctx.Sites.FindAsync(siteId);
            if (entity?.OwnerId != _userId) return false;
            _ctx.Sites.Remove(entity);
            return await _ctx.SaveChangesAsync() == 1;
        }

        public async Task<SiteDetail> GetSiteByIdAsync(int siteId)
        {
            var entity = await _ctx.Sites
                .Include(c => c.Canvassers)
                .FirstOrDefaultAsync(s => s.SiteId == siteId && s.OwnerId == _userId);
            var detail = new SiteDetail
            {
                SiteId = siteId,
                Name = entity.Name,
                Area = entity.Area,
                Drop = entity.Drop,
                DropDistance = entity.DropDistance,
                DropAddress = entity.DropAddress,
                Canvassers = entity.Canvassers.Select(c => new CanvasserListItem
                {
                    CanvasserId = c.CanvasserId,
                    Name = c.Name,
                }).ToList(),
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            return detail;
        }

        public async Task<SiteDetail> GetSiteByNameAsync(string name)
        {
            var entity = await _ctx.Sites
                .Include(nameof(Canvasser))
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower() && c.OwnerId == _userId);    
            if (entity is null) return null;
            var detail = new SiteDetail
            {
                SiteId = entity.SiteId,
                Name = name,
                Area = entity.Area,
                Drop = entity.Drop,
                DropDistance = entity.DropDistance,
                DropAddress = entity.DropAddress,
                Canvassers = entity.Canvassers.Select(c => new CanvasserListItem
                {
                    CanvasserId = c.CanvasserId,
                    Name = c.Name,
                }).ToList(),
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            return detail;
        }

        public async Task<IEnumerable<SiteListItem>> ListSitesAsync()
        {
            var query = _ctx.Sites
                .Where(s => s.OwnerId == _userId)
                .Select(s => new SiteListItem
                {
                    SiteId = s.SiteId,
                    Name = s.Name,
                });
            return await query.ToListAsync();
        }

        public async Task<bool> UpdateSiteAsync(SiteEdit model)
        {
            if (model == null) return false;
            var entity = await _ctx.Sites.FindAsync(model.SiteId);  
            if (entity?.OwnerId != _userId) return false;
            entity.Name = model.Name;
            entity.Area = model.Area;
            entity.DropDistance = model.DropDistance;   
            entity.DropAddress = model.DropAddress;
            entity.DateModified = DateTimeOffset.Now;
            
            return await _ctx.SaveChangesAsync() == 1;
        }
    }
}
