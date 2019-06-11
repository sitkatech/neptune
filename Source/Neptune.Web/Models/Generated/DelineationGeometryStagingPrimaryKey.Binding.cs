//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DelineationGeometryStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DelineationGeometryStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DelineationGeometryStaging>
    {
        public DelineationGeometryStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationGeometryStagingPrimaryKey(DelineationGeometryStaging delineationGeometryStaging) : base(delineationGeometryStaging){}

        public static implicit operator DelineationGeometryStagingPrimaryKey(int primaryKeyValue)
        {
            return new DelineationGeometryStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationGeometryStagingPrimaryKey(DelineationGeometryStaging delineationGeometryStaging)
        {
            return new DelineationGeometryStagingPrimaryKey(delineationGeometryStaging);
        }
    }
}