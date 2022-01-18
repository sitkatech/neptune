//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeCollectionMethod]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ObservationTypeCollectionMethodExtensionMethods
    {
        public static ObservationTypeCollectionMethodDto AsDto(this ObservationTypeCollectionMethod observationTypeCollectionMethod)
        {
            var observationTypeCollectionMethodDto = new ObservationTypeCollectionMethodDto()
            {
                ObservationTypeCollectionMethodID = observationTypeCollectionMethod.ObservationTypeCollectionMethodID,
                ObservationTypeCollectionMethodName = observationTypeCollectionMethod.ObservationTypeCollectionMethodName,
                ObservationTypeCollectionMethodDisplayName = observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName,
                SortOrder = observationTypeCollectionMethod.SortOrder,
                ObservationTypeCollectionMethodDescription = observationTypeCollectionMethod.ObservationTypeCollectionMethodDescription
            };
            DoCustomMappings(observationTypeCollectionMethod, observationTypeCollectionMethodDto);
            return observationTypeCollectionMethodDto;
        }

        static partial void DoCustomMappings(ObservationTypeCollectionMethod observationTypeCollectionMethod, ObservationTypeCollectionMethodDto observationTypeCollectionMethodDto);

        public static ObservationTypeCollectionMethodSimpleDto AsSimpleDto(this ObservationTypeCollectionMethod observationTypeCollectionMethod)
        {
            var observationTypeCollectionMethodSimpleDto = new ObservationTypeCollectionMethodSimpleDto()
            {
                ObservationTypeCollectionMethodID = observationTypeCollectionMethod.ObservationTypeCollectionMethodID,
                ObservationTypeCollectionMethodName = observationTypeCollectionMethod.ObservationTypeCollectionMethodName,
                ObservationTypeCollectionMethodDisplayName = observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName,
                SortOrder = observationTypeCollectionMethod.SortOrder,
                ObservationTypeCollectionMethodDescription = observationTypeCollectionMethod.ObservationTypeCollectionMethodDescription
            };
            DoCustomSimpleDtoMappings(observationTypeCollectionMethod, observationTypeCollectionMethodSimpleDto);
            return observationTypeCollectionMethodSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ObservationTypeCollectionMethod observationTypeCollectionMethod, ObservationTypeCollectionMethodSimpleDto observationTypeCollectionMethodSimpleDto);
    }
}