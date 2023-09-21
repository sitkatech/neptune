//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int OrganizationID { get; set; }
        public int StateProvinceID { get; set; }
        public int StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }
        public int StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }
    }

}