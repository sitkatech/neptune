//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPAssessmentTypeExtensionMethods
    {
        public static TreatmentBMPAssessmentTypeDto AsDto(this TreatmentBMPAssessmentType treatmentBMPAssessmentType)
        {
            var treatmentBMPAssessmentTypeDto = new TreatmentBMPAssessmentTypeDto()
            {
                TreatmentBMPAssessmentTypeID = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeID,
                TreatmentBMPAssessmentTypeName = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeName,
                TreatmentBMPAssessmentTypeDisplayName = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeDisplayName
            };
            DoCustomMappings(treatmentBMPAssessmentType, treatmentBMPAssessmentTypeDto);
            return treatmentBMPAssessmentTypeDto;
        }

        static partial void DoCustomMappings(TreatmentBMPAssessmentType treatmentBMPAssessmentType, TreatmentBMPAssessmentTypeDto treatmentBMPAssessmentTypeDto);

        public static TreatmentBMPAssessmentTypeSimpleDto AsSimpleDto(this TreatmentBMPAssessmentType treatmentBMPAssessmentType)
        {
            var treatmentBMPAssessmentTypeSimpleDto = new TreatmentBMPAssessmentTypeSimpleDto()
            {
                TreatmentBMPAssessmentTypeID = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeID,
                TreatmentBMPAssessmentTypeName = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeName,
                TreatmentBMPAssessmentTypeDisplayName = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeDisplayName
            };
            DoCustomSimpleDtoMappings(treatmentBMPAssessmentType, treatmentBMPAssessmentTypeSimpleDto);
            return treatmentBMPAssessmentTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPAssessmentType treatmentBMPAssessmentType, TreatmentBMPAssessmentTypeSimpleDto treatmentBMPAssessmentTypeSimpleDto);
    }
}