using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class MatchesService : IMatchesService
    {
        private MulderContext Context { get; set; }

        public MatchesService(MulderContext context)
        {
            this.Context = context;
        }

        public List<MatchInfo> GetMatches()
        {
            var matches = this.Context.Matches.Select(x => new MatchInfo()
            {
                Id = x.Id.ToString(),
                Location = x.Location,
                Year = x.Year.ToString(),
                SocreInfo = this.GetScoreInfo(x.MatchesScore.Select(y => y).ToList())
            }).ToList();

            return matches;
        }

        private ScoreInfo GetScoreInfo(List<MatchesScore> matchesScore)
        {
            var scoreInfo = new ScoreInfo()
            {
                Team1Id = matchesScore[0]?.TeamId.ToString(),
                Team1HalfTimeScore = matchesScore[0]?.HalfTimeScore.ToString(),
                Team1Score = matchesScore[0]?.FullTimeScore.ToString(),
                Team2Id = matchesScore[1]?.TeamId.ToString(),
                Team2HalfTimeScore = matchesScore[1]?.HalfTimeScore.ToString(),
                Team2Score = matchesScore[1]?.FullTimeScore.ToString()
            };
            return scoreInfo;
        }

    }
}
