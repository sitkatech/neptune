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
            CreateAccountRedirectUrl = configuration["CreateAccountRedirectUrl"];
            GeoserverMapServiceUrl = configuration["GeoserverMapServiceUrl"];
            KeystoneAuthConfiguration = new KeystoneAuthConfigurationDto(configuration);
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
        [JsonPropertyName("createAccountRedirectUrl")]
        public string CreateAccountRedirectUrl { get; set; }
        [JsonPropertyName("geoserverMapServiceUrl")]
        public string GeoserverMapServiceUrl { get; set; }
        [JsonPropertyName("keystoneAuthConfiguration")]
        public KeystoneAuthConfigurationDto KeystoneAuthConfiguration { get; set; }
        [JsonPropertyName("ocStormwaterToolsBaseUrl")]
        public string OcStormwaterToolsBaseUrl { get; set;}
    }

    public class KeystoneAuthConfigurationDto
    {
        public KeystoneAuthConfigurationDto(IConfiguration configuration)
        {
            ClientID = configuration["Keystone_ClientID"];
            Issuer = configuration["Keystone_Issuer"];
            RedirectUriRelative = configuration["Keystone_RedirectUriRelative"];
            Scope = configuration["Keystone_Scope"];
            SessionChecksEnabled = bool.Parse(configuration["Keystone_SessionCheckEnabled"]);
            LogoutUrl = configuration["Keystone_LogoutUrl"];
            PostLogoutRedirectUri = configuration["Keystone_PostLogoutRedirectUri"];
            WaitForTokenInMsec = int.Parse(configuration["Keystone_WaitForTokenInMsec"]);
            ResponseType = configuration["Keystone_ResponseType"];
            DisablePKCE = bool.Parse(configuration["Keystone_DisablePKCE"]);
        }

        [JsonPropertyName("clientId")]
        public string ClientID { get; set; }
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }
        [JsonPropertyName("redirectUriRelative")]
        public string RedirectUriRelative { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
        [JsonPropertyName("sessionChecksEnabled")]
        public bool SessionChecksEnabled { get; set; }
        [JsonPropertyName("logoutUrl")]
        public string LogoutUrl { get; set; }
        [JsonPropertyName("postLogoutRedirectUri")]
        public string PostLogoutRedirectUri { get; set; }
        [JsonPropertyName("waitForTokenInMsec")]
        public int WaitForTokenInMsec { get; set; }
        [JsonPropertyName("responseType")]
        public string ResponseType {get; set;}
        [JsonPropertyName("disablePKCE")]
        public bool DisablePKCE {get; set;}
    }
}
