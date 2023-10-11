using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;

namespace Neptune.API.Hangfire
{
    public class ParcelLayerUploadBackgroundJob : ScheduledBackgroundJobBase<ParcelLayerUploadBackgroundJob>
    {
        public const string JobName = "Parcel Layer Upload";

        private const int ToleranceInSquareMeters = 200;
        public int PersonID { get; }

        public ParcelLayerUploadBackgroundJob(ILogger<ParcelLayerUploadBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, int personID) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            PersonID = personID;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override async void RunJobImplementation()
        {
            var person = await DbContext.People.FindAsync(PersonID);

            if (person == null)
            {
                throw new InvalidOperationException("PersonID must be valid!");
            }
            try
            {
                var parcelStagingsCount = DbContext.ParcelStagings.Count();

                if (parcelStagingsCount > 0)
                {
                    // first wipe the dependent WQMPParcel table, then wipe the old parcels
                    await DbContext.Database.ExecuteSqlRawAsync("ALTER TABLE dbo.WaterQualityManagementPlanParcel drop constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID");
                    await DbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.WaterQualityManagementPlanParcel");
                    await DbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.Parcel");
                    await DbContext.Database.ExecuteSqlRawAsync("ALTER TABLE dbo.WaterQualityManagementPlanParcel add constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID foreign key (ParcelID) references dbo.Parcel(ParcelID)");
                    await DbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.pParcelUpdateFromStaging");

                    // we need to get the 4326 representation of the geometry; unfortunately can't do it in sql
                    var parcels = DbContext.ParcelGeometries.ToList();
                    foreach (var parcel in parcels)
                    {
                        parcel.Geometry4326 = parcel.GeometryNative.ProjectTo4326();
                    }

                    await DbContext.SaveChangesAsync();

                    // calculate wqmp parcel intersections
                    foreach (var waterQualityManagementPlanBoundary in DbContext.WaterQualityManagementPlanBoundaries)
                    {
                        var waterQualityManagementPlanParcels = ParcelGeometries
                            .GetIntersected(DbContext, waterQualityManagementPlanBoundary.GeometryNative).ToList().Where(x =>
                                x.GeometryNative.Intersection(waterQualityManagementPlanBoundary.GeometryNative).Area >
                                ToleranceInSquareMeters)
                            .Select(x =>
                                new WaterQualityManagementPlanParcel
                                {
                                    WaterQualityManagementPlanID = waterQualityManagementPlanBoundary.WaterQualityManagementPlanID,
                                    ParcelID = x.ParcelID
                                })
                            .ToList();
                        DbContext.WaterQualityManagementPlanParcels.AddRange(waterQualityManagementPlanParcels);
                    }
                    await DbContext.SaveChangesAsync();

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
                        From = new MailAddress(NeptuneConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                    };

                    mailMessage.To.Add(person.Email);
                    await _sitkaSmtpClient.Send(mailMessage);
                }

                await DbContext.Database.ExecuteSqlRawAsync($"EXEC dbo.pParcelStagingDeleteByPersonID @PersonID = {PersonID}");
            }
            catch (Exception)
            {
                var body =
                    "There was an unexpected system error during processing of your Parcel Upload. The Orange County Stormwater Tools development team will investigate and be in touch when this issue is resolved.";

                var mailMessage = new MailMessage
                {
                    Subject = "Parcel Upload Error",
                    Body = body,
                    From = new MailAddress(NeptuneConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                };

                mailMessage.To.Add(person.Email);
                await _sitkaSmtpClient.Send(mailMessage);

                throw;
            }
        }
    }
}