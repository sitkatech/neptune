//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeCollectionMethod]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ObservationTypeCollectionMethodExtensionMethods
    {

        public static ObservationTypeCollectionMethodSimpleDto AsSimpleDto(this ObservationTypeCollectionMethod observationTypeCollectionMethod)
        {
            var observationTypeCollectionMethodSimpleDto = new ObservationTypeCollectionMethodSimpleDto()
            {
                ObservationTypeCollectionMethodID = observationTypeCollectionMethod.ObservationTypeCollectionMethodID,
                ObservationTypeCollectionMethodName = observationTypeCollectionMethod.ObservationTypeCollectionMethodName,
                ObservationTypeCollectionMethodDisplayName = observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName,
                ObservationTypeCollectionMethodDescription = observationTypeCollectionMethod.ObservationTypeCollectionMethodDescription
            };
            DoCustomSimpleDtoMappings(observationTypeCollectionMethod, observationTypeCollectionMethodSimpleDto);
            return observationTypeCollectionMethodSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ObservationTypeCollectionMethod observationTypeCollectionMethod, ObservationTypeCollectionMethodSimpleDto observationTypeCollectionMethodSimpleDto);
    }
}