//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttribute]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class CustomAttributeExtensionMethods
    {
        public static CustomAttributeDto AsDto(this CustomAttribute customAttribute)
        {
            var customAttributeDto = new CustomAttributeDto()
            {
                CustomAttributeID = customAttribute.CustomAttributeID,
                TreatmentBMP = customAttribute.TreatmentBMP.AsDto(),
                TreatmentBMPTypeCustomAttributeType = customAttribute.TreatmentBMPTypeCustomAttributeType.AsDto(),
                TreatmentBMPType = customAttribute.TreatmentBMPType.AsDto(),
                CustomAttributeType = customAttribute.CustomAttributeType.AsDto()
            };
            DoCustomMappings(customAttribute, customAttributeDto);
            return customAttributeDto;
        }

        static partial void DoCustomMappings(CustomAttribute customAttribute, CustomAttributeDto customAttributeDto);

        public static CustomAttributeSimpleDto AsSimpleDto(this CustomAttribute customAttribute)
        {
            var customAttributeSimpleDto = new CustomAttributeSimpleDto()
            {
                CustomAttributeID = customAttribute.CustomAttributeID,
                TreatmentBMPID = customAttribute.TreatmentBMPID,
                TreatmentBMPTypeCustomAttributeTypeID = customAttribute.TreatmentBMPTypeCustomAttributeTypeID,
                TreatmentBMPTypeID = customAttribute.TreatmentBMPTypeID,
                CustomAttributeTypeID = customAttribute.CustomAttributeTypeID
            };
            DoCustomSimpleDtoMappings(customAttribute, customAttributeSimpleDto);
            return customAttributeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(CustomAttribute customAttribute, CustomAttributeSimpleDto customAttributeSimpleDto);
    }
}