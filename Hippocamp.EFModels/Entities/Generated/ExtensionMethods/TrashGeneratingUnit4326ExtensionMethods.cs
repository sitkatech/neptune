//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit4326]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TrashGeneratingUnit4326ExtensionMethods
    {
        public static TrashGeneratingUnit4326Dto AsDto(this TrashGeneratingUnit4326 trashGeneratingUnit4326)
        {
            var trashGeneratingUnit4326Dto = new TrashGeneratingUnit4326Dto()
            {
                TrashGeneratingUnit4326ID = trashGeneratingUnit4326.TrashGeneratingUnit4326ID,
                StormwaterJurisdiction = trashGeneratingUnit4326.StormwaterJurisdiction.AsDto(),
                OnlandVisualTrashAssessmentAreaID = trashGeneratingUnit4326.OnlandVisualTrashAssessmentAreaID,
                LandUseBlock = trashGeneratingUnit4326.LandUseBlock?.AsDto(),
                LastUpdateDate = trashGeneratingUnit4326.LastUpdateDate,
                DelineationID = trashGeneratingUnit4326.DelineationID,
                WaterQualityManagementPlanID = trashGeneratingUnit4326.WaterQualityManagementPlanID
            };
            DoCustomMappings(trashGeneratingUnit4326, trashGeneratingUnit4326Dto);
            return trashGeneratingUnit4326Dto;
        }

        static partial void DoCustomMappings(TrashGeneratingUnit4326 trashGeneratingUnit4326, TrashGeneratingUnit4326Dto trashGeneratingUnit4326Dto);

        public static TrashGeneratingUnit4326SimpleDto AsSimpleDto(this TrashGeneratingUnit4326 trashGeneratingUnit4326)
        {
            var trashGeneratingUnit4326SimpleDto = new TrashGeneratingUnit4326SimpleDto()
            {
                TrashGeneratingUnit4326ID = trashGeneratingUnit4326.TrashGeneratingUnit4326ID,
                StormwaterJurisdictionID = trashGeneratingUnit4326.StormwaterJurisdictionID,
                OnlandVisualTrashAssessmentAreaID = trashGeneratingUnit4326.OnlandVisualTrashAssessmentAreaID,
                LandUseBlockID = trashGeneratingUnit4326.LandUseBlockID,
                LastUpdateDate = trashGeneratingUnit4326.LastUpdateDate,
                DelineationID = trashGeneratingUnit4326.DelineationID,
                WaterQualityManagementPlanID = trashGeneratingUnit4326.WaterQualityManagementPlanID
            };
            DoCustomSimpleDtoMappings(trashGeneratingUnit4326, trashGeneratingUnit4326SimpleDto);
            return trashGeneratingUnit4326SimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TrashGeneratingUnit4326 trashGeneratingUnit4326, TrashGeneratingUnit4326SimpleDto trashGeneratingUnit4326SimpleDto);
    }
}