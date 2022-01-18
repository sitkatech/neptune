//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int MaintenanceRecordID { get; set; }
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
    }

}