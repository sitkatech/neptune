using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using LtInfo.Common;
using LtInfo.Common.Email;

namespace Neptune.Web.ScheduledJobs
{
    public class ParcelLayerUploadBackgroundJob : ScheduledBackgroundJobBase
    {
        public int PersonID { get; }

        public ParcelLayerUploadBackgroundJob(int personID)
        {
            PersonID = personID;
        }

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };



        protected override void RunJobImplementation()
        {
            var person = DbContext.People.Find(PersonID);

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
                    DbContext.Database.ExecuteSqlCommand("ALTER TABLE dbo.WaterQualityManagementPlanParcel drop constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID");
                    DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.WaterQualityManagementPlanParcel");
                    DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.Parcel");
                    DbContext.Database.ExecuteSqlCommand("ALTER TABLE dbo.WaterQualityManagementPlanParcel add constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID foreign key (ParcelID) references dbo.Parcel(ParcelID)");
                    DbContext.Database.ExecuteSqlCommand("EXECUTE dbo.pParcelUpdateFromStaging");
                    DbContext.Database.ExecuteSqlCommand("EXECUTE dbo.pRebuildWaterQualityManagementPlanParcel");

                    // we need to get the 4326 representation of the geometry; unfortunately can't do it in sql
                    var parcels = DbContext.Parcels.ToList();
                    foreach (var parcel in parcels)
                    {
                        parcel.ParcelGeometry4326 = CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(parcel.ParcelGeometry);
                    }

                    DbContext.SaveChangesWithNoAuditing();

                    var errorCount = parcelStagingsCount - parcels.Count;
                    var errorMessage = errorCount > 0
                        ? $"{errorCount} Parcels were not imported because they either had an invalid geometry or no APN. "
                        : "";
                    var body =
                        $"Your Parcel Upload has been processed. {parcels.Count.ToString(CultureInfo.CurrentCulture)} updated Parcels are now in the Orange County Stormwater Tools system. {errorMessage}It may take up to 24 hours for updated Trash Results to appear in the system.";

                    var mailMessage = new MailMessage
                    {
                        Subject = "Parcel Upload Results",
                        Body = body,
                        From = DoNotReplyMailAddress()
                    };

                    mailMessage.To.Add(person.Email);
                    SitkaSmtpClient.Send(mailMessage);
                }

                DbContext.pParcelStagingDeleteByPersonID(PersonID);
            }
            catch (Exception)
            {
                var body =
                    "There was an unexpected system error during processing of your Parcel Upload. The Orange County Stormwater Tools development team will investigate and be in touch when this issue is resolved.";

                var mailMessage = new MailMessage
                {
                    Subject = "Parcel Upload Error",
                    Body = body,
                    From = DoNotReplyMailAddress()
                };

                mailMessage.To.Add(person.Email);
                SitkaSmtpClient.Send(mailMessage);

                throw;
            }
        }

        public static MailAddress DoNotReplyMailAddress()
        {
            return new MailAddress(NeptuneWebConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools");
        }
    }
}