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
    public class MatchesController  : Controller
    {
        private ILogger Logger { get; set; }
        private IMatchesService MatchesService { get; set; }
        private JsonSerializerSettings JsonSettings { get; set; }

        public MatchesController(ILogger<MatchesController> logger, IMatchesService matchesService)
        {
            this.Logger = logger;
            this.MatchesService = matchesService;
            this.JsonSettings = new JsonSerializerSettings { ContractResolver = BaseFirstContractResolver.Instance };
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var matches = this.MatchesService.GetMatches();
                return Json(new JsonMobileResult(matches), this.JsonSettings);
            }
           catch (Exception ex)
            {
                string errorMessage = "Cannot get matches info. Please try again later.";
                this.Logger.LogError(ex, errorMessage);
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                var match = this.MatchesService.GetMatch(id);
                return Json(new JsonMobileResult(match), this.JsonSettings);
            }
            catch (Exception ex)
            {
                string errorMessage = "Cannot get match details info. Please try again later.";
                this.Logger.LogError(ex, errorMessage);
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }
    }
}
