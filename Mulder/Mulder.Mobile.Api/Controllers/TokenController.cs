using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mulder.Mobile.Api.Services;
using System;

namespace Mulder.Mobile.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        public class RequestSource
        {
            public string Name { get; set; }
        }

        private ILogger Logger { get; set; }
        private ITokenService TokenService { get; set; }

        public TokenController(ILogger<TokenController> logger, ITokenService tokenService)
        {
            this.Logger = logger;
            this.TokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RequestSource requestSource)
        {
            try
            {
                if (this.TokenService.IsRequestSourceValid(requestSource?.Name))
                {
                    var result = new ObjectResult(this.TokenService.GenerateToken(requestSource.Name));
                    return result;
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, "Generating token failed.");
            }
            return BadRequest();
        }
    }
}
