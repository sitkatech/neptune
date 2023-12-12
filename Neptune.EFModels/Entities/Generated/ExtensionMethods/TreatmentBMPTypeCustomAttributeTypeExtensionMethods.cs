//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeCustomAttributeType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPTypeCustomAttributeTypeExtensionMethods
    {
        public static TreatmentBMPTypeCustomAttributeTypeSimpleDto AsSimpleDto(this TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            var dto = new TreatmentBMPTypeCustomAttributeTypeSimpleDto()
            {
                TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID,
                TreatmentBMPTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeID,
                CustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID,
                SortOrder = treatmentBMPTypeCustomAttributeType.SortOrder
            };
            return dto;
        }
    }
}