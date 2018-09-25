using Mulder.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Domain
{
    public class ScoreInfo
    {
        public List<ScoreDetailsInfo> ScoreDetailsInfo { get; set; }

        public ScoreInfo()
        { }

        public ScoreInfo(IEnumerable<MatchesScore> matchesScore)
        {
            this.ScoreDetailsInfo = new List<ScoreDetailsInfo>
            {
                new ScoreDetailsInfo
                {
                    TeamId = matchesScore.ElementAt(0).TeamId,
                    HalfTimeScore = matchesScore.ElementAt(0).HalfTimeScore,
                    Score = matchesScore.ElementAt(0).FullTimeScore,
                },
                new ScoreDetailsInfo
                {
                    TeamId = matchesScore.ElementAt(1).TeamId,
                    HalfTimeScore = matchesScore.ElementAt(1).HalfTimeScore,
                    Score = matchesScore.ElementAt(1).FullTimeScore,
                }
            };
        }
    }
}
