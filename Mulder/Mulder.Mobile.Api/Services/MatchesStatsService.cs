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
            var matches = this.Context.Matches
                .Include(ms => ms.MatchesScore)
                .Select(m => new MatchStatsInfo
                {
                    Id = m.Id,
                    Location = m.Location,
                    Year = m.Year.ToString(),
                    ScoreInfo = new ScoreInfo(m.MatchesScore.Select(si => si)),
                    Stats = m.MatchesScore.Sum(s => s.FullTimeScore)
                }).Where(g => g.Stats > 0).OrderByDescending(o => o.Stats).ToList();

            return matches;
        }

        public List<MatchStatsInfo> TopSpectators()
        {
            var matches = this.Context.Matches
                .Include(ms => ms.MatchesSpectators)
                .Select(m => new MatchStatsInfo
                {
                    Id = m.Id,
                    Location = m.Location,
                    Year = m.Year.ToString(),
                    ScoreInfo = new ScoreInfo(m.MatchesScore.Select(si => si)),
                    Stats = m.MatchesSpectators.Count()
                }).Where(g => g.Stats > 0).OrderByDescending(o => o.Stats).ToList();

            return matches;
        }

    }
}
