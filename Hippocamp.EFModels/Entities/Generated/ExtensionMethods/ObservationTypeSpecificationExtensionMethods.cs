//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ObservationTypeSpecificationExtensionMethods
    {
        public static ObservationTypeSpecificationDto AsDto(this ObservationTypeSpecification observationTypeSpecification)
        {
            var observationTypeSpecificationDto = new ObservationTypeSpecificationDto()
            {
                ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID,
                ObservationTypeSpecificationName = observationTypeSpecification.ObservationTypeSpecificationName,
                ObservationTypeSpecificationDisplayName = observationTypeSpecification.ObservationTypeSpecificationDisplayName,
                SortOrder = observationTypeSpecification.SortOrder,
                ObservationTypeCollectionMethod = observationTypeSpecification.ObservationTypeCollectionMethod.AsDto(),
                ObservationTargetType = observationTypeSpecification.ObservationTargetType.AsDto(),
                ObservationThresholdType = observationTypeSpecification.ObservationThresholdType.AsDto()
            };
            DoCustomMappings(observationTypeSpecification, observationTypeSpecificationDto);
            return observationTypeSpecificationDto;
        }

        static partial void DoCustomMappings(ObservationTypeSpecification observationTypeSpecification, ObservationTypeSpecificationDto observationTypeSpecificationDto);

        public static ObservationTypeSpecificationSimpleDto AsSimpleDto(this ObservationTypeSpecification observationTypeSpecification)
        {
            var observationTypeSpecificationSimpleDto = new ObservationTypeSpecificationSimpleDto()
            {
                ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID,
                ObservationTypeSpecificationName = observationTypeSpecification.ObservationTypeSpecificationName,
                ObservationTypeSpecificationDisplayName = observationTypeSpecification.ObservationTypeSpecificationDisplayName,
                SortOrder = observationTypeSpecification.SortOrder,
                ObservationTypeCollectionMethodID = observationTypeSpecification.ObservationTypeCollectionMethodID,
                ObservationTargetTypeID = observationTypeSpecification.ObservationTargetTypeID,
                ObservationThresholdTypeID = observationTypeSpecification.ObservationThresholdTypeID
            };
            DoCustomSimpleDtoMappings(observationTypeSpecification, observationTypeSpecificationSimpleDto);
            return observationTypeSpecificationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ObservationTypeSpecification observationTypeSpecification, ObservationTypeSpecificationSimpleDto observationTypeSpecificationSimpleDto);
    }
}