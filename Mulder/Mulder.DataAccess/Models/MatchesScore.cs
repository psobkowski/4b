using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class MatchesScore
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public short HalfTimeScore { get; set; }
        public short FullTimeScore { get; set; }

        public Matches Match { get; set; }
        public Teams Team { get; set; }
    }
}
