//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeCustomAttributeType]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPTypeCustomAttributeTypeDto
    {
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public CustomAttributeTypeDto CustomAttributeType { get; set; }
        public int? SortOrder { get; set; }
    }

    public partial class TreatmentBMPTypeCustomAttributeTypeSimpleDto
    {
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 CustomAttributeTypeID { get; set; }
        public int? SortOrder { get; set; }
    }

}