using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class MatchesLineUp
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public bool YellowCard { get; set; }
        public bool RedCard { get; set; }
        public bool ManOfTheMatch { get; set; }

        public Matches Match { get; set; }
        public Players Player { get; set; }
        public Teams Team { get; set; }
    }
}
