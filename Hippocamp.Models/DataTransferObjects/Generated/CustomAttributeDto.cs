//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttribute]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class CustomAttributeDto
    {
        public int CustomAttributeID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public TreatmentBMPTypeCustomAttributeTypeDto TreatmentBMPTypeCustomAttributeType { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public CustomAttributeTypeDto CustomAttributeType { get; set; }
    }

    public partial class CustomAttributeSimpleDto
    {
        public int CustomAttributeID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
    }

}