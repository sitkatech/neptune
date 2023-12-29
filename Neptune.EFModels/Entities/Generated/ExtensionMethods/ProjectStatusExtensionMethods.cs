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
            var dto = new ProjectStatusSimpleDto()
            {
                ProjectStatusID = projectStatus.ProjectStatusID,
                ProjectStatusName = projectStatus.ProjectStatusName,
                ProjectStatusDisplayName = projectStatus.ProjectStatusDisplayName
            };
            return dto;
        }
    }
}