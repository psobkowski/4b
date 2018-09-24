using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Services
{
    public class PlayersService
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

        public List<PlayerInfo> GetPlayers(int teamId)
        {
            var players = this.Context.Players
                .Where(t => t.CurrentTeamId == teamId)
                .Select(p => new PlayerInfo
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
                .Where(p => p.Id == playerId)
                .Select(p => new PlayerDetailsInfo
                {
                    Id = p.Id,
                    TeamId = p.CurrentTeamId,
                    Number = p.Number,
                    Nick = p.NickName
                }).SingleOrDefault();

            return player;
        }

    }
}
