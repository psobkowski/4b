using System;
using System.Collections.Generic;

namespace Mulder.Mobile.Api.Domain
{
    public class MatchDetailsInfo : MatchInfo
    {
        public string Address { get; set; }
        public DateTime? Date { get; set; }
        public List<PlayerMatchInfo> Players { get; set; }
        public List<SpectatorInfo> Spectators {get; set; }
    }
}
