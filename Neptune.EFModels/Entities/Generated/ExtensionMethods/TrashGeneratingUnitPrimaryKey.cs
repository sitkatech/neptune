//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TrashGeneratingUnit


namespace Neptune.EFModels.Entities
{
    public class TrashGeneratingUnitPrimaryKey : EntityPrimaryKey<TrashGeneratingUnit>
    {
        public TrashGeneratingUnitPrimaryKey() : base(){}
        public TrashGeneratingUnitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TrashGeneratingUnitPrimaryKey(TrashGeneratingUnit trashGeneratingUnit) : base(trashGeneratingUnit){}

        public static implicit operator TrashGeneratingUnitPrimaryKey(int primaryKeyValue)
        {
            return new TrashGeneratingUnitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TrashGeneratingUnitPrimaryKey(TrashGeneratingUnit trashGeneratingUnit)
        {
            return new TrashGeneratingUnitPrimaryKey(trashGeneratingUnit);
        }
    }
}