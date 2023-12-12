//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistoryStatusType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectNetworkSolveHistoryStatusTypeExtensionMethods
    {
        public static ProjectNetworkSolveHistoryStatusTypeSimpleDto AsSimpleDto(this ProjectNetworkSolveHistoryStatusType projectNetworkSolveHistoryStatusType)
        {
            var dto = new ProjectNetworkSolveHistoryStatusTypeSimpleDto()
            {
                ProjectNetworkSolveHistoryStatusTypeID = projectNetworkSolveHistoryStatusType.ProjectNetworkSolveHistoryStatusTypeID,
                ProjectNetworkSolveHistoryStatusTypeName = projectNetworkSolveHistoryStatusType.ProjectNetworkSolveHistoryStatusTypeName,
                ProjectNetworkSolveHistoryStatusTypeDisplayName = projectNetworkSolveHistoryStatusType.ProjectNetworkSolveHistoryStatusTypeDisplayName
            };
            return dto;
        }
    }
}