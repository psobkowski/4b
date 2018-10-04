using System.Collections.Generic;

namespace Mulder.Mobile.Api.Domain
{
    public class MatchInfo
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Year { get; set; }
        public List<ScoreInfo> ScoreInfo { get; set; }
    }
}
