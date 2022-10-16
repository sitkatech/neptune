using System.Linq;
using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectExtensionMethods
    { 
        static partial void DoCustomSimpleDtoMappings(Project project, ProjectSimpleDto projectSimpleDto)
        {
            projectSimpleDto.Organization = project.Organization.AsSimpleDto();
            projectSimpleDto.StormwaterJurisdiction = project.StormwaterJurisdiction.AsSimpleDto();
            projectSimpleDto.ProjectStatus = project.ProjectStatus.AsSimpleDto();
            projectSimpleDto.PrimaryContactPerson = project.PrimaryContactPerson.AsSimpleDto();
            projectSimpleDto.CreatePerson = project.CreatePerson.AsSimpleDto();
            projectSimpleDto.HasModeledResults = project.AreaTreatedAcres != null;
        }
    }
}