//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class StormwaterJurisdictionDto
    {
        public int StormwaterJurisdictionID { get; set; }
        public OrganizationDto Organization { get; set; }
        public StateProvinceDto StateProvince { get; set; }
        public StormwaterJurisdictionPublicBMPVisibilityTypeDto StormwaterJurisdictionPublicBMPVisibilityType { get; set; }
        public StormwaterJurisdictionPublicWQMPVisibilityTypeDto StormwaterJurisdictionPublicWQMPVisibilityType { get; set; }
    }

    public partial class StormwaterJurisdictionSimpleDto
    {
        public int StormwaterJurisdictionID { get; set; }
        public System.Int32 OrganizationID { get; set; }
        public System.Int32 StateProvinceID { get; set; }
        public System.Int32 StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }
        public System.Int32 StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }
    }

}