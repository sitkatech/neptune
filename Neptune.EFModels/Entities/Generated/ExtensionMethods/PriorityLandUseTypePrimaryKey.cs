//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PriorityLandUseType


namespace Neptune.EFModels.Entities
{
    public class PriorityLandUseTypePrimaryKey : EntityPrimaryKey<PriorityLandUseType>
    {
        public PriorityLandUseTypePrimaryKey() : base(){}
        public PriorityLandUseTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PriorityLandUseTypePrimaryKey(PriorityLandUseType priorityLandUseType) : base(priorityLandUseType){}

        public static implicit operator PriorityLandUseTypePrimaryKey(int primaryKeyValue)
        {
            return new PriorityLandUseTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PriorityLandUseTypePrimaryKey(PriorityLandUseType priorityLandUseType)
        {
            return new PriorityLandUseTypePrimaryKey(priorityLandUseType);
        }
    }
}