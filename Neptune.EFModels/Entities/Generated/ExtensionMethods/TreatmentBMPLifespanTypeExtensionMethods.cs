//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPLifespanType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPLifespanTypeExtensionMethods
    {
        public static TreatmentBMPLifespanTypeSimpleDto AsSimpleDto(this TreatmentBMPLifespanType treatmentBMPLifespanType)
        {
            var dto = new TreatmentBMPLifespanTypeSimpleDto()
            {
                TreatmentBMPLifespanTypeID = treatmentBMPLifespanType.TreatmentBMPLifespanTypeID,
                TreatmentBMPLifespanTypeName = treatmentBMPLifespanType.TreatmentBMPLifespanTypeName,
                TreatmentBMPLifespanTypeDisplayName = treatmentBMPLifespanType.TreatmentBMPLifespanTypeDisplayName
            };
            return dto;
        }
    }
}