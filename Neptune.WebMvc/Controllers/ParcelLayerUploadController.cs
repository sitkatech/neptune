using System.Globalization;
using System.Net.Mail;
using Hangfire;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Views.ParcelLayerUpload;

namespace Neptune.WebMvc.Controllers
{
    public class ParcelLayerUploadController : NeptuneBaseController<ParcelLayerUploadController>
    {
        private readonly SitkaSmtpClientService _sitkaSmtpClient;
        private const int ToleranceInSquareMeters = 200;

        public ParcelLayerUploadController(NeptuneDbContext dbContext, ILogger<ParcelLayerUploadController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, SitkaSmtpClientService sitkaSmtpClientService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _sitkaSmtpClient = sitkaSmtpClientService;
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult UpdateParcelLayerGeometry()
        {
            var viewModel = new UploadParcelLayerViewModel { PersonID = CurrentPerson.PersonID };
            return ViewUpdateParcelLayerGeometry(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public async Task<IActionResult> UpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateParcelLayerGeometry(viewModel);
            }

            BackgroundJob.Enqueue(() => RunJobImplementation());
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Parcels were successfully added to the staging area. The staged Parcels will be processed and added to the system. You will receive an email notification when this process completes or if errors in the upload are discovered during processing.");

            return Redirect(SitkaRoute<ParcelController>.BuildUrlFromExpression(_linkGenerator, c => c.Index()));
        }

        private ViewResult ViewUpdateParcelLayerGeometry(UploadParcelLayerViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<ParcelLayerUploadController>.BuildUrlFromExpression(_linkGenerator, c => c.UpdateParcelLayerGeometry());

            var viewData = new UploadParcelLayerViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, newGisUploadUrl);
            return RazorView<UploadParcelLayer, UploadParcelLayerViewData, UploadParcelLayerViewModel>(viewData, viewModel);
        }

        protected async Task RunJobImplementation()
        {
            try
            {
                var parcelStagingsCount = _dbContext.ParcelStagings.Count();

                if (parcelStagingsCount > 0)
                {
                    // first wipe the dependent WQMPParcel table, then wipe the old parcels
                    await _dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE dbo.WaterQualityManagementPlanParcel drop constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID");
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.WaterQualityManagementPlanParcel");
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.Parcel");
                    await _dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE dbo.WaterQualityManagementPlanParcel add constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID foreign key (ParcelID) references dbo.Parcel(ParcelID)");
                    await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.pParcelUpdateFromStaging");

                    // we need to get the 4326 representation of the geometry; unfortunately can't do it in sql
                    var parcels = _dbContext.ParcelGeometries.ToList();
                    foreach (var parcel in parcels)
                    {
                        parcel.Geometry4326 = parcel.GeometryNative.ProjectTo4326();
                    }

                    await _dbContext.SaveChangesAsync();

                    // calculate wqmp parcel intersections
                    foreach (var waterQualityManagementPlanBoundary in _dbContext.WaterQualityManagementPlanBoundaries)
                    {
                        var waterQualityManagementPlanParcels = ParcelGeometries
                            .GetIntersected(_dbContext, waterQualityManagementPlanBoundary.GeometryNative).ToList().Where(x =>
                                x.GeometryNative.Intersection(waterQualityManagementPlanBoundary.GeometryNative).Area >
                                ToleranceInSquareMeters)
                            .Select(x =>
                                new WaterQualityManagementPlanParcel
                                {
                                    WaterQualityManagementPlanID = waterQualityManagementPlanBoundary.WaterQualityManagementPlanID,
                                    ParcelID = x.ParcelID
                                })
                            .ToList();
                        _dbContext.WaterQualityManagementPlanParcels.AddRange(waterQualityManagementPlanParcels);
                    }
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
                        From = new MailAddress(_webConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                    };

                    mailMessage.To.Add(CurrentPerson.Email);
                    await _sitkaSmtpClient.Send(mailMessage);
                }

                await _dbContext.Database.ExecuteSqlRawAsync($"EXEC dbo.pParcelStagingDeleteByPersonID @PersonID = {CurrentPerson.PersonID}");
            }
            catch (Exception)
            {
                var body =
                    "There was an unexpected system error during processing of your Parcel Upload. The Orange County Stormwater Tools development team will investigate and be in touch when this issue is resolved.";

                var mailMessage = new MailMessage
                {
                    Subject = "Parcel Upload Error",
                    Body = body,
                    From = new MailAddress(_webConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                };

                mailMessage.To.Add(CurrentPerson.Email);
                await _sitkaSmtpClient.Send(mailMessage);

                throw;
            }
        }

    }
}
