//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ObservationTypeSpecificationDto
    {
        public int ObservationTypeSpecificationID { get; set; }
        public string ObservationTypeSpecificationName { get; set; }
        public string ObservationTypeSpecificationDisplayName { get; set; }
        public int SortOrder { get; set; }
        public ObservationTypeCollectionMethodDto ObservationTypeCollectionMethod { get; set; }
        public ObservationTargetTypeDto ObservationTargetType { get; set; }
        public ObservationThresholdTypeDto ObservationThresholdType { get; set; }
    }

    public partial class ObservationTypeSpecificationSimpleDto
    {
        public int ObservationTypeSpecificationID { get; set; }
        public string ObservationTypeSpecificationName { get; set; }
        public string ObservationTypeSpecificationDisplayName { get; set; }
        public int SortOrder { get; set; }
        public int ObservationTypeCollectionMethodID { get; set; }
        public int ObservationTargetTypeID { get; set; }
        public int ObservationThresholdTypeID { get; set; }
    }

}