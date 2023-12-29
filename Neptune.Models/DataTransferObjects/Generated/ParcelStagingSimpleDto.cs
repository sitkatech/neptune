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
        public double ParcelStagingAreaSquareFeet { get; set; }
        public string OwnerName { get; set; }
        public string ParcelStreetNumber { get; set; }
        public string ParcelAddress { get; set; }
        public string ParcelZipCode { get; set; }
        public string LandUse { get; set; }
        public int? SquareFeetHome { get; set; }
        public int? SquareFeetLot { get; set; }
        public int UploadedByPersonID { get; set; }
    }
}