using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Domain
{
    public class TeamDetailsInfo : TeamInfo
    {
        public string Goals { get; set; }
        public string Wins { get; set; }
        public string Losses { get; set; }
        public string Draws { get; set; }
        public string YellowCards { get; set; }
        public string ManOfTheMatch { get; set; }
        public MatchInfo BestGame { get; set; }
        public List<PlayerInfo> Players { get; set; }
    }
}
