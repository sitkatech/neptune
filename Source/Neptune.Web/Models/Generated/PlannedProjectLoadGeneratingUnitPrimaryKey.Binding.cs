//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PlannedProjectLoadGeneratingUnit
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class PlannedProjectLoadGeneratingUnitPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PlannedProjectLoadGeneratingUnit>
    {
        public PlannedProjectLoadGeneratingUnitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PlannedProjectLoadGeneratingUnitPrimaryKey(PlannedProjectLoadGeneratingUnit plannedProjectLoadGeneratingUnit) : base(plannedProjectLoadGeneratingUnit){}

        public static implicit operator PlannedProjectLoadGeneratingUnitPrimaryKey(int primaryKeyValue)
        {
            return new PlannedProjectLoadGeneratingUnitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PlannedProjectLoadGeneratingUnitPrimaryKey(PlannedProjectLoadGeneratingUnit plannedProjectLoadGeneratingUnit)
        {
            return new PlannedProjectLoadGeneratingUnitPrimaryKey(plannedProjectLoadGeneratingUnit);
        }
    }
}