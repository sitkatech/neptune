//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LSPCBasin
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class LSPCBasinPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<LSPCBasin>
    {
        public LSPCBasinPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LSPCBasinPrimaryKey(LSPCBasin lSPCBasin) : base(lSPCBasin){}

        public static implicit operator LSPCBasinPrimaryKey(int primaryKeyValue)
        {
            return new LSPCBasinPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LSPCBasinPrimaryKey(LSPCBasin lSPCBasin)
        {
            return new LSPCBasinPrimaryKey(lSPCBasin);
        }
    }
}