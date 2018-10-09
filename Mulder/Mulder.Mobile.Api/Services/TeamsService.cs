using Microsoft.EntityFrameworkCore;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class TeamsService : ITeamsService
    {
        private MulderContext Context { get; set; }

        public TeamsService(MulderContext context)
        {
            this.Context = context;
        }

        public List<TeamInfo> GetTeams()
        {
            var teams = this.Context.Teams.Select(t => new TeamInfo
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return teams;
        }

        public TeamDetailsInfo GetTeam(int teamId)
        {
            var matches = this.GetTeamMatches(teamId);
            var players = this.GetTeamPlayers(teamId);

            var query = this.Context.MatchesLineUp
            .Where(x => x.TeamId == teamId)
            .Select(x => new
            {
                x.TeamId,
                x.Team.Name,
                x.YellowCard,
                x.ManOfTheMatch
            }).AsEnumerable();

            var team = query.GroupBy(x => new { x.TeamId, x.Name },
            (key, gr) => new TeamDetailsInfo
            {
                Id = key.TeamId,
                Name = key.Name,
                YellowCards = gr.Count(y => y.YellowCard).ToString(),
                ManOfTheMatch = gr.Count(mm => mm.ManOfTheMatch).ToString(),
                BestGame = matches.OrderByDescending(i => MatchResultHelper.Diff(i.ScoreInfo, key.TeamId))
                                  .ThenByDescending(i => i.ScoreInfo.Sum(g => g.FullTimeScore)).First(),
                Losses = matches.Count(i => MatchResultHelper.Diff(i.ScoreInfo, key.TeamId) < 0).ToString(),
                Draws = matches.Count(i => MatchResultHelper.Diff(i.ScoreInfo, key.TeamId) == 0).ToString(),
                Wins = matches.Count(i => MatchResultHelper.Diff(i.ScoreInfo, key.TeamId) > 0).ToString(),
                Goals = matches.Sum(m => m.ScoreInfo.First(x => x.TeamId == teamId).FullTimeScore).ToString(),
                Players = players
            }).Single();

            return team;
        }

        private List<PlayerInfo> GetTeamPlayers(int teamId)
        {
            var players = this.Context.Players
            .Where(x => x.CurrentTeamId == teamId)
            .Select(p => new PlayerInfo
            {
                Id = p.Id,
                Number = p.Number,
                Nick = p.NickName
            }).ToList();

            return players;
        }

        private List<MatchInfo> GetTeamMatches(int teamId)
        {
            var query = this.Context.Matches
            .Where(m => m.MatchesScore.Any(t => t.TeamId == teamId))
            .SelectMany(x => x.MatchesScore, (x,c) =>  new
            {
                c.MatchId,
                x.Location,
                x.Year,
                c.TeamId,
                c.HalfTimeScore,
                c.FullTimeScore
            })

            .AsEnumerable();

            var matches = query.GroupBy(x => new
            {
                x.MatchId,
                x.Location,
                x.Year
            },
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
    }
}
