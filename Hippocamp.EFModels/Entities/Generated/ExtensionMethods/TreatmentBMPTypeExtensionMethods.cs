//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPTypeExtensionMethods
    {
        public static TreatmentBMPTypeDto AsDto(this TreatmentBMPType treatmentBMPType)
        {
            var treatmentBMPTypeDto = new TreatmentBMPTypeDto()
            {
                TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
                TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName,
                TreatmentBMPTypeDescription = treatmentBMPType.TreatmentBMPTypeDescription,
                IsAnalyzedInModelingModule = treatmentBMPType.IsAnalyzedInModelingModule,
                TreatmentBMPModelingType = treatmentBMPType.TreatmentBMPModelingType?.AsDto()
            };
            DoCustomMappings(treatmentBMPType, treatmentBMPTypeDto);
            return treatmentBMPTypeDto;
        }

        static partial void DoCustomMappings(TreatmentBMPType treatmentBMPType, TreatmentBMPTypeDto treatmentBMPTypeDto);

        public static TreatmentBMPTypeSimpleDto AsSimpleDto(this TreatmentBMPType treatmentBMPType)
        {
            var treatmentBMPTypeSimpleDto = new TreatmentBMPTypeSimpleDto()
            {
                TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
                TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName,
                TreatmentBMPTypeDescription = treatmentBMPType.TreatmentBMPTypeDescription,
                IsAnalyzedInModelingModule = treatmentBMPType.IsAnalyzedInModelingModule,
                TreatmentBMPModelingTypeID = treatmentBMPType.TreatmentBMPModelingTypeID
            };
            DoCustomSimpleDtoMappings(treatmentBMPType, treatmentBMPTypeSimpleDto);
            return treatmentBMPTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPType treatmentBMPType, TreatmentBMPTypeSimpleDto treatmentBMPTypeSimpleDto);
    }
}