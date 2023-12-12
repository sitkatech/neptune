//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectExtensionMethods
    {
        public static ProjectSimpleDto AsSimpleDto(this Project project)
        {
            var dto = new ProjectSimpleDto()
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
                DateUpdated = project.DateUpdated
            };
            return dto;
        }
    }
}