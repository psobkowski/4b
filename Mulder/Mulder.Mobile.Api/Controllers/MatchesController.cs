using Microsoft.AspNetCore.Mvc;
using Mulder.Mobile.Api.Domain;
using Mulder.Mobile.Api.Services;
using System;

namespace Mulder.Mobile.Api.Controllers
{
    [Route("api/[controller]")]
    public class MatchesController  : Controller
    {
        private IMatchesService MatchesService { get; set; }

        public MatchesController(IMatchesService matchesService)
        {
            this.MatchesService = matchesService;
        }

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var matches = this.MatchesService.GetMatches();
                return Json(new JsonMobileResult(matches));
            }
           catch (Exception ex)
            {
                string errorMessage = "Cannot get matches info. Please try again later.";
                return Json(new JsonMobileResult(errorMessage));
            }
        }
    }
}
