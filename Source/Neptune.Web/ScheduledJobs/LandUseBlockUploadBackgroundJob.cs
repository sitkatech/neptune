using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net.Mail;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Email;

namespace Neptune.Web.ScheduledJobs
{
    public class LandUseBlockUploadBackgroundJob : ScheduledBackgroundJobBase
    {
        public int PersonID { get; }

        public LandUseBlockUploadBackgroundJob(int personID)
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

                var landUseBlockStagings = person.LandUseBlockStagingsWhereYouAreTheUploadedByPerson;
                var stormwaterJurisdictions = DbContext.StormwaterJurisdictions.ToList();
                var stormwaterJurisdictionsPersonCanEdit =
                    person.GetStormwaterJurisdictionsPersonCanViewWithContext(DbContext).ToList();

                var allowedStormwaterJurisdictionNames = string.Join(", ",
                    stormwaterJurisdictions.Select(x => x.Organization.OrganizationName).ToList());
                var allowedPermitTypeNames =
                    string.Join(", ", PermitType.All.Select(x => x.PermitTypeDisplayName).ToList());
                var allowedPriorityLandUseTypeNames = string.Join(", ",
                    PriorityLandUseType.All.Select(x => x.PriorityLandUseTypeDisplayName).ToList());

                var count = 0;
                var errorList = new List<string>();
                var landUseBlocksToUpload = new List<LandUseBlock>();

                foreach (var landUseBlockStaging in landUseBlockStagings)
                {
                    var landUseBlock = new LandUseBlock(default(int), default(int), string.Empty, default(DbGeometry),
                        default(decimal), string.Empty, default(decimal), default(decimal), default(int), default(int), default(DbGeometry));


                    var landUseBlockPLUType = landUseBlockStaging.PriorityLandUseType;
                    if (!PriorityLandUseType.All.Select(x => x.PriorityLandUseTypeDisplayName)
                        .Contains(landUseBlockPLUType))
                    {

                        errorList.Add(
                            $"The Priority Land Use Type '{landUseBlockPLUType}' at row {count} was not found. Acceptable values are {allowedPriorityLandUseTypeNames}");
                    }
                    else
                    {
                        landUseBlock.PriorityLandUseTypeID = PriorityLandUseType.All
                            .Single(x => x.PriorityLandUseTypeDisplayName == landUseBlockPLUType).PriorityLandUseTypeID;
                    }

                    landUseBlock.LandUseDescription = landUseBlockStaging.LandUseDescription;


                    landUseBlock.TrashGenerationRate = landUseBlockStaging.TrashGenerationRate;
                    landUseBlock.LandUseForTGR = landUseBlockStaging.LandUseForTGR;

                    if (landUseBlockStaging.LandUseForTGR == "Residential")
                    {
                        landUseBlock.MedianHouseholdIncomeResidential = landUseBlockStaging.MedianHouseholdIncome;
                        landUseBlock.MedianHouseholdIncomeRetail = 0;
                    }
                    else if (landUseBlockStaging.LandUseForTGR == "Retail")
                    {
                        landUseBlock.MedianHouseholdIncomeResidential = 0;
                        landUseBlock.MedianHouseholdIncomeRetail = landUseBlockStaging.MedianHouseholdIncome;
                    }
                    else
                    {
                        landUseBlock.MedianHouseholdIncomeResidential = 0;
                        landUseBlock.MedianHouseholdIncomeRetail = 0;
                    }

                    var stormwaterJurisdictionName = landUseBlockStaging.StormwaterJurisdiction;
                    if (string.IsNullOrEmpty(stormwaterJurisdictionName))
                    {
                        errorList.Add(
                            $"The Stormwater Jurisdiction at row {count} is null, empty or whitespace. A value must be provided");
                    }

                    if (!stormwaterJurisdictions.Select(x => x.Organization.OrganizationName)
                        .Contains(stormwaterJurisdictionName))
                    {

                        errorList.Add(
                            $"The Stormwater Jurisdiction '{stormwaterJurisdictionName}' at row {count} was not found. Acceptable values are {allowedStormwaterJurisdictionNames}");
                    }
                    else
                    {
                        var stormwaterJurisdictionToAssign = stormwaterJurisdictions
                            .Single(x => x.Organization.OrganizationName == landUseBlockStaging.StormwaterJurisdiction);
                        if (stormwaterJurisdictionsPersonCanEdit.Select(x => x.StormwaterJurisdictionID)
                            .Contains(stormwaterJurisdictionToAssign.StormwaterJurisdictionID))
                        {
                            landUseBlock.StormwaterJurisdictionID = stormwaterJurisdictionToAssign.StormwaterJurisdictionID;

                            if (landUseBlockStaging.LandUseBlockStagingGeometry == null)
                            {
                                errorList.Add(
                                    $"The Land Use Block Geometry at row {count} is null. A value must be provided");
                            } else if (!landUseBlockStaging.LandUseBlockStagingGeometry.IsValid)
                            {
                                errorList.Add(
                                    $"The Land Use Block Geometry at row {count} is invalid.");
                            }
                            else
                            {

                                var clippedGeometry = landUseBlockStaging.LandUseBlockStagingGeometry
                                    .Intersection(stormwaterJurisdictionToAssign.StormwaterJurisdictionGeometry.GeometryNative);

                                if (clippedGeometry == null || clippedGeometry.IsEmpty)
                                {
                                    errorList.Add(
                                        $"The Land Use Block Geometry at row {count} is not in the assigned Stormwater Jurisdiction. Please make sure Land Use Block is in {stormwaterJurisdictionToAssign.Organization.OrganizationName}.");
                                }
                                else
                                {
                                    landUseBlock.LandUseBlockGeometry = clippedGeometry;
                                    landUseBlock.LandUseBlockGeometry4326 =
                                        CoordinateSystemHelper.ProjectCaliforniaStatePlaneVIToWebMercator(clippedGeometry);
                                }
                            }
                        }
                        else
                        {
                            errorList.Add(
                                $"You do not have permission to edit Stormwater Jurisdiction {stormwaterJurisdictionToAssign.Organization.OrganizationName}. Please remove all features with this Stormwater Jurisdiction from the upload and try again.");
                        }
                    }



                    var permitType = landUseBlockStaging.PermitType;
                    if (string.IsNullOrEmpty(permitType))
                    {
                        errorList.Add(
                            $"The Permit Type at row {count} is null, empty or whitespace. A value must be provided");
                    }

                    if (!PermitType.All.Select(x => x.PermitTypeDisplayName).Contains(landUseBlockStaging.PermitType))
                    {
                        errorList.Add(
                            $"The Permit Type '{permitType}' at row {count} was not found. Acceptable values are {allowedPermitTypeNames}");
                    }
                    else
                    {
                        landUseBlock.PermitTypeID =
                            PermitType.All.Single(x => x.PermitTypeDisplayName == permitType).PermitTypeID;
                    }

                    landUseBlocksToUpload.Add(landUseBlock);
                    count++;
                }

                if (!errorList.Any())
                {
                    var stormwaterJurisdictionIDsToClear =
                        landUseBlocksToUpload.Select(x => x.StormwaterJurisdictionID).Distinct();
                    var stormwaterJurisdictionToClears = stormwaterJurisdictions
                        .Where(x => stormwaterJurisdictionIDsToClear.Contains(x.StormwaterJurisdictionID)).ToList();

                    //foreach (var stormwaterJurisdictionToClear in stormwaterJurisdictionToClears)
                    //{
                    //    var landUseBlocksToClear = stormwaterJurisdictionToClear.LandUseBlocks;
                    //    var trashGeneratingUnitsToUpdateLandUseBlockID = landUseBlocksToClear.SelectMany(x=>x.TrashGeneratingUnits);

                    //    foreach (var trashGeneratingUnit in trashGeneratingUnitsToUpdateLandUseBlockID)
                    //    {
                    //        trashGeneratingUnit.LandUseBlockID = null;
                    //    }
                    //    DbContext.SaveChanges(person);

                    //    DbContext.LandUseBlocks.DeleteLandUseBlock(landUseBlocksToClear);
                    //    DbContext.SaveChanges(person);

                    //}

                    // NP 6/25 SUPER gross to fire plain sql at the db, but trying to use the ORM as above results in exceeding any reasonable timeout
                    var landUseBlockIDsToClear = stormwaterJurisdictionToClears.SelectMany(x => x.LandUseBlocks)
                        .Select(x => x.LandUseBlockID).ToList();

                    if (landUseBlockIDsToClear.Any())
                    {
                        var landUseBlockIDsToClearCommaSeparatedString = string.Join(",", landUseBlockIDsToClear);

                        var nullOutTGULandUseBlockIDs =
                            $"UPDATE dbo.TrashGeneratingUnit SET LandUseBlockID = null WHERE LandUseBlockID in ({landUseBlockIDsToClearCommaSeparatedString})";

                        var nullOutTGU4326LandUseBlockIDs =
                            $"UPDATE dbo.TrashGeneratingUnit4326 SET LandUseBlockID = null WHERE LandUseBlockID in ({landUseBlockIDsToClearCommaSeparatedString})";
                        var deleteLandUseBlocks =
                            $"DELETE FROM dbo.LandUseBlock WHERE LandUseBlockID in ({landUseBlockIDsToClearCommaSeparatedString})";

                        DbContext.Database.CommandTimeout = 960;
                        DbContext.Database.ExecuteSqlCommand(nullOutTGULandUseBlockIDs);
                        DbContext.Database.ExecuteSqlCommand(nullOutTGU4326LandUseBlockIDs);
                        DbContext.Database.ExecuteSqlCommand(deleteLandUseBlocks);
                    }

                    DbContext.LandUseBlocks.AddRange(landUseBlocksToUpload);
                    DbContext.SaveChanges(person);

                    var body =
                        "Your Land Use Block Upload has been processed. The updated Land Use Blocks are now in the Orange County Stormwater Tools system. It may take up to 24 hours for updated Trash Results to appear in the system.";

                    var mailMessage = new MailMessage
                    {
                        Subject = "Land Use Block Upload Results",
                        Body = body,
                        From = DoNotReplyMailAddress()
                    };

                    mailMessage.To.Add(person.Email);
                    SitkaSmtpClient.Send(mailMessage);
                }
                else
                {
                    var body =
                        "Your Land Use Block upload had errors. Please review the following report, correct the errors, and try again: \n" +
                        string.Join("\n", errorList);

                    var mailMessage = new MailMessage
                    {
                        Subject = "Land Use Block Upload Error",
                        Body = body,
                        From = DoNotReplyMailAddress()
                    };

                    mailMessage.To.Add(person.Email);
                    SitkaSmtpClient.Send(mailMessage);
                }

                DbContext.pLandUseBlockStagingDeleteByPersonID(PersonID);
            }
            catch (Exception)
            {
                var body =
                    "There was an unexpected system error during processing of your Land Use Block Upload. The Orange County Stormwater Tools development team will investigate and be in touch when this issue is resolved.";

                var mailMessage = new MailMessage
                {
                    Subject = "Land Use Block Upload Error",
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