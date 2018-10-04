using Microsoft.EntityFrameworkCore;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class MatchesService : IMatchesService
    {
        private MulderContext Context { get; set; }

        public MatchesService(MulderContext context)
        {
            this.Context = context;
        }

        public List<MatchInfo> GetMatches()
        {
            var matches = this.Context.Matches.Select(x => new MatchInfo
            {
                Id = x.Id,
                Location = x.Location,
                Year = x.Year.ToString(),
                ScoreInfo = x.MatchesScore.Select(ms => new ScoreInfo
                    {
                        TeamId = ms.TeamId,
                        FullTimeScore = ms.FullTimeScore,
                        HalfTimeScore = ms.HalfTimeScore,
                    }).ToList()
            }).ToList();

            return matches;
        }

        public MatchDetailsInfo GetMatch(int matchId)
        {
            var match = this.Context.Matches
                .Where(x => x.Id == matchId)
                .Include(ml => ml.MatchesLineUp)
                .ThenInclude(p => p.Player)
                .Select(m => new MatchDetailsInfo
                {
                    Id = m.Id,
                    Location = m.Location,
                    Address = m.Address,
                    Date = m.Date,
                    Year = m.Year.ToString(),
                    ScoreInfo = m.MatchesScore.Select(ms => new ScoreInfo
                    {
                        TeamId = ms.TeamId,
                        FullTimeScore = ms.FullTimeScore,
                        HalfTimeScore = ms.HalfTimeScore,
                    }).ToList(),
                    Players = m.MatchesLineUp.Select(p => new PlayerMatchInfo
                    {
                        PlayerId = p.PlayerId,
                        PlayerNick = p.Player.NickName,
                        TeamId = p.TeamId,
                        RedCard = p.RedCard,
                        YellowCard = p.YellowCard,
                        ManOfTheMatch = p.ManOfTheMatch,
                        Goals = this.GetGoals(p.PlayersScore)
                    }).ToList(),
                    Spectators = m.MatchesSpectators.Select(s => new SpectatorInfo
                    {
                        Id = s.Id,
                        Name = s.Spectator.Name
                    }).ToList()
                }).Single();

            return match;
        }

        private List<GoalInfo> GetGoals(IEnumerable<PlayersScore> playersScore)
        {
            var goals = playersScore.Select(g => new GoalInfo
            {
                Minute = g.Minute != null ? g.Minute.ToString() : string.Empty
            }).ToList();

            return goals;
        }
    }
}
