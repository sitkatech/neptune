//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelGeometry]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class ParcelGeometryDto
    {
        public int ParcelGeometryID { get; set; }
        public ParcelDto Parcel { get; set; }
    }

    public partial class ParcelGeometrySimpleDto
    {
        public int ParcelGeometryID { get; set; }
        public System.Int32 ParcelID { get; set; }
    }

}