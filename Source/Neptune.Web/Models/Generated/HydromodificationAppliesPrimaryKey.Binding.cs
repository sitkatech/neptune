//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HydromodificationApplies
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class HydromodificationAppliesPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<HydromodificationApplies>
    {
        public HydromodificationAppliesPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HydromodificationAppliesPrimaryKey(HydromodificationApplies hydromodificationApplies) : base(hydromodificationApplies){}

        public static implicit operator HydromodificationAppliesPrimaryKey(int primaryKeyValue)
        {
            return new HydromodificationAppliesPrimaryKey(primaryKeyValue);
        }

        public static implicit operator HydromodificationAppliesPrimaryKey(HydromodificationApplies hydromodificationApplies)
        {
            return new HydromodificationAppliesPrimaryKey(hydromodificationApplies);
        }
    }
}