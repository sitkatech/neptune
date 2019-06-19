//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DelineationStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DelineationStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DelineationStaging>
    {
        public DelineationStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationStagingPrimaryKey(DelineationStaging delineationStaging) : base(delineationStaging){}

        public static implicit operator DelineationStagingPrimaryKey(int primaryKeyValue)
        {
            return new DelineationStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationStagingPrimaryKey(DelineationStaging delineationStaging)
        {
            return new DelineationStagingPrimaryKey(delineationStaging);
        }
    }
}