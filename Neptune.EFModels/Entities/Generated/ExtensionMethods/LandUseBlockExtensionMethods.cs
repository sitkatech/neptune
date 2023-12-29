//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class LandUseBlockExtensionMethods
    {
        public static LandUseBlockSimpleDto AsSimpleDto(this LandUseBlock landUseBlock)
        {
            var dto = new LandUseBlockSimpleDto()
            {
                LandUseBlockID = landUseBlock.LandUseBlockID,
                PriorityLandUseTypeID = landUseBlock.PriorityLandUseTypeID,
                LandUseDescription = landUseBlock.LandUseDescription,
                TrashGenerationRate = landUseBlock.TrashGenerationRate,
                LandUseForTGR = landUseBlock.LandUseForTGR,
                MedianHouseholdIncomeResidential = landUseBlock.MedianHouseholdIncomeResidential,
                MedianHouseholdIncomeRetail = landUseBlock.MedianHouseholdIncomeRetail,
                StormwaterJurisdictionID = landUseBlock.StormwaterJurisdictionID,
                PermitTypeID = landUseBlock.PermitTypeID
            };
            return dto;
        }
    }
}