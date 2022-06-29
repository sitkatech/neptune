//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectExtensionMethods
    {
        public static ProjectDto AsDto(this Project project)
        {
            var projectDto = new ProjectDto()
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                Organization = project.Organization.AsDto(),
                StormwaterJurisdiction = project.StormwaterJurisdiction.AsDto(),
                ProjectStatus = project.ProjectStatus.AsDto(),
                PrimaryContactPerson = project.PrimaryContactPerson.AsDto(),
                CreatePerson = project.CreatePerson.AsDto(),
                DateCreated = project.DateCreated,
                ProjectDescription = project.ProjectDescription,
                AdditionalContactInformation = project.AdditionalContactInformation,
                DoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs,
                CalculateOCTAM2Tier2Scores = project.CalculateOCTAM2Tier2Scores,
                ShareOCTAM2Tier2Scores = project.ShareOCTAM2Tier2Scores,
                OCTAM2Tier2ScoresLastSharedDate = project.OCTAM2Tier2ScoresLastSharedDate
            };
            DoCustomMappings(project, projectDto);
            return projectDto;
        }

        static partial void DoCustomMappings(Project project, ProjectDto projectDto);

        public static ProjectSimpleDto AsSimpleDto(this Project project)
        {
            var projectSimpleDto = new ProjectSimpleDto()
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
                OCTAM2Tier2ScoresLastSharedDate = project.OCTAM2Tier2ScoresLastSharedDate
            };
            DoCustomSimpleDtoMappings(project, projectSimpleDto);
            return projectSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(Project project, ProjectSimpleDto projectSimpleDto);
    }
}