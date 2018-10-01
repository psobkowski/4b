using System.Collections.Generic;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Services
{
    public interface IPlayersStatsService
    {
        List<PlayerStatsInfo> TopScorers();
        List<PlayerStatsInfo> TopCaps();
        List<PlayerStatsInfo> TopMvps();
    }
}