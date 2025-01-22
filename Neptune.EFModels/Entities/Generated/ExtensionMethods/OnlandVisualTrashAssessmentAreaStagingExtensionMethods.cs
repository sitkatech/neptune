//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentAreaStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentAreaStagingExtensionMethods
    {
        public static OnlandVisualTrashAssessmentAreaStagingSimpleDto AsSimpleDto(this OnlandVisualTrashAssessmentAreaStaging onlandVisualTrashAssessmentAreaStaging)
        {
            var dto = new OnlandVisualTrashAssessmentAreaStagingSimpleDto()
            {
                OnlandVisualTrashAssessmentAreaStagingID = onlandVisualTrashAssessmentAreaStaging.OnlandVisualTrashAssessmentAreaStagingID,
                AreaName = onlandVisualTrashAssessmentAreaStaging.AreaName,
                StormwaterJurisdictionID = onlandVisualTrashAssessmentAreaStaging.StormwaterJurisdictionID,
                Description = onlandVisualTrashAssessmentAreaStaging.Description,
                UploadedByPersonID = onlandVisualTrashAssessmentAreaStaging.UploadedByPersonID
            };
            return dto;
        }
    }
}