using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

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

            var landUseBlockStagings = person.LandUseBlockStagingsWhereYouAreTheUploadedByPerson;
            var stormwaterJurisdictions = DbContext.StormwaterJurisdictions.ToList();
            var stormwaterJurisdictionsPersonCanEdit = person.GetStormwaterJurisdictionsPersonCanEditWithContext(DbContext).ToList();

            var allowedStormwaterJurisdictionNames = string.Join(", ", stormwaterJurisdictions.Select(x => x.Organization.OrganizationName).ToList());
            var allowedPermitTypeNames = string.Join(", ", PermitType.All.Select(x => x.PermitTypeDisplayName).ToList());
            var allowedPriorityLandUseTypeNames = string.Join(", ", PriorityLandUseType.All.Select(x => x.PriorityLandUseTypeDisplayName).ToList());

            var count = 0;
            var errorList = new List<string>();
            var landUseBlocksToUpload = new List<LandUseBlock>();

            foreach (var landUseBlockStaging in landUseBlockStagings)
            {
                var landUseBlock = new LandUseBlock(default(int), default(int), string.Empty, default(DbGeometry), default(decimal), string.Empty, default(decimal), default(decimal), default(int), default(int));


                var landUseBlockPLUType = landUseBlockStaging.PriorityLandUseType;
                if (!PriorityLandUseType.All.Select(x => x.PriorityLandUseTypeDisplayName).Contains(landUseBlockPLUType))
                {
                    
                    errorList.Add($"The Priority Land Use Type '{landUseBlockPLUType}' at row {count} was not found. Acceptable values are {allowedPriorityLandUseTypeNames}");
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
                    errorList.Add($"The Stormwater Jurisdiction at row {count} is null, empty or whitespace. A value must be provided");
                }
                if (!stormwaterJurisdictions.Select(x => x.Organization.OrganizationName).Contains(stormwaterJurisdictionName))
                {
                    
                    errorList.Add($"The Stormwater Jurisdiction '{stormwaterJurisdictionName}' at row {count} was not found. Acceptable values are {allowedStormwaterJurisdictionNames}");
                }
                else
                {
                    var stormwaterJurisdiction = stormwaterJurisdictions
                        .Single(x => x.Organization.OrganizationName == landUseBlockStaging.StormwaterJurisdiction);
                    if (stormwaterJurisdictionsPersonCanEdit.Select(x=>x.StormwaterJurisdictionID).Contains(stormwaterJurisdiction.StormwaterJurisdictionID))
                    {
                        landUseBlock.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
                    }
                    else
                    {
                        errorList.Add($"You do not have permission to edit Stormwater Jurisdiction {stormwaterJurisdiction.Organization.OrganizationName}. Please remove all features with this Stormwater Jurisdiction from the upload and try again.");
                    }
                }

                if (landUseBlockStaging.LandUseBlockStagingGeometry == null)
                {
                    errorList.Add($"The Land Use Block Geometry at row {count} is null. A value must be provided");
                }
                else
                {
                    var stormwaterJurisdiction = stormwaterJurisdictions
                        .Single(x => x.StormwaterJurisdictionID == landUseBlock.StormwaterJurisdictionID);
                    var clippedGeometry = landUseBlock.LandUseBlockGeometry = landUseBlockStaging.LandUseBlockStagingGeometry.Intersection(stormwaterJurisdiction.StormwaterJurisdictionGeometry);

                    if (clippedGeometry.IsEmpty)
                    {
                        errorList.Add($"The Clipped Land Use Block Geometry at row {count} is null. Please make sure Land Use Block is in the '{stormwaterJurisdiction}' stormwater jurisdiction");
                    }

                }

                var permitType = landUseBlockStaging.PermitType;
                if (string.IsNullOrEmpty(permitType))
                {
                    errorList.Add($"The Permit Type at row {count} is null, empty or whitespace. A value must be provided");
                }

                if (!PermitType.All.Select(x => x.PermitTypeDisplayName).Contains(landUseBlockStaging.PermitType))
                {
                    errorList.Add($"The Permit Type '{permitType}' at row {count} was not found. Acceptable values are {allowedPermitTypeNames}");
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
                HttpRequestStorage.DatabaseEntities.LandUseBlocks.AddRange(landUseBlocksToUpload);
                HttpRequestStorage.DatabaseEntities.SaveChanges(person);
            }
            else
            {
                //email notification would be nice since right now the errors just disappear.
            }

            HttpRequestStorage.DatabaseEntities.LandUseBlockStagings.DeleteLandUseBlockStaging(landUseBlockStagings);
        }
    }
}