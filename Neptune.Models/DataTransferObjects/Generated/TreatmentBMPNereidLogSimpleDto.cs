//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPNereidLog]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPNereidLogSimpleDto
    {
        public int TreatmentBMPNereidLogID { get; set; }
        public int TreatmentBMPID { get; set; }
        public DateTime? LastRequestDate { get; set; }
        public string NereidRequest { get; set; }
        public string NereidResponse { get; set; }
    }
}