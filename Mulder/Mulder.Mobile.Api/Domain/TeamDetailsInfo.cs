using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Domain
{
    public class TeamDetailsInfo : TeamInfo
    {
        public string Goals
        {
            get
            {
                return this.Matches.Sum(m => m.ScoreInfo.ScoreDetailsInfo.Single(x => x.TeamId == this.Id).Score).ToString();
            }
        }

        public string Wins
        {
            get
            {
                return this.Matches.Count(m => m.ScoreInfo.ScoreDetailsInfo.Single(x => x.TeamId == this.Id).Score
                                             > m.ScoreInfo.ScoreDetailsInfo.Single(x => x.TeamId != this.Id).Score).ToString();
            }
        }

        public string Losses
        {
            get
            {
                return this.Matches.Count(m => m.ScoreInfo.ScoreDetailsInfo.Single(x => x.TeamId == this.Id).Score
                                             < m.ScoreInfo.ScoreDetailsInfo.Single(x => x.TeamId != this.Id).Score).ToString();
            }
        }
        public string Draws
        {
            get
            {
                return this.Matches.Count(m => m.ScoreInfo.ScoreDetailsInfo.Single(x => x.TeamId == this.Id).Score
                                            == m.ScoreInfo.ScoreDetailsInfo.Single(x => x.TeamId != this.Id).Score).ToString();
            }
        }

        public string YellowCards { get; set; }
        public string ManOfTheMatch { get; set; }
        public List<PlayerInfo> Players { get; set; }

        [JsonIgnore]
        public List<MatchInfo> Matches { get; set; }
    }
}
