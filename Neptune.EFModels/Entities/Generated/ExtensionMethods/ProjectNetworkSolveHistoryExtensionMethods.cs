//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistory]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectNetworkSolveHistoryExtensionMethods
    {
        public static ProjectNetworkSolveHistorySimpleDto AsSimpleDto(this ProjectNetworkSolveHistory projectNetworkSolveHistory)
        {
            var dto = new ProjectNetworkSolveHistorySimpleDto()
            {
                ProjectNetworkSolveHistoryID = projectNetworkSolveHistory.ProjectNetworkSolveHistoryID,
                ProjectID = projectNetworkSolveHistory.ProjectID,
                RequestedByPersonID = projectNetworkSolveHistory.RequestedByPersonID,
                ProjectNetworkSolveHistoryStatusTypeID = projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID,
                LastUpdated = projectNetworkSolveHistory.LastUpdated,
                ErrorMessage = projectNetworkSolveHistory.ErrorMessage
            };
            return dto;
        }
    }
}