using System.Collections.Generic;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Services
{
    public interface IStatsService
    {
        List<PlayerGoalsInfo> TopScorers();
    }
}