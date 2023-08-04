//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]
using System;


namespace Neptune.Models.DataTransferObjects
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
        public System.Int32 TreatmentBMPID { get; set; }
        public System.Int32 FieldVisitStatusID { get; set; }
        public System.Int32 PerformedByPersonID { get; set; }
        public DateTime VisitDate { get; set; }
        public bool InventoryUpdated { get; set; }
        public System.Int32 FieldVisitTypeID { get; set; }
        public bool IsFieldVisitVerified { get; set; }
    }

}