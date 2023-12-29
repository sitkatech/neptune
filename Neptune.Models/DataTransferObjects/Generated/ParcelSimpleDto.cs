//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Parcel]

namespace Neptune.Models.DataTransferObjects
{
    public partial class ParcelSimpleDto
    {
        public int ParcelID { get; set; }
        public string ParcelNumber { get; set; }
        public string OwnerName { get; set; }
        public string ParcelStreetNumber { get; set; }
        public string ParcelAddress { get; set; }
        public string ParcelZipCode { get; set; }
        public string LandUse { get; set; }
        public int? SquareFeetHome { get; set; }
        public int? SquareFeetLot { get; set; }
        public double ParcelAreaInAcres { get; set; }
    }
}