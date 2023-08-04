//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class RegionalSubbasinRevisionRequestDto
    {
        public int RegionalSubbasinRevisionRequestID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public PersonDto RequestPerson { get; set; }
        public RegionalSubbasinRevisionRequestStatusDto RegionalSubbasinRevisionRequestStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public PersonDto ClosedByPerson { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string Notes { get; set; }
        public string CloseNotes { get; set; }
    }

    public partial class RegionalSubbasinRevisionRequestSimpleDto
    {
        public int RegionalSubbasinRevisionRequestID { get; set; }
        public System.Int32 TreatmentBMPID { get; set; }
        public System.Int32 RequestPersonID { get; set; }
        public System.Int32 RegionalSubbasinRevisionRequestStatusID { get; set; }
        public DateTime RequestDate { get; set; }
        public System.Int32? ClosedByPersonID { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string Notes { get; set; }
        public string CloseNotes { get; set; }
    }

}