//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PriorityLandUseType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class PriorityLandUseTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PriorityLandUseType>
    {
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