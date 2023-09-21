//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPLifespanType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TreatmentBMPLifespanTypeExtensionMethods
    {
        public static TreatmentBMPLifespanTypeDto AsDto(this TreatmentBMPLifespanType treatmentBMPLifespanType)
        {
            var treatmentBMPLifespanTypeDto = new TreatmentBMPLifespanTypeDto()
            {
                TreatmentBMPLifespanTypeID = treatmentBMPLifespanType.TreatmentBMPLifespanTypeID,
                TreatmentBMPLifespanTypeName = treatmentBMPLifespanType.TreatmentBMPLifespanTypeName,
                TreatmentBMPLifespanTypeDisplayName = treatmentBMPLifespanType.TreatmentBMPLifespanTypeDisplayName
            };
            DoCustomMappings(treatmentBMPLifespanType, treatmentBMPLifespanTypeDto);
            return treatmentBMPLifespanTypeDto;
        }

        static partial void DoCustomMappings(TreatmentBMPLifespanType treatmentBMPLifespanType, TreatmentBMPLifespanTypeDto treatmentBMPLifespanTypeDto);

        public static TreatmentBMPLifespanTypeSimpleDto AsSimpleDto(this TreatmentBMPLifespanType treatmentBMPLifespanType)
        {
            var treatmentBMPLifespanTypeSimpleDto = new TreatmentBMPLifespanTypeSimpleDto()
            {
                TreatmentBMPLifespanTypeID = treatmentBMPLifespanType.TreatmentBMPLifespanTypeID,
                TreatmentBMPLifespanTypeName = treatmentBMPLifespanType.TreatmentBMPLifespanTypeName,
                TreatmentBMPLifespanTypeDisplayName = treatmentBMPLifespanType.TreatmentBMPLifespanTypeDisplayName
            };
            DoCustomSimpleDtoMappings(treatmentBMPLifespanType, treatmentBMPLifespanTypeSimpleDto);
            return treatmentBMPLifespanTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TreatmentBMPLifespanType treatmentBMPLifespanType, TreatmentBMPLifespanTypeSimpleDto treatmentBMPLifespanTypeSimpleDto);
    }
}