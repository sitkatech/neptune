//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TrashGeneratingUnitExtensionMethods
    {
        public static TrashGeneratingUnitDto AsDto(this TrashGeneratingUnit trashGeneratingUnit)
        {
            var trashGeneratingUnitDto = new TrashGeneratingUnitDto()
            {
                TrashGeneratingUnitID = trashGeneratingUnit.TrashGeneratingUnitID,
                StormwaterJurisdiction = trashGeneratingUnit.StormwaterJurisdiction.AsDto(),
                OnlandVisualTrashAssessmentAreaID = trashGeneratingUnit.OnlandVisualTrashAssessmentAreaID,
                LandUseBlock = trashGeneratingUnit.LandUseBlock?.AsDto(),
                LastUpdateDate = trashGeneratingUnit.LastUpdateDate,
                DelineationID = trashGeneratingUnit.DelineationID,
                WaterQualityManagementPlanID = trashGeneratingUnit.WaterQualityManagementPlanID
            };
            DoCustomMappings(trashGeneratingUnit, trashGeneratingUnitDto);
            return trashGeneratingUnitDto;
        }

        static partial void DoCustomMappings(TrashGeneratingUnit trashGeneratingUnit, TrashGeneratingUnitDto trashGeneratingUnitDto);

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