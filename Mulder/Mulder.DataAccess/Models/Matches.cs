using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class Matches
    {
        public Matches()
        {
            MatchesLineUp = new HashSet<MatchesLineUp>();
            MatchesScore = new HashSet<MatchesScore>();
            MatchesSpectators = new HashSet<MatchesSpectators>();
        }

        public int Id { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public DateTime? Date { get; set; }
        public short Year { get; set; }

        public ICollection<MatchesLineUp> MatchesLineUp { get; set; }
        public ICollection<MatchesScore> MatchesScore { get; set; }
        public ICollection<MatchesSpectators> MatchesSpectators { get; set; }
    }
}
