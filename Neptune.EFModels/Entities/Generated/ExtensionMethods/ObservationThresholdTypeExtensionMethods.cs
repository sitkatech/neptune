//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationThresholdType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ObservationThresholdTypeExtensionMethods
    {

        public static ObservationThresholdTypeSimpleDto AsSimpleDto(this ObservationThresholdType observationThresholdType)
        {
            var observationThresholdTypeSimpleDto = new ObservationThresholdTypeSimpleDto()
            {
                ObservationThresholdTypeID = observationThresholdType.ObservationThresholdTypeID,
                ObservationThresholdTypeName = observationThresholdType.ObservationThresholdTypeName,
                ObservationThresholdTypeDisplayName = observationThresholdType.ObservationThresholdTypeDisplayName
            };
            DoCustomSimpleDtoMappings(observationThresholdType, observationThresholdTypeSimpleDto);
            return observationThresholdTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ObservationThresholdType observationThresholdType, ObservationThresholdTypeSimpleDto observationThresholdTypeSimpleDto);
    }
}