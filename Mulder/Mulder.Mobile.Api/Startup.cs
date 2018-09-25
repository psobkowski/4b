using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Services;

namespace Mulder.Mobile.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFrameworkSqlServer().AddDbContext<MulderContext>(
                options =>
                {
                    options.UseSqlServer(@"Server=SPN-PC002;Database=Mulder;User Id=mul_owner;Password=mu_owner;");
                });
            
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<IMatchesService, MatchesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
