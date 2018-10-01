using Microsoft.AspNetCore.Mvc;
using Mulder.Mobile.Api.ContractResolvers;
using Mulder.Mobile.Api.Domain;
using Mulder.Mobile.Api.Services;
using Newtonsoft.Json;
using System;

namespace Mulder.Mobile.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlayersStatsController  : Controller
    {
        private IPlayersStatsService PlayersStatsService { get; set; }

        private JsonSerializerSettings JsonSettings { get; set; }

        public PlayersStatsController(IPlayersStatsService playersStatsService)
        {
            this.PlayersStatsService = playersStatsService;
            this.JsonSettings = new JsonSerializerSettings { ContractResolver = BaseFirstContractResolver.Instance };
        }

        [HttpGet("TopScorers")]
        public JsonResult TopScorers()
        {
            try
            {
                var players = this.PlayersStatsService.TopScorers();
                return Json(new JsonMobileResult(players), this.JsonSettings);
            }
           catch (Exception ex)
            {
                string errorMessage = "Cannot get top scorers info. Please try again later.";
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }

        [HttpGet("TopCaps")]
        public JsonResult TopCaps()
        {
            try
            {
                var players = this.PlayersStatsService.TopCaps();
                return Json(new JsonMobileResult(players), this.JsonSettings);
            }
            catch (Exception ex)
            {
                string errorMessage = "Cannot get top caps info. Please try again later.";
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }

        [HttpGet("TopMvps")]
        public JsonResult TopMvps()
        {
            try
            {
                var players = this.PlayersStatsService.TopMvps();
                return Json(new JsonMobileResult(players), this.JsonSettings);
            }
            catch (Exception ex)
            {
                string errorMessage = "Cannot get top man of the matach info. Please try again later.";
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }
    }
}
