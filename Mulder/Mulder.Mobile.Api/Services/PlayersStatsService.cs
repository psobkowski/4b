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
            var players = this.Context.PlayersScore
            .GroupBy(x => new { x.MatchesLineUp.PlayerId, x.MatchesLineUp.Player.NickName, x.MatchesLineUp.Player.Number },
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

        public List<PlayerStatsInfo> TopCaps()
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

    }
}
