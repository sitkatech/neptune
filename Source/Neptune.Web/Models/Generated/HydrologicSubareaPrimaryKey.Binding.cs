//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HydrologicSubarea
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class HydrologicSubareaPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<HydrologicSubarea>
    {
        public HydrologicSubareaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HydrologicSubareaPrimaryKey(HydrologicSubarea hydrologicSubarea) : base(hydrologicSubarea){}

        public static implicit operator HydrologicSubareaPrimaryKey(int primaryKeyValue)
        {
            return new HydrologicSubareaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator HydrologicSubareaPrimaryKey(HydrologicSubarea hydrologicSubarea)
        {
            return new HydrologicSubareaPrimaryKey(hydrologicSubarea);
        }
    }
}