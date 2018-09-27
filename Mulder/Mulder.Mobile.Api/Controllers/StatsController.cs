using Microsoft.AspNetCore.Mvc;
using Mulder.Mobile.Api.ContractResolvers;
using Mulder.Mobile.Api.Domain;
using Mulder.Mobile.Api.Services;
using Newtonsoft.Json;
using System;

namespace Mulder.Mobile.Api.Controllers
{
    [Route("api/[controller]")]
    public class StatsController  : Controller
    {
        private IStatsService StatsService { get; set; }

        private JsonSerializerSettings JsonSettings { get; set; }

        public StatsController(IStatsService statsService)
        {
            this.StatsService = statsService;
            this.JsonSettings = new JsonSerializerSettings { ContractResolver = BaseFirstContractResolver.Instance };
        }

        [HttpGet("TopScorers")]
        public JsonResult TopScorers()
        {
            try
            {
                var players = this.StatsService.TopScorers();
                return Json(new JsonMobileResult(players), this.JsonSettings);
            }
           catch (Exception ex)
            {
                string errorMessage = "Cannot get top scorers info. Please try again later.";
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }
    }
}
