//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistory]
namespace Neptune.EFModels.Entities
{
    public partial class ProjectNetworkSolveHistory : IHavePrimaryKey
    {
        public int PrimaryKey => ProjectNetworkSolveHistoryID;
        public ProjectNetworkSolveHistoryStatusType ProjectNetworkSolveHistoryStatusType => ProjectNetworkSolveHistoryStatusType.AllLookupDictionary[ProjectNetworkSolveHistoryStatusTypeID];

        public static class FieldLengths
        {

        }
    }
}