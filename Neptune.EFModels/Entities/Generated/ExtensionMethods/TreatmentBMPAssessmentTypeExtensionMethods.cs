//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentTypeExtensionMethods
    {
        public static TreatmentBMPAssessmentTypeSimpleDto AsSimpleDto(this TreatmentBMPAssessmentType treatmentBMPAssessmentType)
        {
            var dto = new TreatmentBMPAssessmentTypeSimpleDto()
            {
                TreatmentBMPAssessmentTypeID = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeID,
                TreatmentBMPAssessmentTypeName = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeName,
                TreatmentBMPAssessmentTypeDisplayName = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeDisplayName
            };
            return dto;
        }
    }
}