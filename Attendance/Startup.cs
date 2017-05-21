using Attendance.ConfigurationHelpers;
using Attendance.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackLibrary;
using SlackLibrary.Interfaces;
using System;
using System.IO;
using WebSocketManager;

namespace Attendance
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("connectionStrings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<SlackJsonConfigurationModel>(Configuration);
            services.AddMvc();

            services.AddWebSocketManager();
            
            services.AddSingleton<ISlackConfiguration>((x) => {
                return new SlackConfiguration()
                {
                    SlackCommandToken = Configuration.GetValue<string>("SlackCommandToken"),
                    SlackConnectionString = Configuration.GetValue<string>("SlackConnectionString"),
                    Uri = new Uri(Configuration.GetValue<string>("SlackWebHookUri")),
                    DBConnectionString = Configuration.GetValue<string>("DBConnectionString")
                };
            });

            services.AddTransient<MessageDBContext>();
            
            services.AddTransient<ISlackClient, SlackClient>();
            services.AddTransient<ISlackMessageStore, SlackMessageStore>();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "api/{controller}/{action}/{id?}"
                );
            });

            app.MapWebSocketManager("/slack", serviceProvider.GetService<SlackHandler>());
        }
    }
}