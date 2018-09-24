using Mulder.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Mulder.Mobile.Api.Domain;
using System;
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
                Id = x.Id.ToString(),
                Location = x.Location,
                Year = x.Year.ToString(),
                ScoreInfo = this.GetScoreInfo(x.MatchesScore)
            }).ToList();

            return matches;
        }

        public MatchDetailsInfo GetMatch(string matchId)
        {
            var match = this.Context.Matches
                .Where(x => x.Id == Convert.ToInt32(matchId))
                .Include(ml => ml.MatchesLineUp)
                .ThenInclude(p => p.Player)
                .Select(m => new MatchDetailsInfo
                {
                    Id = m.Id.ToString(),
                    Location = m.Location,
                    Address = m.Address,
                    Date = m.Date,
                    Year = m.Year.ToString(),
                    ScoreInfo = this.GetScoreInfo(m.MatchesScore),
                    Players = m.MatchesLineUp.Select(p => new PlayerMatchInfo
                    {
                        PlayerId = p.PlayerId.ToString(),
                        PlayerNick = p.Player.NickName,
                        TeamId = p.TeamId.ToString(),
                        RedCard = p.RedCard,
                        YellowCard = p.YellowCard,
                        ManOfTheMatch = p.ManOfTheMatch,
                        Goals = this.GetGoals(p.PlayersScore)
                    }).ToList(),
                    Spectators = m.MatchesSpectators.Select(s => new SpectatorInfo
                    {
                        Id = s.Id.ToString(),
                        Name = s.Spectator.Name
                    }).ToList()
                }).SingleOrDefault();

            return match;
        }

        private ScoreInfo GetScoreInfo(IEnumerable<MatchesScore> matchesScore)
        {
            var scoreInfo = new ScoreInfo
            {
                Team1Id = matchesScore.ElementAt(0)?.TeamId.ToString(),
                Team1HalfTimeScore = matchesScore.ElementAt(0)?.HalfTimeScore.ToString(),
                Team1Score = matchesScore.ElementAt(0)?.FullTimeScore.ToString(),
                Team2Id = matchesScore.ElementAt(1)?.TeamId.ToString(),
                Team2HalfTimeScore = matchesScore.ElementAt(1)?.HalfTimeScore.ToString(),
                Team2Score = matchesScore.ElementAt(1)?.FullTimeScore.ToString()
            };
            return scoreInfo;
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
