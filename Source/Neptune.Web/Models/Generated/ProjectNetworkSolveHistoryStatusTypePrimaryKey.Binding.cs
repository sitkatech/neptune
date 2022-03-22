//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectNetworkSolveHistoryStatusType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ProjectNetworkSolveHistoryStatusTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectNetworkSolveHistoryStatusType>
    {
        public ProjectNetworkSolveHistoryStatusTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectNetworkSolveHistoryStatusTypePrimaryKey(ProjectNetworkSolveHistoryStatusType projectNetworkSolveHistoryStatusType) : base(projectNetworkSolveHistoryStatusType){}

        public static implicit operator ProjectNetworkSolveHistoryStatusTypePrimaryKey(int primaryKeyValue)
        {
            return new ProjectNetworkSolveHistoryStatusTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectNetworkSolveHistoryStatusTypePrimaryKey(ProjectNetworkSolveHistoryStatusType projectNetworkSolveHistoryStatusType)
        {
            return new ProjectNetworkSolveHistoryStatusTypePrimaryKey(projectNetworkSolveHistoryStatusType);
        }
    }
}