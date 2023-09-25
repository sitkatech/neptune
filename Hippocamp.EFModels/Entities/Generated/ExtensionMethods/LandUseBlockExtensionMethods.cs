//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class LandUseBlockExtensionMethods
    {
        public static LandUseBlockDto AsDto(this LandUseBlock landUseBlock)
        {
            var landUseBlockDto = new LandUseBlockDto()
            {
                LandUseBlockID = landUseBlock.LandUseBlockID,
                PriorityLandUseType = landUseBlock.PriorityLandUseType?.AsDto(),
                LandUseDescription = landUseBlock.LandUseDescription,
                TrashGenerationRate = landUseBlock.TrashGenerationRate,
                LandUseForTGR = landUseBlock.LandUseForTGR,
                MedianHouseholdIncomeResidential = landUseBlock.MedianHouseholdIncomeResidential,
                MedianHouseholdIncomeRetail = landUseBlock.MedianHouseholdIncomeRetail,
                StormwaterJurisdiction = landUseBlock.StormwaterJurisdiction.AsDto(),
                PermitType = landUseBlock.PermitType.AsDto()
            };
            DoCustomMappings(landUseBlock, landUseBlockDto);
            return landUseBlockDto;
        }

        static partial void DoCustomMappings(LandUseBlock landUseBlock, LandUseBlockDto landUseBlockDto);

        public static LandUseBlockSimpleDto AsSimpleDto(this LandUseBlock landUseBlock)
        {
            var landUseBlockSimpleDto = new LandUseBlockSimpleDto()
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
            DoCustomSimpleDtoMappings(landUseBlock, landUseBlockSimpleDto);
            return landUseBlockSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(LandUseBlock landUseBlock, LandUseBlockSimpleDto landUseBlockSimpleDto);
    }
}