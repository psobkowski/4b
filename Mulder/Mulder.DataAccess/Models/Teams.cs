using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class Teams
    {
        public Teams()
        {
            MatchesLineUp = new HashSet<MatchesLineUp>();
            MatchesScore = new HashSet<MatchesScore>();
            Players = new HashSet<Players>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<MatchesLineUp> MatchesLineUp { get; set; }
        public ICollection<MatchesScore> MatchesScore { get; set; }
        public ICollection<Players> Players { get; set; }
    }
}
