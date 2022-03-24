//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectNereidResult
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ProjectNereidResultPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectNereidResult>
    {
        public ProjectNereidResultPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectNereidResultPrimaryKey(ProjectNereidResult projectNereidResult) : base(projectNereidResult){}

        public static implicit operator ProjectNereidResultPrimaryKey(int primaryKeyValue)
        {
            return new ProjectNereidResultPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectNereidResultPrimaryKey(ProjectNereidResult projectNereidResult)
        {
            return new ProjectNereidResultPrimaryKey(projectNereidResult);
        }
    }
}