//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.FieldVisitSection
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class FieldVisitSectionPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<FieldVisitSection>
    {
        public FieldVisitSectionPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public FieldVisitSectionPrimaryKey(FieldVisitSection fieldVisitSection) : base(fieldVisitSection){}

        public static implicit operator FieldVisitSectionPrimaryKey(int primaryKeyValue)
        {
            return new FieldVisitSectionPrimaryKey(primaryKeyValue);
        }

        public static implicit operator FieldVisitSectionPrimaryKey(FieldVisitSection fieldVisitSection)
        {
            return new FieldVisitSectionPrimaryKey(fieldVisitSection);
        }
    }
}