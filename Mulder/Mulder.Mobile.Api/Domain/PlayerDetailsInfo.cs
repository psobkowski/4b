using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Domain
{
    public class PlayerDetailsInfo : PlayerInfo
    {
        public string Name { get; set; }
        public bool Guest { get; set; }
        public string ManOfTheMatch { get; set; }
        public string YellowCards { get; set; }
        public string Wins { get; set; }
        public List<PlayerMatchStats> PlayerMatchStats { get; set; }

        [JsonIgnore]
        private int? _MatchesCount { get; set; }
        [JsonIgnore]
        public int MatchesCount
        {
            get
            {
                return (int)(this._MatchesCount = this._MatchesCount ?? this.PlayerMatchStats.Count());
            }
        }

        public string Matches
        {
            get { return this.MatchesCount.ToString(); }
        }

        [JsonIgnore]
        private int? _GoalsSum { get; set; }
        [JsonIgnore]
        public int GoalsSum
        {
            get
            {
                return (int)(this._GoalsSum = this._GoalsSum ?? this.PlayerMatchStats.Sum(x => x.Goals));
            }
        }

        public string Goals
        {
            get { return this.GoalsSum.ToString(); }
        }

        public string GoalsPerMatch
        {
            get
            {
                return string.Format("{0:0.0}", this.MatchesCount > 0 ? (double)this.GoalsSum / this.MatchesCount : 0);
            }
        }
    }
}
