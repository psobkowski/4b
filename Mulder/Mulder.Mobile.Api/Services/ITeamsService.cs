using System.Collections.Generic;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Services
{
    public interface ITeamsService
    {
        TeamDetailsInfo GetTeam(int id);
        List<TeamInfo> GetTeams();
    }
}