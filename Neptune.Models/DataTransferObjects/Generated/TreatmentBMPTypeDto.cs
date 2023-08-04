//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPTypeDto
    {
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public string TreatmentBMPTypeDescription { get; set; }
        public bool IsAnalyzedInModelingModule { get; set; }
        public TreatmentBMPModelingTypeDto TreatmentBMPModelingType { get; set; }
    }

    public partial class TreatmentBMPTypeSimpleDto
    {
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public string TreatmentBMPTypeDescription { get; set; }
        public bool IsAnalyzedInModelingModule { get; set; }
        public System.Int32? TreatmentBMPModelingTypeID { get; set; }
    }

}