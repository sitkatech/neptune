using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;
using Neptune.Common.JsonConverters;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    var services = builder.Services;
    services.AddRazorPages();
    services.AddControllersWithViews().AddJsonOptions(options => {
        var scale = Math.Pow(10, 3);
        var geometryFactory = new GeometryFactory(new PrecisionModel(scale), 4326);
        options.JsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory(geometryFactory, false));
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
        options.JsonSerializerOptions.Converters.Add(new DoubleConverter(2));
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.JsonSerializerOptions.WriteIndented = false;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
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

    services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            if (builder.Environment.IsDevelopment())
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
            options.Authority = configuration.KeystoneOpenIDUrl;
            options.RequireHttpsMetadata = false;
            options.SecurityTokenValidators.Clear();
            options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler
            {
                MapInboundClaims = false
            });
            options.TokenValidationParameters.NameClaimType = "name";
            options.TokenValidationParameters.RoleClaimType = "role";
        });

    services.AddHttpContextAccessor();
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

    app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
