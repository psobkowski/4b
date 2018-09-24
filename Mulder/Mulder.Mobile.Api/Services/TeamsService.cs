using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System;
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

        public TeamInfo GetTeam(int teamId)
        {
            var team = this.Context.Teams.Where(x => x.Id == teamId)
                .Select(t => new TeamInfo
                {
                    Id = t.Id,
                    Name = t.Name
                }).SingleOrDefault();

            return team;
        }
    }
}
