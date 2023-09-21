//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationThresholdType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ObservationThresholdTypeExtensionMethods
    {
        public static ObservationThresholdTypeDto AsDto(this ObservationThresholdType observationThresholdType)
        {
            var observationThresholdTypeDto = new ObservationThresholdTypeDto()
            {
                ObservationThresholdTypeID = observationThresholdType.ObservationThresholdTypeID,
                ObservationThresholdTypeName = observationThresholdType.ObservationThresholdTypeName,
                ObservationThresholdTypeDisplayName = observationThresholdType.ObservationThresholdTypeDisplayName,
                SortOrder = observationThresholdType.SortOrder
            };
            DoCustomMappings(observationThresholdType, observationThresholdTypeDto);
            return observationThresholdTypeDto;
        }

        static partial void DoCustomMappings(ObservationThresholdType observationThresholdType, ObservationThresholdTypeDto observationThresholdTypeDto);

        public static ObservationThresholdTypeSimpleDto AsSimpleDto(this ObservationThresholdType observationThresholdType)
        {
            var observationThresholdTypeSimpleDto = new ObservationThresholdTypeSimpleDto()
            {
                ObservationThresholdTypeID = observationThresholdType.ObservationThresholdTypeID,
                ObservationThresholdTypeName = observationThresholdType.ObservationThresholdTypeName,
                ObservationThresholdTypeDisplayName = observationThresholdType.ObservationThresholdTypeDisplayName,
                SortOrder = observationThresholdType.SortOrder
            };
            DoCustomSimpleDtoMappings(observationThresholdType, observationThresholdTypeSimpleDto);
            return observationThresholdTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ObservationThresholdType observationThresholdType, ObservationThresholdTypeSimpleDto observationThresholdTypeSimpleDto);
    }
}