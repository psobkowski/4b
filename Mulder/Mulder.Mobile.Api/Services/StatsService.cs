using Microsoft.EntityFrameworkCore;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class StatsService : IStatsService
    {
        private MulderContext Context { get; set; }

        public StatsService(MulderContext context)
        {
            this.Context = context;
        }

        public List<PlayerGoalsInfo> TopScorers()
        {
            var players = this.Context.Players
                .Include(ml => ml.MatchesLineUp)
                .Select(p => new PlayerGoalsInfo
                {
                    Id = p.Id,
                    Nick = p.NickName,
                    Number = p.Number,
                    Goals = p.MatchesLineUp.Sum(ml => ml.PlayersScore.Count)
                }).Where(g => g.Goals > 0).OrderByDescending(o => o.Goals).ToList();

            return players;
        }

    }
}
