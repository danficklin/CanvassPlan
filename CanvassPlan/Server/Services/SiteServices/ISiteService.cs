using CanvassPlan.Shared.Models.Site;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Services.SiteServices
{
    public interface ISiteService
    {
        Task<IEnumerable<SiteListItem>> ListSitesAsync();
        Task<bool> AddSiteAsync(SiteCreate model);
        Task<SiteDetail> GetSiteByIdAsync(int siteId);
        Task<SiteDetail> GetSiteByNameAsync(string name);
        Task<bool> UpdateSiteAsync(SiteEdit model);
        Task<bool> ToggleSiteActiveAsync(int id);
        Task<bool> DeleteSiteAsync(int siteId);
        void SetUserId(string userId);

    }
}
