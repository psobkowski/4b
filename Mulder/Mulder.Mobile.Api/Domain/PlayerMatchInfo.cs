using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mulder.Mobile.Api.Domain
{
    public class PlayerMatchInfo
    {
        public string PlayerId { get; set; }
        public string PlayerNick { get; set; }
        public string TeamId { get; set; }
        public List<GoalInfo> Goals { get; set; }
        public bool ManOfTheMatch { get; set; }
        public bool YellowCard { get; set; }
        public bool RedCard { get; set; }
    }
}
