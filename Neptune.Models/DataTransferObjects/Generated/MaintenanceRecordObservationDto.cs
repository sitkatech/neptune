//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class MaintenanceRecordObservationDto
    {
        public int MaintenanceRecordObservationID { get; set; }
        public MaintenanceRecordDto MaintenanceRecord { get; set; }
        public TreatmentBMPTypeCustomAttributeTypeDto TreatmentBMPTypeCustomAttributeType { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public CustomAttributeTypeDto CustomAttributeType { get; set; }
    }

    public partial class MaintenanceRecordObservationSimpleDto
    {
        public int MaintenanceRecordObservationID { get; set; }
        public System.Int32 MaintenanceRecordID { get; set; }
        public System.Int32 TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 CustomAttributeTypeID { get; set; }
    }

}