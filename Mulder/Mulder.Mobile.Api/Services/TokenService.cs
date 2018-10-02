using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mulder.Mobile.Api.Resolvers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mulder.Mobile.Api.Services
{
    public class TokenService : ITokenService
    {
        private IOptions<SecretsResolver> Secrets { get; }

        public TokenService(IOptions<SecretsResolver> secrets)
        {
            this.Secrets = secrets;
        }

        public bool IsRequestSourceValid(string requestSource)
        {
            return requestSource == this.Secrets.Value.RequestSource;
        }

        public string GenerateToken(string requestSource)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, requestSource),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Secrets.Value.SecurityKey));
            var credential = new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityToken(credential, new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
