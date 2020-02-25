//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterJurisdictionGeometry
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionGeometryPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterJurisdictionGeometry>
    {
        public StormwaterJurisdictionGeometryPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterJurisdictionGeometryPrimaryKey(StormwaterJurisdictionGeometry stormwaterJurisdictionGeometry) : base(stormwaterJurisdictionGeometry){}

        public static implicit operator StormwaterJurisdictionGeometryPrimaryKey(int primaryKeyValue)
        {
            return new StormwaterJurisdictionGeometryPrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterJurisdictionGeometryPrimaryKey(StormwaterJurisdictionGeometry stormwaterJurisdictionGeometry)
        {
            return new StormwaterJurisdictionGeometryPrimaryKey(stormwaterJurisdictionGeometry);
        }
    }
}