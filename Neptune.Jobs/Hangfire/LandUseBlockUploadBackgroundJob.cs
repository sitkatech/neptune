using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public class LandUseBlockUploadBackgroundJob(
        NeptuneDbContext dbContext,
        IOptions<NeptuneJobConfiguration> neptuneJobConfiguration,
        SitkaSmtpClientService sitkaSmtpClientService)
    {
        private readonly NeptuneJobConfiguration _neptuneJobConfiguration = neptuneJobConfiguration.Value;

        public async Task RunJob(int personID)
        {
            var person = People.GetByID(dbContext, personID);

            if (person == null)
            {
                throw new InvalidOperationException("PersonID must be valid!");
            }
            try
            {
                var landUseBlockStagings = LandUseBlockStagings.ListByPersonID(dbContext, personID);
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
                        .Contains(landUseBlockPLUType, StringComparer.InvariantCultureIgnoreCase))
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
                    landUseBlock.LandUseBlockGeometry = landUseBlockStaging.Geometry;
                    landUseBlock.LandUseBlockGeometry4326 = landUseBlockStaging.Geometry.ProjectTo4326();
                    landUseBlock.StormwaterJurisdictionID = landUseBlockStaging.StormwaterJurisdictionID;

                    var landUseForTGR = landUseBlockStaging.LandUseForTGR?.ToUpper();
                    if (landUseForTGR == "RESIDENTIAL")
                    {
                        landUseBlock.MedianHouseholdIncomeResidential = landUseBlockStaging.MedianHouseholdIncomeResidential;
                        landUseBlock.MedianHouseholdIncomeRetail = 0;
                    }
                    else if (landUseForTGR == "RETAIL")
                    {
                        landUseBlock.MedianHouseholdIncomeResidential = 0;
                        landUseBlock.MedianHouseholdIncomeRetail = landUseBlockStaging.MedianHouseholdIncomeRetail;
                    }
                    else
                    {
                        landUseBlock.MedianHouseholdIncomeResidential = 0;
                        landUseBlock.MedianHouseholdIncomeRetail = 0;
                    }
                    
                    var permitType = landUseBlockStaging.PermitType;
                    if (string.IsNullOrEmpty(permitType))
                    {
                        errorList.Add(
                            $"The Permit Type at row {count} is null, empty or whitespace. A value must be provided");
                    }

                    if (!PermitType.All.Select(x => x.PermitTypeDisplayName).Contains(landUseBlockStaging.PermitType, StringComparer.InvariantCultureIgnoreCase))
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
                    await dbContext.TrashGeneratingUnits
                        .Where(x => stormwaterJurisdictionIDsToClear.Contains(x.StormwaterJurisdictionID))
                        .ExecuteUpdateAsync(x => x.SetProperty(y => y.LandUseBlockID, (int?)null));
                    await dbContext.TrashGeneratingUnit4326s
                        .Where(x => stormwaterJurisdictionIDsToClear.Contains(x.StormwaterJurisdictionID))
                        .ExecuteUpdateAsync(x => x.SetProperty(y => y.LandUseBlockID, (int?)null));
                    await dbContext.LandUseBlocks
                        .Where(x => stormwaterJurisdictionIDsToClear.Contains(x.StormwaterJurisdictionID))
                        .ExecuteDeleteAsync();

                    await dbContext.LandUseBlocks.AddRangeAsync(landUseBlocksToUpload);
                    await dbContext.SaveChangesAsync();

                    var body = "Your Land Use Block Upload has been processed. The updated Land Use Blocks are now in the Orange County Stormwater Tools system. It may take up to 24 hours for updated Trash Results to appear in the system.";

                    var mailMessage = new MailMessage
                    {
                        Subject = "Land Use Block Upload Results",
                        Body = body,
                        From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail, "Orange County Stormwater Tools")
                    };

                    mailMessage.To.Add(person.Email);
                    await sitkaSmtpClientService.Send(mailMessage);
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
                    await sitkaSmtpClientService.Send(mailMessage);
                }

                await dbContext.Database.ExecuteSqlRawAsync($"EXEC dbo.pLandUseBlockStagingDeleteByPersonID @PersonID = {personID}");
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
                await sitkaSmtpClientService.Send(mailMessage);

                throw;
            }
        }
    }
}