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
            var matchesSql = this.Context.MatchesScore
                .GroupBy(x => new { x.MatchId, x.Match.Location, x.Match.Year, x.TeamId, x.HalfTimeScore, x.FullTimeScore },
                (key, gr) => new { key.MatchId, key.Location, key.Year, key.TeamId, key.HalfTimeScore, key.FullTimeScore })
                .ToList();

            var matches = matchesSql.GroupBy(x => new { x.MatchId, x.Location, x.Year },
               (key, gr) => new MatchInfo
               {
                   Id = key.MatchId,
                   Location = key.Location,
                   Year = key.Year.ToString(),
                   ScoreInfo = gr.Select(si => new ScoreInfo
                   {
                       TeamId = si.TeamId,
                       HalfTimeScore = si.HalfTimeScore,
                       FullTimeScore = si.FullTimeScore
                   }).ToList()
               })
               .ToList();

            return matches;
        }

        public MatchDetailsInfo GetMatch(int matchId)
        {
            var spectators = this.GetSpectators(matchId);
            var players = this.GetPlayers(matchId);

            var matchesSql = this.Context.MatchesScore
                .Where(x => x.MatchId == matchId)
                .GroupBy(x => new { x.MatchId, x.Match.Location, x.Match.Year, x.Match.Address, x.Match.Date, x.TeamId, x.HalfTimeScore, x.FullTimeScore },
                    (key, gr) => new { key.MatchId, key.Location, key.Year, key.Address, key.Date, key.TeamId, key.HalfTimeScore, key.FullTimeScore })
                .ToList();

            var match = matchesSql.GroupBy(x => new { x.MatchId, x.Location, x.Address, x.Date, x.Year },
               (key, gr) => new MatchDetailsInfo
               {
                   Id = key.MatchId,
                   Location = key.Location,
                   Year = key.Year.ToString(),
                   Address = key.Address,
                   Date = key.Date,
                   ScoreInfo = gr.Select(si => new ScoreInfo
                   {
                       TeamId = si.TeamId,
                       HalfTimeScore = si.HalfTimeScore,
                       FullTimeScore = si.FullTimeScore
                   }).ToList(),
                   Players = players,
                   Spectators = spectators
               })
               .Single();          

            return match;
        }

        private List<SpectatorInfo> GetSpectators(int matchId)
        {
            var spectators = this.Context.MatchesSpectators
                .Where(x => x.MatchId == matchId)
                .Select(s => new SpectatorInfo
                {
                    Id = s.Id,
                    Name = s.Spectator.Name
                }).ToList();

            return spectators;
        }

        private List<PlayerMatchInfo> GetPlayers(int matchId)
        {
            var players = this.Context.MatchesLineUp
                .Where(x => x.MatchId == matchId)
                .Select(p => new PlayerMatchInfo
                {
                    PlayerId = p.PlayerId,
                    PlayerNick = p.Player.NickName,
                    TeamId = p.TeamId,
                    RedCard = p.RedCard,
                    YellowCard = p.YellowCard,
                    ManOfTheMatch = p.ManOfTheMatch,
                    Goals = p.PlayersScore.Select(g => new GoalInfo
                    {
                        Minute = g.Minute != null ? g.Minute.ToString() : string.Empty
                    }).ToList()
                }).ToList();

            return players;
        }


    }
}
