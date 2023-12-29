//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectNetworkSolveHistoryStatusType


namespace Neptune.EFModels.Entities
{
    public class ProjectNetworkSolveHistoryStatusTypePrimaryKey : EntityPrimaryKey<ProjectNetworkSolveHistoryStatusType>
    {
        public ProjectNetworkSolveHistoryStatusTypePrimaryKey() : base(){}
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