//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LSPCBasinStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class LSPCBasinStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LSPCBasinStaging>
    {
        public LSPCBasinStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LSPCBasinStagingPrimaryKey(LSPCBasinStaging lSPCBasinStaging) : base(lSPCBasinStaging){}

        public static implicit operator LSPCBasinStagingPrimaryKey(int primaryKeyValue)
        {
            return new LSPCBasinStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LSPCBasinStagingPrimaryKey(LSPCBasinStaging lSPCBasinStaging)
        {
            return new LSPCBasinStagingPrimaryKey(lSPCBasinStaging);
        }
    }
}