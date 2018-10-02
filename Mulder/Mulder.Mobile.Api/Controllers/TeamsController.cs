using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulder.Mobile.Api.Resolvers;
using Mulder.Mobile.Api.Domain;
using Mulder.Mobile.Api.Services;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Logging;

namespace Mulder.Mobile.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TeamsController  : Controller
    {
        private ILogger Logger { get; set; }
        private ITeamsService TeamsService { get; set; }
        private JsonSerializerSettings JsonSettings { get; set; }

        public TeamsController(ILogger<TeamsController> logger, ITeamsService teamsService)
        {
            this.Logger = logger;
            this.TeamsService = teamsService;
            this.JsonSettings = new JsonSerializerSettings { ContractResolver = BaseFirstContractResolver.Instance };
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var teams = this.TeamsService.GetTeams();
                return Json(new JsonMobileResult(teams), this.JsonSettings);
            }
           catch (Exception ex)
            {
                var errorMessage = "Cannot get teams info. Please try again later.";
                this.Logger.LogError(ex, errorMessage);
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                var team = this.TeamsService.GetTeam(id);
                return Json(new JsonMobileResult(team), this.JsonSettings);
            }
            catch (Exception ex)
            {
                var errorMessage = "Cannot get team details info. Please try again later.";
                this.Logger.LogError(ex, errorMessage);
                return Json(new JsonMobileResult(errorMessage), this.JsonSettings);
            }
        }
    }
}
