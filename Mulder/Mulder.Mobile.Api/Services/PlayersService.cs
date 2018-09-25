using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class PlayersService : IPlayersService
    {
        private MulderContext Context { get; set; }

        public PlayersService(MulderContext context)
        {
            this.Context = context;
        }

        public List<PlayerInfo> GetPlayers()
        {
            var players = this.Context.Players.Select(p => new PlayerInfo
            {
                Id = p.Id,
                TeamId = p.CurrentTeamId,
                Number = p.Number,
                Nick = p.NickName
            }).ToList();

            return players;
        }

        public PlayerDetailsInfo GetPlayer(int playerId)
        {
            var player = this.Context.Players
                .Where(x => x.Id == playerId)
                .Include(ml => ml.MatchesLineUp)
                .Select(p => new PlayerDetailsInfo
                {
                    Id = p.Id,
                    TeamId = p.CurrentTeamId,
                    Number = p.Number,
                    Nick = p.NickName,
                    Name = p.Name,
                    Guest = p.OnLoan ?? false,
                    PlayerMatchStats = p.MatchesLineUp.Select(pm => new PlayerMatchStats
                    {
                        MatchId = pm.MatchId,
                        MatchYear = pm.Match.Year.ToString(),
                        Goals = pm.PlayersScore.Count(),
                        MatchResult = MatchResultHelper.Result(pm.Match.MatchesScore.Single(ms => ms.TeamId == pm.TeamId).FullTimeScore,
                                                               pm.Match.MatchesScore.Single(ms => ms.TeamId != pm.TeamId).FullTimeScore),
                        ManOfTheMatch = pm.ManOfTheMatch,
                        YellowCard = pm.YellowCard
                    }).ToList()
                }).Single();

            return player;
        }

    }
}
