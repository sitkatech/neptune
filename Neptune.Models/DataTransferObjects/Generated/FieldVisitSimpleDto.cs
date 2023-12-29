//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]

namespace Neptune.Models.DataTransferObjects
{
    public partial class FieldVisitSimpleDto
    {
        public int FieldVisitID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int FieldVisitStatusID { get; set; }
        public int PerformedByPersonID { get; set; }
        public DateTime VisitDate { get; set; }
        public bool InventoryUpdated { get; set; }
        public int FieldVisitTypeID { get; set; }
        public bool IsFieldVisitVerified { get; set; }
    }
}