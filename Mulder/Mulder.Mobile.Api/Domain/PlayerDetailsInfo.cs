using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mulder.Mobile.Api.Domain
{
    public class PlayerDetailsInfo : PlayerInfo
    {
        public string Name { get; set; }
        public string Matches { get; set; }
        public string Goals { get; set; }
        public string Avg {get; set;}
    }
}
