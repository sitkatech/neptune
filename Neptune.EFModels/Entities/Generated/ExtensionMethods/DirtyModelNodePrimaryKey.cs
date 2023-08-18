//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DirtyModelNode


namespace Neptune.EFModels.Entities
{
    public class DirtyModelNodePrimaryKey : EntityPrimaryKey<DirtyModelNode>
    {
        public DirtyModelNodePrimaryKey() : base(){}
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