//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LandUseBlockGeomteryStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class LandUseBlockGeomteryStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LandUseBlockGeomteryStaging>
    {
        public LandUseBlockGeomteryStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LandUseBlockGeomteryStagingPrimaryKey(LandUseBlockGeomteryStaging landUseBlockGeomteryStaging) : base(landUseBlockGeomteryStaging){}

        public static implicit operator LandUseBlockGeomteryStagingPrimaryKey(int primaryKeyValue)
        {
            return new LandUseBlockGeomteryStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LandUseBlockGeomteryStagingPrimaryKey(LandUseBlockGeomteryStaging landUseBlockGeomteryStaging)
        {
            return new LandUseBlockGeomteryStagingPrimaryKey(landUseBlockGeomteryStaging);
        }
    }
}