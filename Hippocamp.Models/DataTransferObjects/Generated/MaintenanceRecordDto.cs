//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int FieldVisitID { get; set; }
        public string MaintenanceRecordDescription { get; set; }
        public int? MaintenanceRecordTypeID { get; set; }
    }

}