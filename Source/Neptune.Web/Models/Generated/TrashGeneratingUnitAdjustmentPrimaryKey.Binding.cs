//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TrashGeneratingUnitAdjustment
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TrashGeneratingUnitAdjustmentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TrashGeneratingUnitAdjustment>
    {
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