//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]

namespace Neptune.Models.DataTransferObjects
{
    public partial class MaintenanceRecordSimpleDto
    {
        public int MaintenanceRecordID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int FieldVisitID { get; set; }
        public string MaintenanceRecordDescription { get; set; }
        public int? MaintenanceRecordTypeID { get; set; }
    }
}