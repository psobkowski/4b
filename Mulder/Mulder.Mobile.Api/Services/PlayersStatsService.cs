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
            var scorers = this.Context.PlayersScore
            .GroupBy(x => new { x.MatchesLineUp.PlayerId, x.MatchesLineUp.Player.NickName, x.MatchesLineUp.Player.Number },
            (key, gr) => new
            {
                Id = key.PlayerId,
                Stats = gr.Count()
            })
            .AsEnumerable();

            var caps = this.GetCaps();

            var players = scorers.Join(caps, p => p.Id, c => c.Id,
            (p, c) => new PlayerStatsInfo
            {
                Id = p.Id,
                Nick = c.Nick,
                Number = c.Number,
                Stats = p.Stats,
                Ratio = string.Format("{0:0.0}", c.Stats > 0 ? (double)p.Stats / c.Stats : 0)
            })
            .OrderByDescending(o => o.Stats).ThenByDescending(o => o.Ratio)
            .ToList();

            return players;
        }

        public List<PlayerStatsInfo> TopCaps()
        {
            var players = this.GetCaps()
            .OrderByDescending(o => o.Stats).ThenBy(o => o.Nick)
            .ToList();

            return players;
        }

        public List<PlayerStatsInfo> TopMvps()
        {
            var players = this.Context.MatchesLineUp
                .Where(x => x.ManOfTheMatch)
                .GroupBy(x => new { x.PlayerId, x.Player.NickName, x.Player.Number },
                (key, gr) => new PlayerStatsInfo
                {
                    Id = key.PlayerId,
                    Nick = key.NickName,
                    Number = key.Number,
                    Stats = gr.Count()
                })
                .OrderByDescending(o => o.Stats).ThenBy(o => o.Nick)
                .ToList();

            return players;
        }

        private IEnumerable<PlayerStatsInfo> GetCaps()
        {
            var players = this.Context.MatchesLineUp
            .GroupBy(x => new { x.PlayerId, x.Player.NickName, x.Player.Number },
            (key, gr) => new PlayerStatsInfo
            {
                Id = key.PlayerId,
                Nick = key.NickName,
                Number = key.Number,
                Stats = gr.Count()
            })
            //.OrderByDescending(o => o.Stats).ThenBy(o => o.Nick)
            .AsEnumerable();

            return players;
        }

    }
}
