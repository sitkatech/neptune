//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class ObservationTypeSpecificationDto
    {
        public int ObservationTypeSpecificationID { get; set; }
        public string ObservationTypeSpecificationName { get; set; }
        public string ObservationTypeSpecificationDisplayName { get; set; }
        public ObservationTypeCollectionMethodDto ObservationTypeCollectionMethod { get; set; }
        public ObservationTargetTypeDto ObservationTargetType { get; set; }
        public ObservationThresholdTypeDto ObservationThresholdType { get; set; }
    }

    public partial class ObservationTypeSpecificationSimpleDto
    {
        public int ObservationTypeSpecificationID { get; set; }
        public string ObservationTypeSpecificationName { get; set; }
        public string ObservationTypeSpecificationDisplayName { get; set; }
        public System.Int32 ObservationTypeCollectionMethodID { get; set; }
        public System.Int32 ObservationTargetTypeID { get; set; }
        public System.Int32 ObservationThresholdTypeID { get; set; }
    }

}