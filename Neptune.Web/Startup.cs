using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace Neptune.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        public IConfiguration Configuration { get; set; }

        public Startup(IWebHostEnvironment environment)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var options = new RewriteOptions().AddRedirectToHttps(301, 9001);
                app.UseRewriter(options);
            }
            
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value == "/assets/config.json")
                {
                    var result = new ConfigDto(Configuration);
                    var json = System.Text.Json.JsonSerializer.Serialize(result);
                    await context.Response.WriteAsync(json);
                    return;
                }

                await next();

                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }

    public class ConfigDto
    {
        public ConfigDto(IConfiguration configuration)
        {
            Production = bool.Parse(configuration["Production"]);
            Staging = bool.Parse(configuration["Staging"]);
            Dev = bool.Parse(configuration["Dev"]);
            MainAppApiUrl = configuration["MainAppApiUrl"];
            GeoserverMapServiceUrl = configuration["GeoserverMapServiceUrl"];
            Auth0 = new Auth0Dto(configuration);
            OcStormwaterToolsBaseUrl = configuration["OcStormwaterToolsBaseUrl"];
        }

        [JsonPropertyName("production")]
        public bool Production { get; set; }
        [JsonPropertyName("staging")]
        public bool Staging { get; set; }
        [JsonPropertyName("dev")]
        public bool Dev { get; set; }
        [JsonPropertyName("mainAppApiUrl")]
        public string MainAppApiUrl { get; set; }
        [JsonPropertyName("geoserverMapServiceUrl")]
        public string GeoserverMapServiceUrl { get; set; }
        [JsonPropertyName("auth0")]
        public Auth0Dto Auth0 { get; set; }
        [JsonPropertyName("ocStormwaterToolsBaseUrl")]
        public string OcStormwaterToolsBaseUrl { get; set;}
    }

    public class Auth0Dto
    {
        public Auth0Dto(IConfiguration configuration)
        {
            ClientID = configuration["auth0_clientId"];
            Domain = configuration["auth0_domain"];
            RedirectUri = configuration["auth0_redirectUri"];
            Audience = configuration["auth0_audience"];
        }

        [JsonPropertyName("clientId")]
        public string ClientID { get; set; }
        [JsonPropertyName("domain")]
        public string Domain { get; set; }
        [JsonPropertyName("redirectUri")]
        public string RedirectUri { get; set; }
        [JsonPropertyName("audience")]
        public string Audience { get; set; }
    }
}
