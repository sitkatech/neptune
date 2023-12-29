//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ObservationTypeSpecificationExtensionMethods
    {
        public static ObservationTypeSpecificationSimpleDto AsSimpleDto(this ObservationTypeSpecification observationTypeSpecification)
        {
            var dto = new ObservationTypeSpecificationSimpleDto()
            {
                ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID,
                ObservationTypeSpecificationName = observationTypeSpecification.ObservationTypeSpecificationName,
                ObservationTypeSpecificationDisplayName = observationTypeSpecification.ObservationTypeSpecificationDisplayName,
                ObservationTypeCollectionMethodID = observationTypeSpecification.ObservationTypeCollectionMethodID,
                ObservationTargetTypeID = observationTypeSpecification.ObservationTargetTypeID,
                ObservationThresholdTypeID = observationTypeSpecification.ObservationThresholdTypeID
            };
            return dto;
        }
    }
}