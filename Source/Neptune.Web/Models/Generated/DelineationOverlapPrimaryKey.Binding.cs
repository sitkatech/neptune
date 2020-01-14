//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DelineationOverlap
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DelineationOverlapPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DelineationOverlap>
    {
        public DelineationOverlapPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationOverlapPrimaryKey(DelineationOverlap delineationOverlap) : base(delineationOverlap){}

        public static implicit operator DelineationOverlapPrimaryKey(int primaryKeyValue)
        {
            return new DelineationOverlapPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationOverlapPrimaryKey(DelineationOverlap delineationOverlap)
        {
            return new DelineationOverlapPrimaryKey(delineationOverlap);
        }
    }
}