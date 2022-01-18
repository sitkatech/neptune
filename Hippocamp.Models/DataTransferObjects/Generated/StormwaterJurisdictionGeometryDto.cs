//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionGeometry]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class StormwaterJurisdictionGeometryDto
    {
        public int StormwaterJurisdictionGeometryID { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
    }

    public partial class StormwaterJurisdictionGeometrySimpleDto
    {
        public int StormwaterJurisdictionGeometryID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
    }

}