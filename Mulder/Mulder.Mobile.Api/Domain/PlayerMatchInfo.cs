using System.Collections.Generic;

namespace Mulder.Mobile.Api.Domain
{
    public class PlayerMatchInfo
    {
        public int PlayerId { get; set; }
        public string PlayerNick { get; set; }
        public int TeamId { get; set; }
        public List<GoalInfo> Goals { get; set; }
        public bool ManOfTheMatch { get; set; }
        public bool YellowCard { get; set; }
        public bool RedCard { get; set; }
    }
}
