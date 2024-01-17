﻿using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public class LandUseBlockUploadBackgroundJob
    {
        private readonly NeptuneDbContext _dbContext;
        private readonly NeptuneJobConfiguration _neptuneJobConfiguration;
        private readonly SitkaSmtpClientService _sitkaSmtpClient;

        public LandUseBlockUploadBackgroundJob(NeptuneDbContext dbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService)
        {
            _dbContext = dbContext;
            _neptuneJobConfiguration = neptuneJobConfiguration.Value;
            _sitkaSmtpClient = sitkaSmtpClientService;
        }

        public async Task RunJob(int personID)
        {
            var person = People.GetByID(_dbContext, personID);

            if (person == null)
            {
                throw new InvalidOperationException("PersonID must be valid!");
            }
            try
            {
                var landUseBlockStagings = LandUseBlockStagings.ListByPersonID(_dbContext, personID);
                var stormwaterJurisdictions = _dbContext.StormwaterJurisdictions.Include(x => x.Organization).Include(x => x.StormwaterJurisdictionGeometry).AsNoTracking().ToDictionary(x => x.Organization.OrganizationName);
                var editableStormwaterJurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, person).ToList();

                var stormwaterJurisdictionNames = stormwaterJurisdictions.Select(x => x.Key).ToList();
                var allowedStormwaterJurisdictionNames = string.Join(", ", stormwaterJurisdictionNames.ToList());
                var allowedPermitTypeNames = string.Join(", ", PermitType.All.Select(x => x.PermitTypeDisplayName).ToList());
                var allowedPriorityLandUseTypeNames = string.Join(", ", PriorityLandUseType.All.Select(x => x.PriorityLandUseTypeDisplayName).ToList());

                var count = 0;
                var errorList = new List<string>();
                var landUseBlocksToUpload = new List<LandUseBlock>();

                foreach (var landUseBlockStaging in landUseBlockStagings)
                {
                    var landUseBlock = new LandUseBlock();
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

                    if (!stormwaterJurisdictionNames
                        .Contains(stormwaterJurisdictionName))
                    {

                        errorList.Add(
                            $"The Stormwater Jurisdiction '{stormwaterJurisdictionName}' at row {count} was not found. Acceptable values are {allowedStormwaterJurisdictionNames}");
                    }
                    else
                    {
                        var stormwaterJurisdictionIDToAssign = stormwaterJurisdictions.ContainsKey(landUseBlockStaging.StormwaterJurisdiction) ? stormwaterJurisdictions[landUseBlockStaging.StormwaterJurisdiction] : null;
                        if (stormwaterJurisdictionIDToAssign != null && editableStormwaterJurisdictionIDs
                            .Contains(stormwaterJurisdictionIDToAssign.StormwaterJurisdictionID))
                        {
                            landUseBlock.StormwaterJurisdictionID = stormwaterJurisdictionIDToAssign.StormwaterJurisdictionID;

                            if (landUseBlockStaging.Geometry == null)
                            {
                                errorList.Add(
                                    $"The Land Use Block Geometry at row {count} is null. A value must be provided");
                            } else if (!landUseBlockStaging.Geometry.IsValid)
                            {
                                errorList.Add(
                                    $"The Land Use Block Geometry at row {count} is invalid.");
                            }
                            else
                            {

                                var clippedGeometry = landUseBlockStaging.Geometry
                                    .Intersection(stormwaterJurisdictionIDToAssign.StormwaterJurisdictionGeometry.GeometryNative);

                                if (clippedGeometry == null || clippedGeometry.IsEmpty)
                                {
                                    errorList.Add(
                                        $"The Land Use Block Geometry at row {count} is not in the assigned Stormwater Jurisdiction. Please make sure Land Use Block is in {stormwaterJurisdictionIDToAssign.Organization.OrganizationName}.");
                                }
                                else
                                {
                                    landUseBlock.LandUseBlockGeometry = clippedGeometry;
                                    landUseBlock.LandUseBlockGeometry4326 = clippedGeometry.ProjectTo4326();
                                }
                            }
                        }
                        else
                        {
                            errorList.Add(
                                $"You do not have permission to edit Stormwater Jurisdiction {stormwaterJurisdictionIDToAssign.Organization.OrganizationName}. Please remove all features with this Stormwater Jurisdiction from the upload and try again.");
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
                    var stormwaterJurisdictionToClears = stormwaterJurisdictions.Values
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

                        _dbContext.Database.SetCommandTimeout(960);
                        await _dbContext.Database.ExecuteSqlRawAsync(nullOutTGULandUseBlockIDs);
                        await _dbContext.Database.ExecuteSqlRawAsync(nullOutTGU4326LandUseBlockIDs);
                        await _dbContext.Database.ExecuteSqlRawAsync(deleteLandUseBlocks);
                    }

                    await _dbContext.LandUseBlocks.AddRangeAsync(landUseBlocksToUpload);
                    await _dbContext.SaveChangesAsync();

                    var body = "Your Land Use Block Upload has been processed. The updated Land Use Blocks are now in the Orange County Stormwater Tools system. It may take up to 24 hours for updated Trash Results to appear in the system.";

                    var mailMessage = new MailMessage
                    {
                        Subject = "Land Use Block Upload Results",
                        Body = body,
                        From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                    };

                    mailMessage.To.Add(person.Email);
                    await _sitkaSmtpClient.Send(mailMessage);
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
                        From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                    };

                    mailMessage.To.Add(person.Email);
                    await _sitkaSmtpClient.Send(mailMessage);
                }

                _dbContext.Database.ExecuteSqlRaw($"EXEC dbo.pLandUseBlockStagingDeleteByPersonID @PersonID = {personID}");
            }
            catch (Exception)
            {
                var body =
                    "There was an unexpected system error during processing of your Land Use Block Upload. The Orange County Stormwater Tools development team will investigate and be in touch when this issue is resolved.";

                var mailMessage = new MailMessage
                {
                    Subject = "Land Use Block Upload Error",
                    Body = body,
                    From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                };

                mailMessage.To.Add(person.Email);
                await _sitkaSmtpClient.Send(mailMessage);

                throw;
            }
        }
    }
}