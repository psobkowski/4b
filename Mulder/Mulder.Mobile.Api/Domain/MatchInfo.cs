namespace Mulder.Mobile.Api.Domain
{
    public class MatchInfo
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Year { get; set; }
        public ScoreInfo ScoreInfo { get; set; }
    }
}
