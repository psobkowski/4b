using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Domain
{
    public class PlayerDetailsInfo : PlayerInfo
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
        public bool Guest { get; set; }
        public string ManOfTheMatch { get; set; }
        public string YellowCards { get; set; }
        public string Wins { get; set; }
        public string Matches { get; set; }
        public string Goals { get; set; }
        public string Ratio { get; set; }
        public PlayerMatchStats BestGame { get; set; }
        public List<PlayerMatchStats> PlayerMatchStats { get; set; }
    }
}
