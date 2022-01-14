//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HydromodificationAppliesType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class HydromodificationAppliesTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<HydromodificationAppliesType>
    {
        public HydromodificationAppliesTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HydromodificationAppliesTypePrimaryKey(HydromodificationAppliesType hydromodificationAppliesType) : base(hydromodificationAppliesType){}

        public static implicit operator HydromodificationAppliesTypePrimaryKey(int primaryKeyValue)
        {
            return new HydromodificationAppliesTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator HydromodificationAppliesTypePrimaryKey(HydromodificationAppliesType hydromodificationAppliesType)
        {
            return new HydromodificationAppliesTypePrimaryKey(hydromodificationAppliesType);
        }
    }
}