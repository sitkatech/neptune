//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class DelineationDto
    {
        public int DelineationID { get; set; }
        public DelineationTypeDto DelineationType { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateLastVerified { get; set; }
        public PersonDto VerifiedByPerson { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public DateTime DateLastModified { get; set; }
        public bool HasDiscrepancies { get; set; }
    }

    public partial class DelineationSimpleDto
    {
        public int DelineationID { get; set; }
        public System.Int32 DelineationTypeID { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateLastVerified { get; set; }
        public System.Int32? VerifiedByPersonID { get; set; }
        public System.Int32 TreatmentBMPID { get; set; }
        public DateTime DateLastModified { get; set; }
        public bool HasDiscrepancies { get; set; }
    }

}