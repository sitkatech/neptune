//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class FieldVisitDto
    {
        public int FieldVisitID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public FieldVisitStatusDto FieldVisitStatus { get; set; }
        public PersonDto PerformedByPerson { get; set; }
        public DateTime VisitDate { get; set; }
        public bool InventoryUpdated { get; set; }
        public FieldVisitTypeDto FieldVisitType { get; set; }
        public bool IsFieldVisitVerified { get; set; }
    }

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