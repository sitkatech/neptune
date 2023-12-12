//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentExtensionMethods
    {

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