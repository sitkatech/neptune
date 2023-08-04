//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationStaging]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class DelineationStagingDto
    {
        public int DelineationStagingID { get; set; }
        public PersonDto UploadedByPerson { get; set; }
        public string TreatmentBMPName { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
    }

    public partial class DelineationStagingSimpleDto
    {
        public int DelineationStagingID { get; set; }
        public System.Int32 UploadedByPersonID { get; set; }
        public string TreatmentBMPName { get; set; }
        public System.Int32 StormwaterJurisdictionID { get; set; }
    }

}