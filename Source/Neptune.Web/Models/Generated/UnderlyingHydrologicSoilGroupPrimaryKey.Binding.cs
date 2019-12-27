//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.UnderlyingHydrologicSoilGroup
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class UnderlyingHydrologicSoilGroupPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<UnderlyingHydrologicSoilGroup>
    {
        public UnderlyingHydrologicSoilGroupPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public UnderlyingHydrologicSoilGroupPrimaryKey(UnderlyingHydrologicSoilGroup underlyingHydrologicSoilGroup) : base(underlyingHydrologicSoilGroup){}

        public static implicit operator UnderlyingHydrologicSoilGroupPrimaryKey(int primaryKeyValue)
        {
            return new UnderlyingHydrologicSoilGroupPrimaryKey(primaryKeyValue);
        }

        public static implicit operator UnderlyingHydrologicSoilGroupPrimaryKey(UnderlyingHydrologicSoilGroup underlyingHydrologicSoilGroup)
        {
            return new UnderlyingHydrologicSoilGroupPrimaryKey(underlyingHydrologicSoilGroup);
        }
    }
}