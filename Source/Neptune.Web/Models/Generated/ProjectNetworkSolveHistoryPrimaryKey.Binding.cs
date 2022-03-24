//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectNetworkSolveHistory
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ProjectNetworkSolveHistoryPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectNetworkSolveHistory>
    {
        public ProjectNetworkSolveHistoryPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectNetworkSolveHistoryPrimaryKey(ProjectNetworkSolveHistory projectNetworkSolveHistory) : base(projectNetworkSolveHistory){}

        public static implicit operator ProjectNetworkSolveHistoryPrimaryKey(int primaryKeyValue)
        {
            return new ProjectNetworkSolveHistoryPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectNetworkSolveHistoryPrimaryKey(ProjectNetworkSolveHistory projectNetworkSolveHistory)
        {
            return new ProjectNetworkSolveHistoryPrimaryKey(projectNetworkSolveHistory);
        }
    }
}