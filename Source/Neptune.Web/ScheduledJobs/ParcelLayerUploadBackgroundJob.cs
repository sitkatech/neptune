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
                var parcelStagings = person.ParcelStagingsWhereYouAreTheUploadedByPerson;

                var count = 0;
                var errorList = new List<string>();
                var parcelsToUpload = new List<Parcel>();

                foreach (var parcelStaging in parcelStagings)
                {
                    var parcel = new Parcel(string.Empty, default, default);

                    if (string.IsNullOrEmpty(parcelStaging.ParcelNumber))
                    {
                        errorList.Add(
                            $"Parcel Number (APN) at row {count} is null, empty, or whitespace. A value must be provided");
                    }
                    else
                    {
                        parcel.ParcelNumber = parcelStaging.ParcelNumber;
                    }

                    if (parcelStaging.ParcelStagingGeometry == null)
                    {
                        errorList.Add(
                            $"The Parcel Geometry at row {count} is null. A value must be provided");
                    }
                    else
                    {
                        parcel.ParcelGeometry = parcelStaging.ParcelStagingGeometry;
                        parcel.ParcelGeometry4326 =
                            CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(parcel.ParcelGeometry);
                    }
                    
                    parcel.ParcelAreaInAcres = parcelStaging.ParcelStagingAreaSquareFeet / CoordinateSystemHelper.SquareFeetToAcresDivisor;
                    parcel.ParcelGeometry4326 =
                        CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(parcel.ParcelGeometry);
                    
                    parcel.ParcelAddress = parcelStaging.ParcelAddress;

                    parcelsToUpload.Add(parcel);
                    count++;
                }

                if (!errorList.Any())
                {
                    // first wipe the dependent WQMPParcel table, then wipe the old parcels
                    DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.WaterQualityManagementPlanParcel");
                    DbContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Parcel");
                    DbContext.Parcels.AddRange(parcelsToUpload);
                    DbContext.SaveChanges(person);
                    DbContext.Database.ExecuteSqlCommand("EXECUTE dbo.pRebuildWaterQualityManagementPlanParcel");

                    var body =
                        $"Your Parcel Upload has been processed. {count.ToString(CultureInfo.CurrentCulture)} updated Parcels are now in the Orange County Stormwater Tools system. It may take up to 24 hours for updated Trash Results to appear in the system.";

                    var mailMessage = new MailMessage
                    {
                        Subject = "Parcel Upload Results",
                        Body = body,
                        From = DoNotReplyMailAddress()
                    };

                    mailMessage.To.Add(person.Email);
                    //SitkaSmtpClient.Send(mailMessage);
                }
                else
                {
                    var body =
                        "Your Parcel upload had errors. Please review the following report, correct the errors, and try again: \n" +
                        string.Join("\n", errorList);

                    var mailMessage = new MailMessage
                    {
                        Subject = "Parcel Upload Error",
                        Body = body,
                        From = DoNotReplyMailAddress()
                    };

                    mailMessage.To.Add(person.Email);
                    //SitkaSmtpClient.Send(mailMessage);
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
                //SitkaSmtpClient.Send(mailMessage);

                throw;
            }
        }

        public static MailAddress DoNotReplyMailAddress()
        {
            return new MailAddress(NeptuneWebConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools");
        }
    }
}