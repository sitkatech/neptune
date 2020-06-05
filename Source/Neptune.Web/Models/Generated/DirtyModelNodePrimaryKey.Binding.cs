//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DirtyModelNode
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DirtyModelNodePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DirtyModelNode>
    {
        public DirtyModelNodePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DirtyModelNodePrimaryKey(DirtyModelNode dirtyModelNode) : base(dirtyModelNode){}

        public static implicit operator DirtyModelNodePrimaryKey(int primaryKeyValue)
        {
            return new DirtyModelNodePrimaryKey(primaryKeyValue);
        }

        public static implicit operator DirtyModelNodePrimaryKey(DirtyModelNode dirtyModelNode)
        {
            return new DirtyModelNodePrimaryKey(dirtyModelNode);
        }
    }
}