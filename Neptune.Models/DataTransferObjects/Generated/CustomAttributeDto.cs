//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttribute]
using System;


namespace Neptune.Models.DataTransferObjects
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
        public System.Int32 TreatmentBMPID { get; set; }
        public System.Int32 TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 CustomAttributeTypeID { get; set; }
    }

}