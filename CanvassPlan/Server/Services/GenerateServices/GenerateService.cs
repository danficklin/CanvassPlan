using CanvassPlan.Shared.Models.Canvasser;
using CanvassPlan.Shared.Models.Car;
using CanvassPlan.Shared.Models.Site;
using CanvassPlan.Shared.Models.Team;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CanvassPlan.Server.Services.GenerateServices
{
    public class GenerateService
    {
        private List<CanvasserListItem> Canvassers;
        private List<CarListItem> Cars;
        private List<SiteListItem> Sites;
        private List<TeamListItem> Teams;
        private TeamCreate model = new();
        private string message;
        Random random = new Random();

        private async bool CreateTeamForEachPresentDriver()
        {
            bool AllSuccessful = false;
            int teamsMade = 0;
            int activeCars = 0;
            List<CanvasserListItem> Drivers = Canvassers.Where(c => c.IsDriver == true).ToList();
            foreach (var c in Cars)
            {
                if (c.Inactive == true)
                {
                    activeCars++;
                }
            }
            while (teamsMade < activeCars)
            {
                var d = Drivers[random.Next(Drivers.Count)];
                if (d.IsDriver == true && d.Inactive == false && d.IsAbsent == false)
                {
                    model.Name = d.Name;
                    teamsMade++;
                    var createRes = await http.PostAsJsonAsync<TeamCreate>("/api/team", model);
                    if (createRes.IsSuccessStatusCode) { AllSuccessful = true; }
                    else { AllSuccessful = false; }
                }
                Drivers.Remove(d);
            }
            if (AllSuccessful == true) { return true }
            else { message = "Could not generate a plan. Please try again later."; }
        }
    }
}
