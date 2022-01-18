//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessment]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class OnlandVisualTrashAssessmentExtensionMethods
    {
        public static OnlandVisualTrashAssessmentDto AsDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            var onlandVisualTrashAssessmentDto = new OnlandVisualTrashAssessmentDto()
            {
                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
                CreatedByPerson = onlandVisualTrashAssessment.CreatedByPerson.AsDto(),
                CreatedDate = onlandVisualTrashAssessment.CreatedDate,
                OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea?.AsDto(),
                Notes = onlandVisualTrashAssessment.Notes,
                StormwaterJurisdiction = onlandVisualTrashAssessment.StormwaterJurisdiction.AsDto(),
                AssessingNewArea = onlandVisualTrashAssessment.AssessingNewArea,
                OnlandVisualTrashAssessmentStatus = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus.AsDto(),
                IsDraftGeometryManuallyRefined = onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined,
                OnlandVisualTrashAssessmentScore = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScore?.AsDto(),
                CompletedDate = onlandVisualTrashAssessment.CompletedDate,
                DraftAreaName = onlandVisualTrashAssessment.DraftAreaName,
                DraftAreaDescription = onlandVisualTrashAssessment.DraftAreaDescription,
                IsTransectBackingAssessment = onlandVisualTrashAssessment.IsTransectBackingAssessment,
                IsProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment
            };
            DoCustomMappings(onlandVisualTrashAssessment, onlandVisualTrashAssessmentDto);
            return onlandVisualTrashAssessmentDto;
        }

        static partial void DoCustomMappings(OnlandVisualTrashAssessment onlandVisualTrashAssessment, OnlandVisualTrashAssessmentDto onlandVisualTrashAssessmentDto);

        public static OnlandVisualTrashAssessmentSimpleDto AsSimpleDto(this OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            var onlandVisualTrashAssessmentSimpleDto = new OnlandVisualTrashAssessmentSimpleDto()
            {
                OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
                CreatedByPersonID = onlandVisualTrashAssessment.CreatedByPersonID,
                CreatedDate = onlandVisualTrashAssessment.CreatedDate,
                OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID,
                Notes = onlandVisualTrashAssessment.Notes,
                StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID,
                AssessingNewArea = onlandVisualTrashAssessment.AssessingNewArea,
                OnlandVisualTrashAssessmentStatusID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID,
                IsDraftGeometryManuallyRefined = onlandVisualTrashAssessment.IsDraftGeometryManuallyRefined,
                OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID,
                CompletedDate = onlandVisualTrashAssessment.CompletedDate,
                DraftAreaName = onlandVisualTrashAssessment.DraftAreaName,
                DraftAreaDescription = onlandVisualTrashAssessment.DraftAreaDescription,
                IsTransectBackingAssessment = onlandVisualTrashAssessment.IsTransectBackingAssessment,
                IsProgressAssessment = onlandVisualTrashAssessment.IsProgressAssessment
            };
            DoCustomSimpleDtoMappings(onlandVisualTrashAssessment, onlandVisualTrashAssessmentSimpleDto);
            return onlandVisualTrashAssessmentSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OnlandVisualTrashAssessment onlandVisualTrashAssessment, OnlandVisualTrashAssessmentSimpleDto onlandVisualTrashAssessmentSimpleDto);
    }
}