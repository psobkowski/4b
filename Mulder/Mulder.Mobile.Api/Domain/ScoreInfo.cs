using Mulder.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Domain
{
    public class ScoreInfo
    {
        public int TeamId { get; set; }
        public short FullTimeScore { get; set; }
        public short HalfTimeScore { get; set; }
    }
}
