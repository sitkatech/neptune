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
            projectSimpleDto.HasModeledResults = project.ProjectNereidResults.Any();
        }

        public static ProjectHRUCharacteristicsSummaryDto AsProjectHRUCharacteristicsSummaryDto(this Project project)
        {
            var projectDto = new ProjectHRUCharacteristicsSummaryDto()
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                OrganizationID = project.OrganizationID,
                StormwaterJurisdictionID = project.StormwaterJurisdictionID,
                ProjectStatusID = project.ProjectStatusID,
                PrimaryContactPersonID = project.PrimaryContactPersonID,
                CreatePersonID = project.CreatePersonID,
                DateCreated = project.DateCreated,
                ProjectDescription = project.ProjectDescription,
                AdditionalContactInformation = project.AdditionalContactInformation,
                DoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs,
                CalculateOCTAM2Tier2Scores = project.CalculateOCTAM2Tier2Scores,
                ShareOCTAM2Tier2Scores = project.ShareOCTAM2Tier2Scores,
                OCTAM2Tier2ScoresLastSharedDate = project.OCTAM2Tier2ScoresLastSharedDate,
                Organization = project.Organization.AsSimpleDto(),
                StormwaterJurisdiction = project.StormwaterJurisdiction.AsSimpleDto(),
                ProjectStatus = project.ProjectStatus.AsSimpleDto(),
                PrimaryContactPerson = project.PrimaryContactPerson.AsSimpleDto(),
                CreatePerson = project.CreatePerson.AsSimpleDto(),
                Area = project.ProjectHRUCharacteristics.Sum(x => x.Area),
                ImperviousAcres = project.ProjectHRUCharacteristics.Sum(x => x.ImperviousAcres),
                SEA = project.SEA,
                TPI = project.TPI,
                DryWeatherWQLRI = project.DryWeatherWQLRI,
                WetWeatherWQLRI = project.WetWeatherWQLRI

        };

            return projectDto;
        }
    }
}