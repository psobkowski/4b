using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class PlayersScore
    {
        public int Id { get; set; }
        public int MatchesLineUpId { get; set; }
        public short? Minute { get; set; }
    }
}
