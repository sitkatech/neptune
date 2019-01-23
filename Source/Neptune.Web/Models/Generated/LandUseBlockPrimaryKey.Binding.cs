//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LandUseBlock
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class LandUseBlockPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LandUseBlock>
    {
        public LandUseBlockPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LandUseBlockPrimaryKey(LandUseBlock landUseBlock) : base(landUseBlock){}

        public static implicit operator LandUseBlockPrimaryKey(int primaryKeyValue)
        {
            return new LandUseBlockPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LandUseBlockPrimaryKey(LandUseBlock landUseBlock)
        {
            return new LandUseBlockPrimaryKey(landUseBlock);
        }
    }
}