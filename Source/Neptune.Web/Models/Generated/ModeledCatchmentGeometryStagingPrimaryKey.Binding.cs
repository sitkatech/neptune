//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ModeledCatchmentGeometryStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ModeledCatchmentGeometryStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ModeledCatchmentGeometryStaging>
    {
        public ModeledCatchmentGeometryStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ModeledCatchmentGeometryStagingPrimaryKey(ModeledCatchmentGeometryStaging modeledCatchmentGeometryStaging) : base(modeledCatchmentGeometryStaging){}

        public static implicit operator ModeledCatchmentGeometryStagingPrimaryKey(int primaryKeyValue)
        {
            return new ModeledCatchmentGeometryStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ModeledCatchmentGeometryStagingPrimaryKey(ModeledCatchmentGeometryStaging modeledCatchmentGeometryStaging)
        {
            return new ModeledCatchmentGeometryStagingPrimaryKey(modeledCatchmentGeometryStaging);
        }
    }
}