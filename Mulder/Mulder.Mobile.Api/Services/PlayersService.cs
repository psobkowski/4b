using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class PlayersService : IPlayersService
    {
        private MulderContext Context { get; set; }

        public PlayersService(MulderContext context)
        {
            this.Context = context;
        }

        public PlayerDetailsInfo GetPlayer(int playerId)
        {
            var playerMatchStats = this.GetPlayerMatchStats(playerId);
            var matches = playerMatchStats.Count();
            var goals = playerMatchStats.Sum(x => x.Goals);
            var ratio = string.Format("{0:0.0}", matches > 0 ? (double)goals / matches : 0);

            var p = this.Context.Players
                .Where(x => x.Id == playerId)
                .Select(x => new
                {
                    x.Id,
                    x.CurrentTeamId,
                    x.Number,
                    x.NickName,
                    x.Name,
                    x.OnLoan
                }).Single();

            var player = new PlayerDetailsInfo
            {
                Id = p.Id,
                TeamId = p.CurrentTeamId,
                Number = p.Number,
                Nick = p.NickName,
                Name = p.Name,
                Guest = p.OnLoan ?? false,
                ManOfTheMatch = playerMatchStats.Count(x => x.ManOfTheMatch).ToString(),
                YellowCards = playerMatchStats.Count(x => x.YellowCard).ToString(),
                Wins = playerMatchStats.Count(x => x.MatchResult == MatchResult.Win).ToString(),
                Matches = matches.ToString(),
                Goals = goals.ToString(),
                Ratio = ratio,
                BestGame = playerMatchStats.OrderByDescending(g => g.Goals).ThenByDescending(g => g.MatchResult).ThenByDescending(g => g.MatchGoals).First(),
                PlayerMatchStats = playerMatchStats
            };

            return player;
        }

        private List<PlayerMatchStats> GetPlayerMatchStats(int playerId)
        {
            var query = (from pss in (from u in this.Context.MatchesLineUp
                             join s in this.Context.MatchesScore on u.MatchId equals s.MatchId
                             join ps in this.Context.PlayersScore on u.Id equals ps.MatchesLineUpId into r
                             from pss in r.DefaultIfEmpty()
                             where u.PlayerId == playerId
                             select new
                             {
                                 s.MatchId,
                                 s.Match.Year,
                                 u.Player.CurrentTeamId,
                                 s.TeamId,
                                 s.HalfTimeScore,
                                 s.FullTimeScore,
                                 u.ManOfTheMatch,
                                 u.YellowCard,
                                 Goals = pss == null ? 0 : 1
                             })
                         group pss by new { pss.MatchId, pss.Year, pss.CurrentTeamId, pss.TeamId, pss.HalfTimeScore, pss.FullTimeScore, pss.ManOfTheMatch, pss.YellowCard } into gr
                         select new { Pss = gr.Key, Goals = gr.Sum(x => x.Goals) }).ToList();
                            
            var playerMatchStats = query.GroupBy(x => new { x.Pss.MatchId, x.Pss.Year, x.Pss.CurrentTeamId, x.Pss.ManOfTheMatch, x.Pss.YellowCard, x.Goals },
            (key, gr) => new PlayerMatchStats
            {
                MatchId = key.MatchId,
                MatchYear = key.Year.ToString(),
                ManOfTheMatch = key.ManOfTheMatch,
                YellowCard = key.YellowCard,
                MatchGoals = gr.Sum(gg => gg.Pss.FullTimeScore),
                Goals = key.Goals,
                MatchResult = MatchResultHelper.Result(gr.Select(si => new ScoreInfo
                {
                    TeamId = si.Pss.TeamId,
                    HalfTimeScore = si.Pss.HalfTimeScore,
                    FullTimeScore = si.Pss.FullTimeScore
                }).ToList(), key.CurrentTeamId)

            }).ToList();

            return playerMatchStats;
        }

    }
}
