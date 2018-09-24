using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mulder.Mobile.Api.Domain
{
    public class PlayerInfo
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Number { get; set; }
        public string Nick { get; set; }
    }
}
