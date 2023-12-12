//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTargetType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ObservationTargetTypeExtensionMethods
    {

        public static ObservationTargetTypeSimpleDto AsSimpleDto(this ObservationTargetType observationTargetType)
        {
            var observationTargetTypeSimpleDto = new ObservationTargetTypeSimpleDto()
            {
                ObservationTargetTypeID = observationTargetType.ObservationTargetTypeID,
                ObservationTargetTypeName = observationTargetType.ObservationTargetTypeName,
                ObservationTargetTypeDisplayName = observationTargetType.ObservationTargetTypeDisplayName
            };
            DoCustomSimpleDtoMappings(observationTargetType, observationTargetTypeSimpleDto);
            return observationTargetTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ObservationTargetType observationTargetType, ObservationTargetTypeSimpleDto observationTargetTypeSimpleDto);
    }
}