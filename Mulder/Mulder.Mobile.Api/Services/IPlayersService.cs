using System.Collections.Generic;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Services
{
    public interface IPlayersService
    {
        PlayerDetailsInfo GetPlayer(int playerId);
        List<PlayerInfo> GetPlayers();
    }
}