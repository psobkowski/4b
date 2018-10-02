namespace Mulder.Mobile.Api.Domain
{
    public class PlayerMatchStats
    {
        public int MatchId { get; set; }
        public string MatchYear { get; set; }
        public int Goals { get; set; }
        public int MatchGoals { get; set; }
        public MatchResult MatchResult { get; set; }
        public bool YellowCard { get; set; }
        public bool ManOfTheMatch { get; set; }
    }
}
