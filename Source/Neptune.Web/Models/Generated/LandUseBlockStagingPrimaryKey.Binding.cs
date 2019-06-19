//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LandUseBlockStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class LandUseBlockStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LandUseBlockStaging>
    {
        public LandUseBlockStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LandUseBlockStagingPrimaryKey(LandUseBlockStaging landUseBlockStaging) : base(landUseBlockStaging){}

        public static implicit operator LandUseBlockStagingPrimaryKey(int primaryKeyValue)
        {
            return new LandUseBlockStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LandUseBlockStagingPrimaryKey(LandUseBlockStaging landUseBlockStaging)
        {
            return new LandUseBlockStagingPrimaryKey(landUseBlockStaging);
        }
    }
}