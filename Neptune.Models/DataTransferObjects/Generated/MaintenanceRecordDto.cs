//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class MaintenanceRecordDto
    {
        public int MaintenanceRecordID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public FieldVisitDto FieldVisit { get; set; }
        public string MaintenanceRecordDescription { get; set; }
        public MaintenanceRecordTypeDto MaintenanceRecordType { get; set; }
    }

    public partial class MaintenanceRecordSimpleDto
    {
        public int MaintenanceRecordID { get; set; }
        public System.Int32 TreatmentBMPID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 FieldVisitID { get; set; }
        public string MaintenanceRecordDescription { get; set; }
        public System.Int32? MaintenanceRecordTypeID { get; set; }
    }

}