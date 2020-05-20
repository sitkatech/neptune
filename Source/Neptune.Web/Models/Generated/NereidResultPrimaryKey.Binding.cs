//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NereidResult
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NereidResultPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NereidResult>
    {
        public NereidResultPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NereidResultPrimaryKey(NereidResult nereidResult) : base(nereidResult){}

        public static implicit operator NereidResultPrimaryKey(int primaryKeyValue)
        {
            return new NereidResultPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NereidResultPrimaryKey(NereidResult nereidResult)
        {
            return new NereidResultPrimaryKey(nereidResult);
        }
    }
}