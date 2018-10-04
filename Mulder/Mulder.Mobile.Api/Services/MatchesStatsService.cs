using Microsoft.EntityFrameworkCore;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class MatchesStatsService : IMatchesStatsService
    {
        private MulderContext Context { get; set; }

        public MatchesStatsService(MulderContext context)
        {
            this.Context = context;
        }

        public List<MatchStatsInfo> TopScore()
        {
            var sql = this.Context.MatchesScore
                .GroupBy(x => new { x.MatchId, x.Match.Location, x.Match.Year, x.TeamId, x.HalfTimeScore, x.FullTimeScore },
                (key, gr) => new { key.MatchId, key.Location, key.Year, key.TeamId, key.HalfTimeScore, key.FullTimeScore })
                .ToList();

            var matches = sql.GroupBy(x => new { x.MatchId, x.Location, x.Year },
               (key, gr) => new MatchStatsInfo
               {
                   Id = key.MatchId,
                   Location = key.Location,
                   Year = key.Year.ToString(),
                   Stats = gr.Sum(f => f.FullTimeScore),
                   ScoreInfo = gr.Select(si => new ScoreInfo
                   {
                       TeamId = si.TeamId,
                       HalfTimeScore = si.HalfTimeScore,
                       FullTimeScore = si.FullTimeScore
                   }).ToList()
               })
               .OrderByDescending(o => o.Stats)
               .ToList();

            return matches;
        }

        public List<MatchStatsInfo> TopSpectators()
        {
            var sql = this.Context.MatchesSpectators
                .Join(this.Context.MatchesScore, x => x.MatchId, y => y.MatchId, (x, y) => new { x.MatchId, x.Match.Location, x.Match.Year, y.TeamId, y.HalfTimeScore, y.FullTimeScore })
                .GroupBy(x => new { x.MatchId, x.Location, x.Year, x.TeamId, x.HalfTimeScore, x.FullTimeScore },
                    (key, gr) => new
                    {
                        key.MatchId,
                        key.Location,
                        key.Year,
                        Spectators = gr.Count(),
                        key.TeamId,
                        key.HalfTimeScore,
                        key.FullTimeScore
                    })
                .ToList();

            var matches = sql.GroupBy(x => new { x.MatchId, x.Location, x.Year, x.Spectators },
                (key, gr) => new MatchStatsInfo
                {
                    Id = key.MatchId,
                    Location = key.Location,
                    Year = key.Year.ToString(),
                    Stats = key.Spectators,
                    ScoreInfo = gr.Select(si => new ScoreInfo
                    {
                        TeamId = si.TeamId,
                        HalfTimeScore = si.HalfTimeScore,
                        FullTimeScore = si.FullTimeScore
                    }).ToList()
                })
                .OrderByDescending(o => o.Stats)
                .ToList();

            return matches;
        }
    
    }
}
