using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neptune.API.Services;
using Neptune.API.Services.Filter;
using Neptune.API.Services.Middleware;
using Neptune.Common.Email;
using Neptune.Common.JsonConverters;
using Neptune.Common.Services;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.Jobs;
using Neptune.Jobs.Hangfire;
using Neptune.Jobs.Services;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using OpenAI;
using SendGrid.Extensions.DependencyInjection;
using Serilog;
using System;
using System.ClientModel;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Auth0.AspNetCore.Authentication;
using LogHelper = Neptune.API.Services.Logging.LogHelper;

namespace Neptune.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
                options.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
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
            services.AddSingleton(Configuration);


            #region Auth0 authentication
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = configuration.Auth0Domain;
                options.ClientId = configuration.Auth0ClientID;
            });
            #endregion

            services.AddDbContext<NeptuneDbContext>(c =>
            {
                c.UseSqlServer(configuration.DatabaseConnectionString, x =>
                {
                    x.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    x.UseNetTopologySuite();
                });
            });
            services.AddTransient(s => new KeystoneService(s.GetService<IHttpContextAccessor>(), configuration.KeystoneOpenIDUrl));

            AddExternalHttpClientServices(services, configuration);

            services.AddScoped<AzureBlobStorageService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(s => s.GetService<IHttpContextAccessor>().HttpContext);
            services.AddScoped(s => UserContext.GetUserAsDtoFromHttpContext(s.GetService<NeptuneDbContext>(), s.GetService<IHttpContextAccessor>().HttpContext));

            #region OpenAI
            services.AddSingleton(_ =>
            {
                ApiKeyCredential nonAzureOpenAIApiKey = new(configuration.OpenAIApiKey);
                OpenAIClient client = new(nonAzureOpenAIApiKey,
                    new OpenAIClientOptions
                    {
                        OrganizationId = configuration.OpenAIOrganizationID,
                        ProjectId = configuration.OpenAIProjectID
                    });

                return client;
            });
            #endregion

            #region Sendgrid
            services.AddSendGrid(options => { options.ApiKey = configuration.SendGridApiKey; });
            services.AddSingleton<SitkaSmtpClientService>();
            #endregion

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
                options.DocumentFilter<UseMethodNameAsOperationIdFilter>();
            });
            #endregion

            services.AddHealthChecks().AddDbContextCheck<NeptuneDbContext>();
        }

        private static void AddExternalHttpClientServices(IServiceCollection services, NeptuneConfiguration configuration)
        {
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
                opts.GetLevel = LogHelper.CustomGetLevel;
            });

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
            app.UseMiddleware<EntityNotFoundMiddleware>();
            app.UseMiddleware<LogHelper>();

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
        }
        private void OnShutdown()
        {
            Thread.Sleep(1000);
        }
    }
}
