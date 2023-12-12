//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPTypeExtensionMethods
    {

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