//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class LandUseBlockStagingExtensionMethods
    {

        public static LandUseBlockStagingSimpleDto AsSimpleDto(this LandUseBlockStaging landUseBlockStaging)
        {
            var landUseBlockStagingSimpleDto = new LandUseBlockStagingSimpleDto()
            {
                LandUseBlockStagingID = landUseBlockStaging.LandUseBlockStagingID,
                PriorityLandUseType = landUseBlockStaging.PriorityLandUseType,
                LandUseDescription = landUseBlockStaging.LandUseDescription,
                TrashGenerationRate = landUseBlockStaging.TrashGenerationRate,
                LandUseForTGR = landUseBlockStaging.LandUseForTGR,
                MedianHouseholdIncome = landUseBlockStaging.MedianHouseholdIncome,
                StormwaterJurisdiction = landUseBlockStaging.StormwaterJurisdiction,
                PermitType = landUseBlockStaging.PermitType,
                UploadedByPersonID = landUseBlockStaging.UploadedByPersonID
            };
            DoCustomSimpleDtoMappings(landUseBlockStaging, landUseBlockStagingSimpleDto);
            return landUseBlockStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(LandUseBlockStaging landUseBlockStaging, LandUseBlockStagingSimpleDto landUseBlockStagingSimpleDto);
    }
}