//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationStaging]

namespace Neptune.Models.DataTransferObjects
{
    public partial class DelineationStagingSimpleDto
    {
        public int DelineationStagingID { get; set; }
        public int UploadedByPersonID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string DelineationStatus { get; set; }
    }
}