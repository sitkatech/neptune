using System.Globalization;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire;

public class ParcelUploadJob
{
    private const string BlobContainerName = "file-resource";
    private readonly ILogger<ParcelUploadJob> _logger;
    private readonly NeptuneDbContext _dbContext;
    private readonly GDALAPIService _gdalApiService;
    private readonly SitkaSmtpClientService _sitkaSmtpClientService;
    private readonly NeptuneJobConfiguration _neptuneJobConfiguration;
    private const int ToleranceInSquareMeters = 200;

    public ParcelUploadJob(ILogger<ParcelUploadJob> logger, NeptuneDbContext dbContext, GDALAPIService gdalApiService, IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _gdalApiService = gdalApiService;
        _sitkaSmtpClientService = sitkaSmtpClientService;
        _neptuneJobConfiguration = neptuneJobConfiguration.Value;
    }

    public async Task RunJob(string blobName, string featureClassName, int personID, string email)
    {
        try
        {
            _dbContext.Database.SetCommandTimeout(1800);
            var columns = new List<string>
            {
                "AssessmentNo as ParcelNumber",
                "SiteAddress as ParcelAddress",
                "Shape_Area as ParcelStagingAreaSquareFeet",
                "Shape as ParcelStagingGeometry",
                $"{personID} as UploadedByPersonID"
            };

            var apiRequest = new GdbToGeoJsonRequestDto()
            {
                BlobContainer = BlobContainerName,
                CanonicalName = blobName,
                GdbLayerOutputs = new()
                {
                    new()
                    {
                        Columns = columns,
                        FeatureLayerName = featureClassName,
                        NumberOfSignificantDigits = 4,
                        Filter = "WHERE AssessmentNo is not null",
                        CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                        Extent = null
                    }
                }
            };
            var geoJson = await _gdalApiService.Ogr2OgrGdbToGeoJson(apiRequest);
            var parcelStagings = await GeoJsonSerializer.DeserializeFromFeatureCollectionWithCCWCheck<ParcelStaging>(
                geoJson,
                GeoJsonSerializer.DefaultSerializerOptions, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
            var validParcelStagings = parcelStagings.Where(x => x.Geometry is { IsValid: true, Area: > 0 }).ToList();
            if (validParcelStagings.Any())
            {
                await _dbContext.Database.ExecuteSqlRawAsync("dbo.pParcelStagingDelete");
                _dbContext.ParcelStagings.AddRange(validParcelStagings);
                await _dbContext.SaveChangesAsync();
            }

            var parcelStagingsCount = _dbContext.ParcelStagings.Count();

            if (parcelStagingsCount > 0)
            {
                // first wipe the dependent WQMPParcel table, then wipe the old parcels
                await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.pParcelUpdateFromStaging");

                // we need to get the 4326 representation of the geometry; unfortunately can't do it in sql
                var parcels = _dbContext.ParcelGeometries.ToList();
                parcels.AsParallel().ForAll(x => x.Geometry4326 = x.GeometryNative.ProjectTo4326());
                await _dbContext.SaveChangesAsync();

                var errorCount = parcelStagingsCount - parcels.Count;
                var errorMessage = errorCount > 0
                    ? $"{errorCount} Parcels were not imported because they either had an invalid geometry or no APN. "
                    : "";
                var body =
                    $"Your Parcel Upload has been processed. {parcels.Count.ToString(CultureInfo.CurrentCulture)} updated Parcels are now in the Orange County Stormwater Tools system. {errorMessage}";

                var mailMessage = new MailMessage
                {
                    Subject = "Parcel Upload Results",
                    Body = body,
                    From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                };

                mailMessage.To.Add(email);
                await _sitkaSmtpClientService.Send(mailMessage);
            }

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pParcelStagingDelete");
        }
        catch (Exception)
        {
            const string body =
                "There was an unexpected system error during processing of your Parcel Upload. The Orange County Stormwater Tools development team will investigate and be in touch when this issue is resolved.";

            var mailMessage = new MailMessage
            {
                Subject = "Parcel Upload Error",
                Body = body,
                From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
            };

            mailMessage.To.Add(email);
            await _sitkaSmtpClientService.Send(mailMessage);

            throw;
        }
    }
}