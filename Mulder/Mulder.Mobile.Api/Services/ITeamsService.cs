using System.Collections.Generic;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Services
{
    public interface ITeamsService
    {
        TeamInfo GetTeam(int id);
        List<TeamInfo> GetTeams();
    }
}