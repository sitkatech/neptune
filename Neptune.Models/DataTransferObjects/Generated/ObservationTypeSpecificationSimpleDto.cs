//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]

namespace Neptune.Models.DataTransferObjects
{
    public partial class ObservationTypeSpecificationSimpleDto
    {
        public int ObservationTypeSpecificationID { get; set; }
        public string ObservationTypeSpecificationName { get; set; }
        public string ObservationTypeSpecificationDisplayName { get; set; }
        public int ObservationTypeCollectionMethodID { get; set; }
        public int ObservationTargetTypeID { get; set; }
        public int ObservationThresholdTypeID { get; set; }
    }
}