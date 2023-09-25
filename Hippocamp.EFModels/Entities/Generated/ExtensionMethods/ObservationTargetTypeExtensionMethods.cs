//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTargetType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ObservationTargetTypeExtensionMethods
    {
        public static ObservationTargetTypeDto AsDto(this ObservationTargetType observationTargetType)
        {
            var observationTargetTypeDto = new ObservationTargetTypeDto()
            {
                ObservationTargetTypeID = observationTargetType.ObservationTargetTypeID,
                ObservationTargetTypeName = observationTargetType.ObservationTargetTypeName,
                ObservationTargetTypeDisplayName = observationTargetType.ObservationTargetTypeDisplayName,
                SortOrder = observationTargetType.SortOrder
            };
            DoCustomMappings(observationTargetType, observationTargetTypeDto);
            return observationTargetTypeDto;
        }

        static partial void DoCustomMappings(ObservationTargetType observationTargetType, ObservationTargetTypeDto observationTargetTypeDto);

        public static ObservationTargetTypeSimpleDto AsSimpleDto(this ObservationTargetType observationTargetType)
        {
            var observationTargetTypeSimpleDto = new ObservationTargetTypeSimpleDto()
            {
                ObservationTargetTypeID = observationTargetType.ObservationTargetTypeID,
                ObservationTargetTypeName = observationTargetType.ObservationTargetTypeName,
                ObservationTargetTypeDisplayName = observationTargetType.ObservationTargetTypeDisplayName,
                SortOrder = observationTargetType.SortOrder
            };
            DoCustomSimpleDtoMappings(observationTargetType, observationTargetTypeSimpleDto);
            return observationTargetTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ObservationTargetType observationTargetType, ObservationTargetTypeSimpleDto observationTargetTypeSimpleDto);
    }
}