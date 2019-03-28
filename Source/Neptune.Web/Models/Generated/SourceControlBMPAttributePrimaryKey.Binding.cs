//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SourceControlBMPAttribute
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class SourceControlBMPAttributePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<SourceControlBMPAttribute>
    {
        public SourceControlBMPAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SourceControlBMPAttributePrimaryKey(SourceControlBMPAttribute sourceControlBMPAttribute) : base(sourceControlBMPAttribute){}

        public static implicit operator SourceControlBMPAttributePrimaryKey(int primaryKeyValue)
        {
            return new SourceControlBMPAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator SourceControlBMPAttributePrimaryKey(SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            return new SourceControlBMPAttributePrimaryKey(sourceControlBMPAttribute);
        }
    }
}