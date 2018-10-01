using Microsoft.EntityFrameworkCore;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class PlayersStatsService : IPlayersStatsService
    {
        private MulderContext Context { get; set; }

        public PlayersStatsService(MulderContext context)
        {
            this.Context = context;
        }

        public List<PlayerStatsInfo> TopScorers()
        {
            var players = this.Context.Players
                .Include(ml => ml.MatchesLineUp)
                .Select(p => new PlayerStatsInfo
                {
                    Id = p.Id,
                    Nick = p.NickName,
                    Number = p.Number,
                    Stats = p.MatchesLineUp.Sum(ml => ml.PlayersScore.Count)
                }).Where(g => g.Stats > 0).OrderByDescending(o => o.Stats).ToList();

            return players;
        }

        public List<PlayerStatsInfo> TopCaps()
        {
            var players = this.Context.Players
                .Include(ml => ml.MatchesLineUp)
                .Select(p => new PlayerStatsInfo
                {
                    Id = p.Id,
                    Nick = p.NickName,
                    Number = p.Number,
                    Stats = p.MatchesLineUp.Count()
                }).Where(g => g.Stats > 0).OrderByDescending(o => o.Stats).ToList();

            return players;
        }

        public List<PlayerStatsInfo> TopMvps()
        {
            var players = this.Context.Players
                .Include(ml => ml.MatchesLineUp)
                .Select(p => new PlayerStatsInfo
                {
                    Id = p.Id,
                    Nick = p.NickName,
                    Number = p.Number,
                    Stats = p.MatchesLineUp.Count(ml => ml.ManOfTheMatch)
                }).Where(g => g.Stats > 0).OrderByDescending(o => o.Stats).ToList();

            return players;
        }

    }
}
