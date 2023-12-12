//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit4326]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TrashGeneratingUnit4326ExtensionMethods
    {

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