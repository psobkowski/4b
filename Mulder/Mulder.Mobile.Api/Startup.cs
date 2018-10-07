using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Resolvers;
using Mulder.Mobile.Api.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace Mulder.Mobile.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Configuration = configuration;
            this.Environment = env;

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("secrets.json", optional : false, reloadOnChange : true);

            this.Secrets = builder.Build();
        }

        public IConfiguration Secrets { get; }
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                if (this.Environment.IsDevelopment())
                {
                    options.Filters.Add(new AllowAnonymousFilter());
                }
            });

            services.AddOptions();
            services.Configure<SecretsResolver>(this.Secrets);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = this.CreateTokenValidationParameters(this.Secrets.GetValue<string>("SecurityKey"));
            });

            services.AddDbContext<MulderContext>(options =>
            {
                options.UseSqlServer(this.Secrets.GetValue<string>("SqlConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Mulder Api", Version = "v1" });
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<IMatchesService, MatchesService>();
            services.AddScoped<IPlayersStatsService, PlayersStatsService>();
            services.AddScoped<IMatchesStatsService, MatchesStatsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mulder Api V1");
            });

            app.UseAuthentication();
            app.UseMvc();
        }

        private TokenValidationParameters CreateTokenValidationParameters(string securityKey)
        {
            var key = Encoding.UTF8.GetBytes(securityKey);

            TokenValidationParameters tvp = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
            return tvp;
        }
    }
}
