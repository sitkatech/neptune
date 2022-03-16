//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PlannedProjectNereidResult
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class PlannedProjectNereidResultPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PlannedProjectNereidResult>
    {
        public PlannedProjectNereidResultPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PlannedProjectNereidResultPrimaryKey(PlannedProjectNereidResult plannedProjectNereidResult) : base(plannedProjectNereidResult){}

        public static implicit operator PlannedProjectNereidResultPrimaryKey(int primaryKeyValue)
        {
            return new PlannedProjectNereidResultPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PlannedProjectNereidResultPrimaryKey(PlannedProjectNereidResult plannedProjectNereidResult)
        {
            return new PlannedProjectNereidResultPrimaryKey(plannedProjectNereidResult);
        }
    }
}