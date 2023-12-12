//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectStatusExtensionMethods
    {

        public static ProjectStatusSimpleDto AsSimpleDto(this ProjectStatus projectStatus)
        {
            var projectStatusSimpleDto = new ProjectStatusSimpleDto()
            {
                ProjectStatusID = projectStatus.ProjectStatusID,
                ProjectStatusName = projectStatus.ProjectStatusName,
                ProjectStatusDisplayName = projectStatus.ProjectStatusDisplayName
            };
            DoCustomSimpleDtoMappings(projectStatus, projectStatusSimpleDto);
            return projectStatusSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ProjectStatus projectStatus, ProjectStatusSimpleDto projectStatusSimpleDto);
    }
}