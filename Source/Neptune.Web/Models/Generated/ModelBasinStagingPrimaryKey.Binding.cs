//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ModelBasinStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ModelBasinStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ModelBasinStaging>
    {
        public ModelBasinStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ModelBasinStagingPrimaryKey(ModelBasinStaging modelBasinStaging) : base(modelBasinStaging){}

        public static implicit operator ModelBasinStagingPrimaryKey(int primaryKeyValue)
        {
            return new ModelBasinStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ModelBasinStagingPrimaryKey(ModelBasinStaging modelBasinStaging)
        {
            return new ModelBasinStagingPrimaryKey(modelBasinStaging);
        }
    }
}