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
            var dto = new LandUseBlockStagingSimpleDto()
            {
                LandUseBlockStagingID = landUseBlockStaging.LandUseBlockStagingID,
                PriorityLandUseType = landUseBlockStaging.PriorityLandUseType,
                LandUseDescription = landUseBlockStaging.LandUseDescription,
                TrashGenerationRate = landUseBlockStaging.TrashGenerationRate,
                LandUseForTGR = landUseBlockStaging.LandUseForTGR,
                MedianHouseholdIncomeResidential = landUseBlockStaging.MedianHouseholdIncomeResidential,
                MedianHouseholdIncomeRetail = landUseBlockStaging.MedianHouseholdIncomeRetail,
                StormwaterJurisdictionID = landUseBlockStaging.StormwaterJurisdictionID,
                PermitType = landUseBlockStaging.PermitType,
                UploadedByPersonID = landUseBlockStaging.UploadedByPersonID
            };
            return dto;
        }
    }
}