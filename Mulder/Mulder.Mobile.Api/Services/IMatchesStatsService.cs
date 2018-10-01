using System.Collections.Generic;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Services
{
    public interface IMatchesStatsService
    {
        List<MatchStatsInfo> TopScore();
        List<MatchStatsInfo> TopSpectators();
    }
}