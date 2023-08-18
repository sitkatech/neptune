//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TrashGeneratingUnitAdjustment


namespace Neptune.EFModels.Entities
{
    public class TrashGeneratingUnitAdjustmentPrimaryKey : EntityPrimaryKey<TrashGeneratingUnitAdjustment>
    {
        public TrashGeneratingUnitAdjustmentPrimaryKey() : base(){}
        public TrashGeneratingUnitAdjustmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TrashGeneratingUnitAdjustmentPrimaryKey(TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustment) : base(trashGeneratingUnitAdjustment){}

        public static implicit operator TrashGeneratingUnitAdjustmentPrimaryKey(int primaryKeyValue)
        {
            return new TrashGeneratingUnitAdjustmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TrashGeneratingUnitAdjustmentPrimaryKey(TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustment)
        {
            return new TrashGeneratingUnitAdjustmentPrimaryKey(trashGeneratingUnitAdjustment);
        }
    }
}