//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldVisit
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class FieldVisitPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FieldVisit>
    {
        public FieldVisitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldVisitPrimaryKey(FieldVisit fieldVisit) : base(fieldVisit){}

        public static implicit operator FieldVisitPrimaryKey(int primaryKeyValue)
        {
            return new FieldVisitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldVisitPrimaryKey(FieldVisit fieldVisit)
        {
            return new FieldVisitPrimaryKey(fieldVisit);
        }
    }
}