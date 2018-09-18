using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class Spectators
    {
        public Spectators()
        {
            MatchesSpectators = new HashSet<MatchesSpectators>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<MatchesSpectators> MatchesSpectators { get; set; }
    }
}
