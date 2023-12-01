using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Neptune.API.Services;
using Neptune.API.Services.Telemetry;
using Neptune.EFModels.Entities;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using System.Text.Json.Serialization;
using System.Text.Json;
using Neptune.Common.Email;
using Neptune.Common.JsonConverters;
using SendGrid.Extensions.DependencyInjection;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;
using Hangfire;
using Hangfire.SqlServer;
using Neptune.API.Services.Filter;
using Neptune.Common.Services;
using Neptune.Common.Services.GDAL;
using Neptune.Jobs;
using Neptune.Jobs.Hangfire;
using Neptune.Jobs.Services;

namespace Neptune.API
{
    public class Startup
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IWebHostEnvironment _environment;
        private readonly string _instrumentationKey;
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            _environment = environment;

            _instrumentationKey = Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];

            if (!string.IsNullOrWhiteSpace(_instrumentationKey))
            {
                _telemetryClient = new TelemetryClient(TelemetryConfiguration.CreateDefault())
                {
                    InstrumentationKey = _instrumentationKey
                };
            }
            else
            {
                _telemetryClient = new TelemetryClient(TelemetryConfiguration.CreateDefault());
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(_instrumentationKey);
            services.AddControllers(options =>
                {
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                })
                .AddJsonOptions(options =>
            {
                var scale = Math.Pow(10, 6);
                var geometryFactory = new GeometryFactory(new PrecisionModel(scale), 4326);
                options.JsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory(geometryFactory, false));
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                options.JsonSerializerOptions.Converters.Add(new DoubleConverter(7));
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            });

            services.Configure<NeptuneConfiguration>(Configuration);
            services.Configure<NeptuneJobConfiguration>(Configuration);
            services.Configure<SendGridConfiguration>(Configuration);
            var configuration = Configuration.Get<NeptuneConfiguration>();

            var keystoneHost = configuration.KeystoneOpenIDUrl;

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    if (_environment.IsDevelopment())
                    {
                        // NOTE: CG 3/22 - This allows the self-signed cert on Keystone to work locally.
                        options.BackchannelHttpHandler = new HttpClientHandler()
                        {
                            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
                        };
                        //These allow the use of the container name and the url when developing.
                        options.TokenValidationParameters.ValidateIssuer = false;
                    }
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.Authority = keystoneHost;
                    options.RequireHttpsMetadata = false;
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler
                    {
                        MapInboundClaims = false
                    });
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.RoleClaimType = "role";
                });

            services.AddDbContext<NeptuneDbContext>(c =>
            {
                c.UseSqlServer(configuration.DatabaseConnectionString, x =>
                {
                    x.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    x.UseNetTopologySuite();
                });
            });

            services.AddHttpClient<NereidService>(c =>
            {
                c.BaseAddress = new Uri(configuration.NereidUrl);
                c.Timeout = TimeSpan.FromDays(1);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                httpClientHandler.ServerCertificateCustomValidationCallback =
                    (_, _, _, _) => true;

                return httpClientHandler;
            });

            services.AddHttpClient<OCGISService>(c =>
            {
                c.BaseAddress = new Uri(configuration.OCGISBaseUrl);
                c.Timeout = TimeSpan.FromDays(1);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                httpClientHandler.ServerCertificateCustomValidationCallback =
                    (_, _, _, _) => true;

                return httpClientHandler;
            });

            services.AddHttpClient<GDALAPIService>(c =>
            {
                c.BaseAddress = new Uri(configuration.GDALAPIBaseUrl);
                c.Timeout = TimeSpan.FromDays(1);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                httpClientHandler.ServerCertificateCustomValidationCallback =
                    (_, _, _, _) => true;

                return httpClientHandler;
            });

            services.AddHttpClient<QGISAPIService>(c =>
            {
                c.BaseAddress = new Uri(configuration.QGISAPIBaseUrl);
                c.Timeout = TimeSpan.FromDays(1);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                httpClientHandler.ServerCertificateCustomValidationCallback =
                    (_, _, _, _) => true;

                return httpClientHandler;
            });

            services.AddSingleton<ITelemetryInitializer, CloudRoleNameTelemetryInitializer>();
            services.AddSingleton<ITelemetryInitializer, UserInfoTelemetryInitializer>();

            var logger = GetSerilogLogger();
            services.AddSingleton(logger);

            services.AddTransient(s => new KeystoneService(s.GetService<IHttpContextAccessor>(), keystoneHost));

            services.AddSendGrid(options => { options.ApiKey = configuration.SendGridApiKey; });
            services.AddSingleton<SitkaSmtpClientService>();

            services.AddHttpContextAccessor();
            services.AddScoped<AzureBlobStorageService>();
            services.AddControllers();

            #region Hangfire
            services.AddHangfire(x => x
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.DatabaseConnectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer(x =>
            {
                x.WorkerCount = 1;
            });
            #endregion


            #region Swagger
            // Base swagger services
            services.AddSwaggerGen(options =>
            {
                // extra options here if you wanted
            });
            #endregion

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory, ILogger logger)
        {
            loggerFactory.AddSerilog(logger);
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
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();
            app.UseCors(policy =>
            {
                //TODO: don't allow all origins
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(TelemetryHelper.PostBodyTelemetryMiddleware);

            #region Hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new HangfireAuthorizationFilter(Configuration) }
            });

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            HangfireJobScheduler.ScheduleRecurringJobs();
            #endregion

            #region Swagger
            // Register swagger middleware and enable the swagger UI which will be 
            // accessible at https://<apihostname>/swagger
            // NOTE: There is no auth on these endpoints out of the box.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "V1");
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthz");
            });

            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            var modules = app.ApplicationServices.GetServices<ITelemetryModule>();
            var dependencyModule = modules.OfType<DependencyTrackingTelemetryModule>().FirstOrDefault();

            if (dependencyModule != null)
            {
                var domains = dependencyModule.ExcludeComponentCorrelationHttpHeadersOnDomains;
                domains.Add("core.windows.net");
                domains.Add("10.0.75.1");
            }
        }
        private void OnShutdown()
        {
            _telemetryClient.Flush();
            Thread.Sleep(1000);
        }

        private ILogger GetSerilogLogger()
        {
            var outputTemplate = $"[{_environment.EnvironmentName}] {{Timestamp:yyyy-MM-dd HH:mm:ss zzz}} {{Level}} | {{RequestId}}-{{SourceContext}}: {{Message}}{{NewLine}}{{Exception}}";
            var serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console(outputTemplate: outputTemplate);

            if (!_environment.IsDevelopment())
            {
                serilogLogger.WriteTo.ApplicationInsights(_telemetryClient, new TraceTelemetryConverter());
            }

            return serilogLogger.CreateLogger();
        }
    }
}
