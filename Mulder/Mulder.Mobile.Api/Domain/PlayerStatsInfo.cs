using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mulder.Mobile.Api.Domain
{
    public class PlayerStatsInfo : PlayerInfo
    {
        public int Stats { get; set; }

        public string Ratio { get; set; }
    }
}
