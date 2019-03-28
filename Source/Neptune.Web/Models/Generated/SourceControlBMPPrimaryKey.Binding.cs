//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SourceControlBMP
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class SourceControlBMPPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<SourceControlBMP>
    {
        public SourceControlBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SourceControlBMPPrimaryKey(SourceControlBMP sourceControlBMP) : base(sourceControlBMP){}

        public static implicit operator SourceControlBMPPrimaryKey(int primaryKeyValue)
        {
            return new SourceControlBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator SourceControlBMPPrimaryKey(SourceControlBMP sourceControlBMP)
        {
            return new SourceControlBMPPrimaryKey(sourceControlBMP);
        }
    }
}