//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DelineationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DelineationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DelineationType>
    {
        public DelineationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationTypePrimaryKey(DelineationType delineationType) : base(delineationType){}

        public static implicit operator DelineationTypePrimaryKey(int primaryKeyValue)
        {
            return new DelineationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationTypePrimaryKey(DelineationType delineationType)
        {
            return new DelineationTypePrimaryKey(delineationType);
        }
    }
}