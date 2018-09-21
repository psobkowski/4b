using System.Collections.Generic;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Services
{
    public interface IMatchesService
    {
        List<MatchInfo> GetMatches();
        MatchDetailsInfo GetMatch(string id);
    }
}