//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DroolToolWatershed
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DroolToolWatershedPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DroolToolWatershed>
    {
        public DroolToolWatershedPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DroolToolWatershedPrimaryKey(DroolToolWatershed droolToolWatershed) : base(droolToolWatershed){}

        public static implicit operator DroolToolWatershedPrimaryKey(int primaryKeyValue)
        {
            return new DroolToolWatershedPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DroolToolWatershedPrimaryKey(DroolToolWatershed droolToolWatershed)
        {
            return new DroolToolWatershedPrimaryKey(droolToolWatershed);
        }
    }
}