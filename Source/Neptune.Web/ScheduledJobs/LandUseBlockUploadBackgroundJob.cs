using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.GdalOgr;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class LandUseBlockUploadBackgroundJob : ScheduledBackgroundJobBase
    {
        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        

        protected override void RunJobImplementation()
        {
            var landUseBlockGeometryStaging = HttpRequestStorage.DatabaseEntities.LandUseBlockGeometryStagings.FirstOrDefault();
            if (landUseBlockGeometryStaging == null)
            {
                return;
            }

            var person = landUseBlockGeometryStaging.Person;

            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable, Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId, NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds*10);
            ogr2OgrCommandLineRunner.ImportGeoJsonToMsSql(
                landUseBlockGeometryStaging.LandUseBlockGeometryStagingGeoJson,
                NeptuneWebConfiguration.DatabaseConnectionString, "LandUseBlockStaging", "Select LU_for_TGR as PriorityLandUseType, LU_Descr as LandUseDescription, TGR as TrashGenerationRate, LU_for_TGR as LandUseForTGR, MHI as MedianHouseHoldIncome, Jurisdic as StormwaterJurisdiction, Permit as PermitType", false);

            var landUseBlockStagings = HttpRequestStorage.DatabaseEntities.LandUseBlockStagings.ToList();
            var stormwaterJurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList();


            var count = 0;
            var errorList = new List<string> ();
            var landUseBlocksToUpload = new List<LandUseBlock>();
            var landUseBlockSimple = new LandUseBlock(default(int), default(int), string.Empty, default(DbGeometry), default(decimal), string.Empty, default(decimal), default(decimal), default(int), default(int));
            foreach (var landUseBlock in landUseBlockStagings)
            {
                var landUseBlockPLUType = landUseBlock.PriorityLandUseType;
                if (!PriorityLandUseType.All.Select(x => x.PriorityLandUseTypeDisplayName).Contains(landUseBlockPLUType))
                {
                    errorList.Add($"The Priority Land Use Type '{landUseBlockPLUType}' at row {count} was not found. Acceptable values are {string.Join(", ", PriorityLandUseType.All.Select(x => x.PriorityLandUseTypeDisplayName).ToList())}");
                }
                else
                {
                    landUseBlockSimple.PriorityLandUseTypeID = PriorityLandUseType.All
                        .Single(x => x.PriorityLandUseTypeDisplayName == landUseBlockPLUType).PriorityLandUseTypeID;
                }

                landUseBlockSimple.LandUseDescription = landUseBlock.LandUseDescription;

                if (landUseBlock.LandUseBlockStagingGeometry == null)
                {
                    errorList.Add($"The Land Use Block Geometry at row {count} is null. A value must be provided");
                }
                else
                {
                    landUseBlockSimple.LandUseBlockGeometry = landUseBlock.LandUseBlockStagingGeometry;
                }
                
                landUseBlockSimple.TrashGenerationRate = landUseBlock.TrashGenerationRate;
                landUseBlockSimple.LandUseForTGR = landUseBlock.LandUseForTGR;

                if (landUseBlock.LandUseForTGR == "Residential")
                {
                    landUseBlockSimple.MedianHouseholdIncomeResidential = landUseBlock.MedianHouseholdIncome;
                    landUseBlockSimple.MedianHouseholdIncomeRetail = 0;
                }else if (landUseBlock.LandUseForTGR == "Retail")
                {
                    landUseBlockSimple.MedianHouseholdIncomeResidential = 0;
                    landUseBlockSimple.MedianHouseholdIncomeRetail = landUseBlock.MedianHouseholdIncome;
                }
                else
                {
                    landUseBlockSimple.MedianHouseholdIncomeResidential = 0;
                    landUseBlockSimple.MedianHouseholdIncomeRetail = 0;
                }

                var stormwaterJurisdiction = landUseBlock.StormwaterJurisdiction;
                if (string.IsNullOrEmpty(stormwaterJurisdiction))
                {
                    errorList.Add($"The Stormwater Jurisdiction at row {count} is null, empty or whitespace. A value must be provided");
                }
                if (!stormwaterJurisdictions.Select(x => x.Organization.OrganizationName).Contains(stormwaterJurisdiction))
                {
                    errorList.Add($"The Stormwater Jurisdiction '{stormwaterJurisdiction}' at row {count} was not found. Acceptable values are {string.Join(", ", stormwaterJurisdictions.Select(x => x.Organization.OrganizationName).ToList())}");
                }
                else
                {
                    landUseBlockSimple.StormwaterJurisdictionID = stormwaterJurisdictions
                        .Single(x => x.Organization.OrganizationName == landUseBlock.StormwaterJurisdiction)
                        .StormwaterJurisdictionID;
                }

                var permitType = landUseBlock.PermitType;
                if (string.IsNullOrEmpty(permitType))
                {
                    errorList.Add($"The Permit Type at row {count} is null, empty or whitespace. A value must be provided");
                }
                if (!PermitType.All.Select(x => x.PermitTypeDisplayName).Contains(landUseBlock.PermitType))
                {
                    errorList.Add($"The Permit Type '{permitType}' at row {count} was not found. Acceptable values are {string.Join(", ", PermitType.All.Select(x => x.PermitTypeDisplayName).ToList())}");
                }
                else
                {
                    landUseBlockSimple.PermitTypeID =
                        PermitType.All.Single(x => x.PermitTypeDisplayName == permitType).PermitTypeID;
                }
                landUseBlocksToUpload.Add(landUseBlockSimple);
                count++;
            }

            var message = "";
            if (errorList.Any())
            {
                message =
                    $"Unfortunately your upload of {count} Land Use Blocks was unable to loaded into the Land Use Block table, a report of the errors is provided below: ";
                //sendMessage(person, message, errorList);
            }
            else
            {
                HttpRequestStorage.DatabaseEntities.LandUseBlocks.AddRange(landUseBlocksToUpload);
                HttpRequestStorage.DatabaseEntities.SaveChanges(person);
                message =
                    $"Congratulations, {count} Land Use Blocks were successfully uploaded into the Land Use Block table.";
                //sendMessage(person, message, errorList);
            }

            throw new NotImplementedException();
        }
    }
}