//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LandUseBlockGeometryStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class LandUseBlockGeometryStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LandUseBlockGeometryStaging>
    {
        public LandUseBlockGeometryStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LandUseBlockGeometryStagingPrimaryKey(LandUseBlockGeometryStaging landUseBlockGeometryStaging) : base(landUseBlockGeometryStaging){}

        public static implicit operator LandUseBlockGeometryStagingPrimaryKey(int primaryKeyValue)
        {
            return new LandUseBlockGeometryStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LandUseBlockGeometryStagingPrimaryKey(LandUseBlockGeometryStaging landUseBlockGeometryStaging)
        {
            return new LandUseBlockGeometryStagingPrimaryKey(landUseBlockGeometryStaging);
        }
    }
}