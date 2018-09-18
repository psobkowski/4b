using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class MatchesSpectators
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int SpectatorId { get; set; }

        public Matches Match { get; set; }
        public Spectators Spectator { get; set; }
    }
}
