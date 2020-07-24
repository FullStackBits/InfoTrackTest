using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Domain.Abstractions;
using InfoTrackTest.Domain.Implementations;
using InfoTrackTest.Domain.Services;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Infrastructure.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InfoTrackTest
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
            services.AddTransient<ISearchEngineRequestHandler, SearchEngineRequestHandler>()
                .AddScoped<IURLProcessingQueue, URLProcessingQueue>()
                .AddScoped<IURLSearchResponseRepository, URLSearchResponseRepository>()
                .AddTransient<IWebClient, WebClient>()
                .AddScoped<SearchEngineFactory>()
                .AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SearchEngine}/{action=Index}/{id?}");
            });
        }
    }
}
