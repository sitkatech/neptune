using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Neptune.Common.Email;
using Neptune.Common.JsonConverters;
using Neptune.Common.Services.GDAL;
using Neptune.Common.Services;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.OpenID;
using Neptune.WebMvc.Services;
using NetTopologySuite.IO.Converters;
using SendGrid.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.SqlServer;
using Neptune.Jobs;
using Neptune.Jobs.Services;
using Serilog;
using Serilog.Core;
using LogHelper = Neptune.WebMvc.Services.Logging.LogHelper;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(context.Configuration);
    });
    var logger = CreateSerilogLogger(builder);

    // Add services to the container.
    var services = builder.Services;
    services.AddRazorPages();
    services.AddControllersWithViews(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        })
        .AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Add("~/Views/Shared/TextControls/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/ExpenditureAndBudgetControls/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/PerformanceMeasureControls/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/ProjectControls/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/JurisdictionControls/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/ProjectWatershedControls/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/ProjectUpdateDiffControls/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/EditAttributes/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/SortOrder/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/Location/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/UserJurisdictions/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/HRUCharacteristics/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/Shared/ModeledPerformance/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Views/FieldVisit/ObservationTypePreview/{0}.cshtml");
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory(false));
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new DoubleConverter(10));
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            options.JsonSerializerOptions.WriteIndented = false;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        });

    builder.Configuration.AddJsonFile(builder.Configuration["SECRET_PATH"], optional: false, reloadOnChange: true);

    services.Configure<WebConfiguration>(builder.Configuration);
    services.Configure<NeptuneJobConfiguration>(builder.Configuration);
    services.Configure<SendGridConfiguration>(builder.Configuration);
    var configuration = builder.Configuration.Get<WebConfiguration>();

    services.AddDbContext<NeptuneDbContext>(c =>
    {
        c.UseSqlServer(configuration.DatabaseConnectionString, x =>
        {
            x.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
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

    services.AddAuthorizationPolicies();
    services.AddSendGrid(options => { options.ApiKey = configuration.SendGridApiKey; });
    services.AddSingleton<SitkaSmtpClientService>();

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

    JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
    services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "Keystone";
        })
        .AddCookie("Cookies", options => { options.Cookie.Name = $"Neptune{builder.Environment.EnvironmentName}"; })
        .AddOpenIdConnect("Keystone", options =>
        {
            options.Authority = configuration.KeystoneOpenIDUrl;
            options.CallbackPath = "/Account/LogOn";  // This needs to match redirect uri in Keystone but should NOT be a real url
            options.Scope.Add("openid");
            options.Scope.Add("keystone");
            options.Scope.Add("profile");
            options.Scope.Add("offline_access");
            options.ClientId = configuration.KeystoneOpenIDClientID;
            options.ClientSecret = configuration.KeystoneOpenIDClientSecret;
            //options.ResponseType = "id_token token";
            options.ResponseType = "code";
            options.SaveTokens = true;
            options.TokenValidationParameters.NameClaimType = "name";
            options.TokenValidationParameters.RoleClaimType = "role";
            options.SkipUnrecognizedRequests = true;
            options.Events = new OpenIdConnectEvents()
            {
                OnRedirectToIdentityProvider = async context =>
                {
                    //save current url to state
                    context.ProtocolMessage.State = context.HttpContext.Request.QueryString.HasValue
                        ? context.HttpContext.Request.Query["returnUrl"]
                        : "/";
                },
                OnTokenValidated = context =>
                {
                    var dbContext = context.HttpContext.RequestServices.GetRequiredService<NeptuneDbContext>();
                    var sitkaSmtpClientService = context.HttpContext.RequestServices.GetRequiredService<SitkaSmtpClientService>();

                    if (context.Principal.Identity?.IsAuthenticated == true) // we have a token and we can determine the person.
                    {
                        AuthenticationHelper.ProcessLoginFromKeystone(context, dbContext, configuration, logger, sitkaSmtpClientService);
                    }
                    var url = context.ProtocolMessage.State;
                    var claims = new List<Claim>
                    {
                        new("returnUrl", url)
                    };
                    var appIdentity = new ClaimsIdentity(claims);

                    //add url to claims
                    context.Principal.AddIdentity(appIdentity);

                    return Task.CompletedTask;
                },
                OnTicketReceived = ctx =>
                {
                    var url = ctx.Principal.FindFirst("returnUrl").Value;
                    ctx.ReturnUri = url;
                    return Task.CompletedTask;
                }
            };

        });

    services.AddHttpContextAccessor();
    services.AddScoped<AzureBlobStorageService>();
    services.AddScoped<FileResourceService>();
    services.AddHealthChecks().AddDbContextCheck<NeptuneDbContext>();
}


var app = builder.Build();
{
    app.UseSerilogRequestLogging(opts =>
    {
        opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
        opts.GetLevel = LogHelper.CustomGetLevel;
    });

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        //todo: explore this more on error handling but for now we are doing the handling on the Home/Error route
        //app.UseExceptionHandler(exceptionHandlerApp =>
        //{
        //    exceptionHandlerApp.Run(async context =>
        //    {
        //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        //        // using static System.Net.Mime.MediaTypeNames;
        //        context.Response.ContentType = MediaTypeNames.Text.Html;

        //        await context.Response.WriteAsync("An exception was thrown.");

        //        var exceptionHandlerPathFeature =
        //            context.Features.Get<IExceptionHandlerPathFeature>();

        //        var lastError = exceptionHandlerPathFeature?.Error;
        //        switch (lastError)
        //        {
        //            //case SitkaRecordNotFoundException:
        //            //    SitkaHttpRequestStorage.NotFoundStoredError = lastError as SitkaRecordNotFoundException;
        //            //    break;

        //            case FileNotFoundException:
        //                await context.Response.WriteAsync(" The file was not found.");
        //                break;
        //            case NotImplementedException:
        //                await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
        //                break;
        //        }

        //        if (exceptionHandlerPathFeature?.Path == "/")
        //        {
        //            await context.Response.WriteAsync(" Page: Home.");
        //        }
        //        else
        //        {
        //            context.Response.Redirect("/Home/Error");
        //        }
        //    });
        //});

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) => string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapHealthChecks("/healthz");

    app.Run();
}
return;

Logger CreateSerilogLogger(WebApplicationBuilder webApplicationBuilder)
{
    var outputTemplate = $"[{webApplicationBuilder.Environment.EnvironmentName}] {{Timestamp:yyyy-MM-dd HH:mm:ss zzz}} {{Level}} | {{RequestId}}-{{SourceContext}}: {{Message}}{{NewLine}}{{Exception}}";
    var serilogLogger = new LoggerConfiguration()
        .ReadFrom.Configuration(webApplicationBuilder.Configuration)
        .WriteTo.Console(outputTemplate: outputTemplate);

    return serilogLogger.CreateLogger();
}
