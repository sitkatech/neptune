//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPTypeSimpleDto
    {
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public string TreatmentBMPTypeDescription { get; set; }
        public bool IsAnalyzedInModelingModule { get; set; }
        public int? TreatmentBMPModelingTypeID { get; set; }
    }
}