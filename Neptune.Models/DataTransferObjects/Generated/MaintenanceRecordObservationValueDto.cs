//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservationValue]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class MaintenanceRecordObservationValueDto
    {
        public int MaintenanceRecordObservationValueID { get; set; }
        public MaintenanceRecordObservationDto MaintenanceRecordObservation { get; set; }
        public string ObservationValue { get; set; }
    }

    public partial class MaintenanceRecordObservationValueSimpleDto
    {
        public int MaintenanceRecordObservationValueID { get; set; }
        public System.Int32 MaintenanceRecordObservationID { get; set; }
        public string ObservationValue { get; set; }
    }

}