//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPModelingTypeExtensionMethods
    {
        public static TreatmentBMPModelingTypeDto AsDto(this TreatmentBMPModelingType treatmentBMPModelingType)
        {
            var treatmentBMPModelingTypeDto = new TreatmentBMPModelingTypeDto()
            {
                TreatmentBMPModelingTypeID = treatmentBMPModelingType.TreatmentBMPModelingTypeID,
                TreatmentBMPModelingTypeName = treatmentBMPModelingType.TreatmentBMPModelingTypeName,
                TreatmentBMPModelingTypeDisplayName = treatmentBMPModelingType.TreatmentBMPModelingTypeDisplayName
            };
            DoCustomMappings(treatmentBMPModelingType, treatmentBMPModelingTypeDto);
            return treatmentBMPModelingTypeDto;
        }

        static partial void DoCustomMappings(TreatmentBMPModelingType treatmentBMPModelingType, TreatmentBMPModelingTypeDto treatmentBMPModelingTypeDto);

        public static TreatmentBMPModelingTypeSimpleDto AsSimpleDto(this TreatmentBMPModelingType treatmentBMPModelingType)
        {
            var treatmentBMPModelingTypeSimpleDto = new TreatmentBMPModelingTypeSimpleDto()
            {
                TreatmentBMPModelingTypeID = treatmentBMPModelingType.TreatmentBMPModelingTypeID,
                TreatmentBMPModelingTypeName = treatmentBMPModelingType.TreatmentBMPModelingTypeName,
                TreatmentBMPModelingTypeDisplayName = treatmentBMPModelingType.TreatmentBMPModelingTypeDisplayName
            };
            DoCustomSimpleDtoMappings(treatmentBMPModelingType, treatmentBMPModelingTypeSimpleDto);
            return treatmentBMPModelingTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPModelingType treatmentBMPModelingType, TreatmentBMPModelingTypeSimpleDto treatmentBMPModelingTypeSimpleDto);
    }
}