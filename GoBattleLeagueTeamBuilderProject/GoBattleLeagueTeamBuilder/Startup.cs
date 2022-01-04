using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using GoBattleLeagueTeamBuilder.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GoBattleLeagueTeamBuilder.Models;
using GoBattleLeagueTeamBuilder.Models.Interfaces;
using GoBattleLeagueTeamBuilder.Models.Repositories;
using GoBattleLeagueTeamBuilder.Services;

namespace GoBattleLeagueTeamBuilder
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
            //identity connection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("PokemonGoTeamBuilderAuthneticationConnection")));

            //GoBattleLeagueTeamBuilder connection
            services.AddDbContext<GoBattleLeagueTeamBuilderDBContext>(opts =>
            {
                //local host connection
                opts.UseSqlServer(Configuration["ConnectionStrings:GoBattleLeageTeamBuilderConnection"]);
                //azure connection
                //opts.UseSqlServer(Configuration.GetConnectionString("SpillTrackerAzureDB"));

            });

            // Add our custom interfaces and repos for fun Dependency Injection
            services.AddScoped<IPokedexRepository, PokedexRepository>();
            services.AddScoped<IPVP_IVsAPIRepository, PVP_IVsAPIRepository>();
            services.AddScoped<ISendHTTPWebRequest, SendHTTPWebRequest>();
            services.AddScoped<IRepository<Pokedex>, Repository<Pokedex>>();
            services.AddScoped<IAdminUtilities, AdminUtilities>();



            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
