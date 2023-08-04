//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeCustomAttributeType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPTypeCustomAttributeTypeExtensionMethods
    {
        public static TreatmentBMPTypeCustomAttributeTypeDto AsDto(this TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            var treatmentBMPTypeCustomAttributeTypeDto = new TreatmentBMPTypeCustomAttributeTypeDto()
            {
                TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID,
                TreatmentBMPType = treatmentBMPTypeCustomAttributeType.TreatmentBMPType.AsDto(),
                CustomAttributeType = treatmentBMPTypeCustomAttributeType.CustomAttributeType.AsDto(),
                SortOrder = treatmentBMPTypeCustomAttributeType.SortOrder
            };
            DoCustomMappings(treatmentBMPTypeCustomAttributeType, treatmentBMPTypeCustomAttributeTypeDto);
            return treatmentBMPTypeCustomAttributeTypeDto;
        }

        static partial void DoCustomMappings(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType, TreatmentBMPTypeCustomAttributeTypeDto treatmentBMPTypeCustomAttributeTypeDto);

        public static TreatmentBMPTypeCustomAttributeTypeSimpleDto AsSimpleDto(this TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            var treatmentBMPTypeCustomAttributeTypeSimpleDto = new TreatmentBMPTypeCustomAttributeTypeSimpleDto()
            {
                TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID,
                TreatmentBMPTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeID,
                CustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID,
                SortOrder = treatmentBMPTypeCustomAttributeType.SortOrder
            };
            DoCustomSimpleDtoMappings(treatmentBMPTypeCustomAttributeType, treatmentBMPTypeCustomAttributeTypeSimpleDto);
            return treatmentBMPTypeCustomAttributeTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType, TreatmentBMPTypeCustomAttributeTypeSimpleDto treatmentBMPTypeCustomAttributeTypeSimpleDto);
    }
}