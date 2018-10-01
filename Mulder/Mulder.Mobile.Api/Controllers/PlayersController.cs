using Microsoft.AspNetCore.Mvc;
using Mulder.Mobile.Api.Resolvers;
using Mulder.Mobile.Api.Domain;
using Mulder.Mobile.Api.Services;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Mulder.Mobile.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PlayersController  : Controller
    {
        private IPlayersService PlayersService { get; set; }

        private JsonSerializerSettings JsonSettings { get; set; }

        public PlayersController(IPlayersService playersService)
        {
            this.PlayersService = playersService;
            this.JsonSettings = new JsonSerializerSettings { ContractResolver = BaseFirstContractResolver.Instance };
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                var player = this.PlayersService.GetPlayer(id);
                return Json(new JsonMobileResult(player), this.JsonSettings);
            }
            catch (Exception ex)
            {
                string errorMessage = "Cannot get player details info. Please try again later.";
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }
    }
}
