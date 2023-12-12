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
            var projectNetworkSolveHistoryStatusTypeSimpleDto = new ProjectNetworkSolveHistoryStatusTypeSimpleDto()
            {
                ProjectNetworkSolveHistoryStatusTypeID = projectNetworkSolveHistoryStatusType.ProjectNetworkSolveHistoryStatusTypeID,
                ProjectNetworkSolveHistoryStatusTypeName = projectNetworkSolveHistoryStatusType.ProjectNetworkSolveHistoryStatusTypeName,
                ProjectNetworkSolveHistoryStatusTypeDisplayName = projectNetworkSolveHistoryStatusType.ProjectNetworkSolveHistoryStatusTypeDisplayName
            };
            DoCustomSimpleDtoMappings(projectNetworkSolveHistoryStatusType, projectNetworkSolveHistoryStatusTypeSimpleDto);
            return projectNetworkSolveHistoryStatusTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ProjectNetworkSolveHistoryStatusType projectNetworkSolveHistoryStatusType, ProjectNetworkSolveHistoryStatusTypeSimpleDto projectNetworkSolveHistoryStatusTypeSimpleDto);
    }
}