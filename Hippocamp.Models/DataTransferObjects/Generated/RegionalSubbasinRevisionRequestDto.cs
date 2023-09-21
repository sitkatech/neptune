//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int TreatmentBMPID { get; set; }
        public int RequestPersonID { get; set; }
        public int RegionalSubbasinRevisionRequestStatusID { get; set; }
        public DateTime RequestDate { get; set; }
        public int? ClosedByPersonID { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string Notes { get; set; }
        public string CloseNotes { get; set; }
    }

}