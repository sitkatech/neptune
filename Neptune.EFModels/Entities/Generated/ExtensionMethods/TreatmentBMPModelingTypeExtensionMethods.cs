//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPModelingTypeExtensionMethods
    {
        public static TreatmentBMPModelingTypeSimpleDto AsSimpleDto(this TreatmentBMPModelingType treatmentBMPModelingType)
        {
            var dto = new TreatmentBMPModelingTypeSimpleDto()
            {
                TreatmentBMPModelingTypeID = treatmentBMPModelingType.TreatmentBMPModelingTypeID,
                TreatmentBMPModelingTypeName = treatmentBMPModelingType.TreatmentBMPModelingTypeName,
                TreatmentBMPModelingTypeDisplayName = treatmentBMPModelingType.TreatmentBMPModelingTypeDisplayName
            };
            return dto;
        }
    }
}