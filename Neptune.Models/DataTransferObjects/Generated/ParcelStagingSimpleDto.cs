//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelStaging]

namespace Neptune.Models.DataTransferObjects
{
    public partial class ParcelStagingSimpleDto
    {
        public int ParcelStagingID { get; set; }
        public string ParcelNumber { get; set; }
        public double ParcelAreaInSquareFeet { get; set; }
        public string ParcelAddress { get; set; }
        public string ParcelCityState { get; set; }
        public string ParcelZipCode { get; set; }
    }
}