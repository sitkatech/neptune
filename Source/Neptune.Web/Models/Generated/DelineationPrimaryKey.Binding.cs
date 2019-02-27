//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Delineation
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DelineationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<Delineation>
    {
        public DelineationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationPrimaryKey(Delineation delineation) : base(delineation){}

        public static implicit operator DelineationPrimaryKey(int primaryKeyValue)
        {
            return new DelineationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationPrimaryKey(Delineation delineation)
        {
            return new DelineationPrimaryKey(delineation);
        }
    }
}