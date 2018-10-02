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
    public class MatchesStatsController  : Controller
    {
        private ILogger Logger { get; set; }
        private IMatchesStatsService MatchesStatsService { get; set; }
        private JsonSerializerSettings JsonSettings { get; set; }

        public MatchesStatsController(ILogger<MatchesStatsController> logger, IMatchesStatsService matchesStatsService)
        {
            this.Logger = logger;
            this.MatchesStatsService = matchesStatsService;
            this.JsonSettings = new JsonSerializerSettings { ContractResolver = BaseFirstContractResolver.Instance };
        }

        [HttpGet("TopScore")]
        public JsonResult TopScore()
        {
            try
            {
                var matches = this.MatchesStatsService.TopScore();
                return Json(new JsonMobileResult(matches), this.JsonSettings);
            }
           catch (Exception ex)
            {
                string errorMessage = "Cannot get top score match ranking. Please try again later.";
                this.Logger.LogError(ex, errorMessage);
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }

        [HttpGet("TopSpectators")]
        public JsonResult TopSpectators()
        {
            try
            {
                var matches = this.MatchesStatsService.TopSpectators();
                return Json(new JsonMobileResult(matches), this.JsonSettings);
            }
            catch (Exception ex)
            {
                string errorMessage = "Cannot get top attandance match ranking. Please try again later.";
                this.Logger.LogError(ex, errorMessage);
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }
    }
}
