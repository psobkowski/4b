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
                }).Take(2).ToList();

            return teams;
        }

        public TeamDetailsInfo GetTeam(int teamId)
        {
            var team = this.Context.Teams
                .Where(x => x.Id == teamId)
                .Include(m => m.MatchesScore)
                .Select(t => new TeamDetailsInfo
                {
                    Id = t.Id,
                    Name = t.Name,
                    YellowCards = t.MatchesLineUp.Count(ml => ml.YellowCard).ToString(),
                    ManOfTheMatch = t.MatchesLineUp.Count(ml => ml.ManOfTheMatch).ToString(),
                    Matches = t.MatchesScore.Select(ml => new MatchInfo
                    {
                        Id = ml.MatchId,
                        Location = ml.Match.Location,
                        Year = ml.Match.Year.ToString(),
                        ScoreInfo = new ScoreInfo(ml.Match.MatchesScore)
                    }).ToList(),
                    Players = t.Players.Select(p => new PlayerInfo
                                {
                                    Id = p.Id,
                                    TeamId = p.CurrentTeamId,
                                    Number = p.Number,
                                    Nick = p.NickName
                                }).ToList()
                }).Single();

            return team;
        }
    }
}
