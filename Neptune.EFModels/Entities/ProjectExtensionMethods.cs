using System.Linq;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectExtensionMethods
    {
        public static ProjectDto AsDto(this Project project)
        {
            var dto = new ProjectDto
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
                OCTAWatersheds = project.OCTAWatersheds,
                PollutantVolume = project.PollutantVolume,
                PollutantMetals = project.PollutantMetals,
                PollutantBacteria = project.PollutantBacteria,
                PollutantNutrients = project.PollutantNutrients,
                PollutantTSS = project.PollutantTSS,
                TPI = project.TPI,
                SEA = project.SEA,
                DryWeatherWQLRI = project.DryWeatherWQLRI,
                WetWeatherWQLRI = project.WetWeatherWQLRI,
                AreaTreatedAcres = project.AreaTreatedAcres,
                ImperviousAreaTreatedAcres = project.ImperviousAreaTreatedAcres,
                UpdatePersonID = project.UpdatePersonID,
                DateUpdated = project.DateUpdated,
                Organization = project.Organization.AsSimpleDto(),
                StormwaterJurisdiction = project.StormwaterJurisdiction.AsDto(),
                ProjectStatus = project.ProjectStatus.AsSimpleDto(),
                PrimaryContactPerson = project.PrimaryContactPerson.AsSimpleDto(),
                CreatePerson = project.CreatePerson.AsSimpleDto(),
                HasModeledResults = project.AreaTreatedAcres != null
            };
            return dto;
        }
    }
}