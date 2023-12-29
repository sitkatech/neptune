//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]

namespace Neptune.Models.DataTransferObjects
{
    public partial class DelineationSimpleDto
    {
        public int DelineationID { get; set; }
        public int DelineationTypeID { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateLastVerified { get; set; }
        public int? VerifiedByPersonID { get; set; }
        public int TreatmentBMPID { get; set; }
        public DateTime DateLastModified { get; set; }
        public bool HasDiscrepancies { get; set; }
    }
}