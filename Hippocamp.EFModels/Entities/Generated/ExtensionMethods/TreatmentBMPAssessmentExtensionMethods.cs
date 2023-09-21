//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentExtensionMethods
    {
        public static TreatmentBMPAssessmentDto AsDto(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPAssessmentDto = new TreatmentBMPAssessmentDto()
            {
                TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID,
                TreatmentBMP = treatmentBMPAssessment.TreatmentBMP.AsDto(),
                TreatmentBMPType = treatmentBMPAssessment.TreatmentBMPType.AsDto(),
                FieldVisit = treatmentBMPAssessment.FieldVisit.AsDto(),
                TreatmentBMPAssessmentType = treatmentBMPAssessment.TreatmentBMPAssessmentType.AsDto(),
                Notes = treatmentBMPAssessment.Notes,
                AssessmentScore = treatmentBMPAssessment.AssessmentScore,
                IsAssessmentComplete = treatmentBMPAssessment.IsAssessmentComplete
            };
            DoCustomMappings(treatmentBMPAssessment, treatmentBMPAssessmentDto);
            return treatmentBMPAssessmentDto;
        }

        static partial void DoCustomMappings(TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPAssessmentDto treatmentBMPAssessmentDto);

        public static TreatmentBMPAssessmentSimpleDto AsSimpleDto(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPAssessmentSimpleDto = new TreatmentBMPAssessmentSimpleDto()
            {
                TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID,
                TreatmentBMPID = treatmentBMPAssessment.TreatmentBMPID,
                TreatmentBMPTypeID = treatmentBMPAssessment.TreatmentBMPTypeID,
                FieldVisitID = treatmentBMPAssessment.FieldVisitID,
                TreatmentBMPAssessmentTypeID = treatmentBMPAssessment.TreatmentBMPAssessmentTypeID,
                Notes = treatmentBMPAssessment.Notes,
                AssessmentScore = treatmentBMPAssessment.AssessmentScore,
                IsAssessmentComplete = treatmentBMPAssessment.IsAssessmentComplete
            };
            DoCustomSimpleDtoMappings(treatmentBMPAssessment, treatmentBMPAssessmentSimpleDto);
            return treatmentBMPAssessmentSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPAssessmentSimpleDto treatmentBMPAssessmentSimpleDto);
    }
}