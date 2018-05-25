//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldVisitStatus
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class FieldVisitStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FieldVisitStatus>
    {
        public FieldVisitStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldVisitStatusPrimaryKey(FieldVisitStatus fieldVisitStatus) : base(fieldVisitStatus){}

        public static implicit operator FieldVisitStatusPrimaryKey(int primaryKeyValue)
        {
            return new FieldVisitStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldVisitStatusPrimaryKey(FieldVisitStatus fieldVisitStatus)
        {
            return new FieldVisitStatusPrimaryKey(fieldVisitStatus);
        }
    }
}