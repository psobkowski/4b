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
           var query =  (from pss in (from u in this.Context.MatchesLineUp
                          join ps in this.Context.PlayersScore on u.Id equals ps.MatchesLineUpId into r
                          from pss in r.DefaultIfEmpty()
                          select new
                          {
                              u.MatchId,
                              u.Player.Id,
                              u.Player.NickName,
                              u.Player.Number,
                              Goals = pss == null ? 0 : 1
                          })
            group pss by new { pss.MatchId, pss.Id, pss.NickName, pss.Number } into gr
            select new { Pss = gr.Key, Goals = gr.Sum(x => x.Goals) }).ToList();

            var players = query
            .GroupBy(x => new {x.Pss.Id, x.Pss.NickName, x.Pss.Number }, (key, gr) => new PlayerStatsInfo
            {
                Id = key.Id,
                Nick = key.NickName,
                Number = key.Number,
                Stats = gr.Sum(x => x.Goals),
                Ratio = string.Format("{0:0.0}", gr.Average(x => x.Goals))
            })
            .Where(x => x.Stats > 0)
            .OrderByDescending(o => o.Stats).ThenByDescending(o => o.Ratio)
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

    }
}
