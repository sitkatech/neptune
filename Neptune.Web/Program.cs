using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Neptune.Common.JsonConverters;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.OpenID;
using Neptune.Web.Services;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
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
            var scale = Math.Pow(10, 3);
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

    builder.Configuration.AddJsonFile(builder.Configuration["SECRET_PATH"], optional: false, reloadOnChange: true);

    services.Configure<WebConfiguration>(builder.Configuration);
    var configuration = builder.Configuration.Get<WebConfiguration>();

    services.AddDbContext<NeptuneDbContext>(c =>
    {
        c.UseSqlServer(configuration.DatabaseConnectionString, x =>
        {
            x.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
            x.UseNetTopologySuite();
        });
    });

    services.AddAuthorizationPolicies();

    JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
    services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "Keystone";
        })
        .AddCookie("Cookies")
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
                OnTokenValidated = tvc =>
                {
                    var dbContext = tvc.HttpContext.RequestServices.GetRequiredService<NeptuneDbContext>();

                    if (tvc.Principal.Identity != null && tvc.Principal.Identity.IsAuthenticated) // we have a token and we can determine the person.
                    {
                        AuthenticationHelper.ProcessLoginFromKeystone(tvc, dbContext, configuration);
                    }
                    return Task.FromResult(0);
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
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
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
