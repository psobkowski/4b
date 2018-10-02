using Microsoft.AspNetCore.Mvc;
using Mulder.Mobile.Api.Resolvers;
using Mulder.Mobile.Api.Domain;
using Mulder.Mobile.Api.Services;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Mulder.Mobile.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PlayersController  : Controller
    {
        private ILogger Logger { get; set; }
        private IPlayersService PlayersService { get; set; }
        private JsonSerializerSettings JsonSettings { get; set; }

        public PlayersController(ILogger<PlayersController> logger, IPlayersService playersService)
        {
            this.Logger = logger;
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
                this.Logger.LogError(ex, errorMessage);
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }
    }
}
