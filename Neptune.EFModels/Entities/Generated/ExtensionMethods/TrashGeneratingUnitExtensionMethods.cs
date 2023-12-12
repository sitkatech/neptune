//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TrashGeneratingUnitExtensionMethods
    {

        public static TrashGeneratingUnitSimpleDto AsSimpleDto(this TrashGeneratingUnit trashGeneratingUnit)
        {
            var trashGeneratingUnitSimpleDto = new TrashGeneratingUnitSimpleDto()
            {
                TrashGeneratingUnitID = trashGeneratingUnit.TrashGeneratingUnitID,
                StormwaterJurisdictionID = trashGeneratingUnit.StormwaterJurisdictionID,
                OnlandVisualTrashAssessmentAreaID = trashGeneratingUnit.OnlandVisualTrashAssessmentAreaID,
                LandUseBlockID = trashGeneratingUnit.LandUseBlockID,
                LastUpdateDate = trashGeneratingUnit.LastUpdateDate,
                DelineationID = trashGeneratingUnit.DelineationID,
                WaterQualityManagementPlanID = trashGeneratingUnit.WaterQualityManagementPlanID
            };
            DoCustomSimpleDtoMappings(trashGeneratingUnit, trashGeneratingUnitSimpleDto);
            return trashGeneratingUnitSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TrashGeneratingUnit trashGeneratingUnit, TrashGeneratingUnitSimpleDto trashGeneratingUnitSimpleDto);
    }
}