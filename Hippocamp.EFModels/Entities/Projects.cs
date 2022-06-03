using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class Projects
    {
        private static IQueryable<Project> GetProjectsImpl(HippocampDbContext dbContext)
        {
            return dbContext.Projects
                .Include(x => x.Organization)
                .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                .Include(x => x.ProjectStatus)
                .Include(x => x.CreatePerson)
                .Include(x => x.PrimaryContactPerson)
                .AsNoTracking();
        }

        public static List<ProjectSimpleDto> ListAsSimpleDto(HippocampDbContext dbContext)
        {
            return GetProjectsImpl(dbContext).OrderByDescending(x => x.ProjectID).Select(x => x.AsSimpleDto()).ToList();
        }

        public static Project GetByID(HippocampDbContext dbContext, int projectID)
        {
            return dbContext.Projects.SingleOrDefault(x => x.ProjectID == projectID);
        }

        public static ProjectSimpleDto GetByIDAsSimpleDto(HippocampDbContext dbContext, int projectID)
        {
            return GetProjectsImpl(dbContext).SingleOrDefault(x => x.ProjectID == projectID).AsSimpleDto();
        }

        public static List<ProjectSimpleDto> ListByPersonIDAsSimpleDto(HippocampDbContext dbContext, int personID)
        {
            var person = People.GetByID(dbContext, personID);
            if (person.RoleID == (int)RoleEnum.Admin || person.RoleID == (int)RoleEnum.SitkaAdmin)
            {
                return ListAsSimpleDto(dbContext);
            }

            var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, personID);

            return GetProjectsImpl(dbContext)
                .Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID))
                .OrderByDescending(x => x.ProjectID)
                .Select(x => x.AsSimpleDto())
                .ToList();
        }

        public static ProjectSimpleDto CreateNew(HippocampDbContext dbContext, ProjectUpsertDto projectUpsertDto, PersonDto personDto)
        {
            var project = new Project()
            {
                ProjectName = projectUpsertDto.ProjectName,
                OrganizationID = projectUpsertDto.OrganizationID.Value,
                StormwaterJurisdictionID = projectUpsertDto.StormwaterJurisdictionID.Value,
                ProjectStatusID = (int) ProjectStatusEnum.Draft,
                PrimaryContactPersonID = projectUpsertDto.PrimaryContactPersonID.Value,
                CreatePersonID = personDto.PersonID,
                DateCreated = DateTime.UtcNow,
                ProjectDescription = projectUpsertDto.ProjectDescription,
                AdditionalContactInformation = projectUpsertDto.AdditionalContactInformation,
                DoesNotIncludeTreatmentBMPs = false,
                CalculateOCTAM2Tier2Scores = projectUpsertDto.CalculateOCTAM2Tier2Scores,
                ShareOCTAM2Tier2Scores = false
            };
            dbContext.Add(project);
            dbContext.SaveChanges();
            dbContext.Entry(project).Reload();
            return GetByIDAsSimpleDto(dbContext, project.ProjectID);
        }

        public static void Update(HippocampDbContext dbContext, Project project, ProjectUpsertDto projectUpsertDto)
        {
            project.ProjectName = projectUpsertDto.ProjectName;
            project.OrganizationID = projectUpsertDto.OrganizationID.Value;
            project.StormwaterJurisdictionID = projectUpsertDto.StormwaterJurisdictionID.Value;
            project.PrimaryContactPersonID = projectUpsertDto.PrimaryContactPersonID.Value;
            project.ProjectDescription = projectUpsertDto.ProjectDescription;
            project.AdditionalContactInformation = projectUpsertDto.AdditionalContactInformation;
            project.DoesNotIncludeTreatmentBMPs = projectUpsertDto.DoesNotIncludeTreatmentBMPs;
            project.CalculateOCTAM2Tier2Scores = projectUpsertDto.CalculateOCTAM2Tier2Scores;
            project.ShareOCTAM2Tier2Scores = projectUpsertDto.ShareOCTAM2Tier2Scores;

            //If we opt to not include treatmentBMPs, ensure we get rid of our pre-existing treatment bmps
            if (project.DoesNotIncludeTreatmentBMPs)
            {
                TreatmentBMPs.MergeProjectTreatmentBMPs(dbContext, new List<TreatmentBMPUpsertDto>(), dbContext.TreatmentBMPs.Where(x => x.ProjectID == project.ProjectID).ToList(), project);
            }

            dbContext.SaveChanges();
        }

        public static void Delete(HippocampDbContext dbContext, int projectID)
        {
            var project = dbContext.Projects.Include(x => x.TreatmentBMPs).Where(x => x.ProjectID == projectID).First();
            var treatmentBMPIDs = project.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
            var delineationIDs = dbContext.Delineations.Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID)).Select(x => x.DelineationID).ToList();
            
            dbContext.ProjectHRUCharacteristics.RemoveRange(dbContext.ProjectHRUCharacteristics.Where(x => x.ProjectID == projectID).ToList());
            dbContext.ProjectLoadGeneratingUnits.RemoveRange(dbContext.ProjectLoadGeneratingUnits.Where(x => x.ProjectID == projectID).ToList());
            dbContext.ProjectNereidResults.RemoveRange(dbContext.ProjectNereidResults.Where(x => x.ProjectID == projectID).ToList());
            dbContext.ProjectNetworkSolveHistories.RemoveRange(dbContext.ProjectNetworkSolveHistories.Where(x => x.ProjectID == projectID).ToList());
            dbContext.DelineationOverlaps.RemoveRange(dbContext.DelineationOverlaps.Where(x => delineationIDs.Contains(x.DelineationID)).ToList());
            dbContext.Delineations.RemoveRange(dbContext.Delineations.Where(x => delineationIDs.Contains(x.DelineationID)).ToList());
            dbContext.TreatmentBMPModelingAttributes.RemoveRange(dbContext.TreatmentBMPModelingAttributes.Where(x => treatmentBMPIDs.Contains(x.TreatmentBMPID)).ToList());
            dbContext.TreatmentBMPs.RemoveRange(project.TreatmentBMPs.ToList());
            dbContext.Projects.Remove(project);
            dbContext.SaveChanges();
        }

        public static List<TreatmentBMPHRUCharacteristicsSummarySimpleDto> GetTreatmentBMPHRUCharacteristicSimplesForProject(HippocampDbContext dbContext, int projectID)
        {
            var projectTreatmentBMPs = dbContext.Projects
                .Include(x => x.TreatmentBMPs)
                .ThenInclude(x => x.Delineation)
                .Include(x => x.TreatmentBMPs)
                .ThenInclude(x => x.TreatmentBMPType)
                .Where(x => x.ProjectID == projectID).SelectMany(x => x.TreatmentBMPs).ToList();

            if (projectTreatmentBMPs == null || projectTreatmentBMPs.Count == 0)
            {
                return new List<TreatmentBMPHRUCharacteristicsSummarySimpleDto>();
            }

            return projectTreatmentBMPs.SelectMany(x =>
                x.GetHRUCharacteristics(dbContext)
                .ToList()
                .GroupBy(y => y.HRUCharacteristicLandUseCode)
                .Select(y => new TreatmentBMPHRUCharacteristicsSummarySimpleDto()
                {
                    TreatmentBMPID = x.TreatmentBMPID,
                    Area = y.Sum(z => z.Area),
                    ImperviousCover = y.Sum(z => z.ImperviousAcres),
                    LandUse = y.Key.HRUCharacteristicLandUseCodeDisplayName
                })
                .ToList()
            )
            .ToList();

        }

        public static IEnumerable<Project> ListOCTAM2Tier2Projects(HippocampDbContext dbContext)
        {
            var projects = GetProjectsImpl(dbContext).Include(x => x.ProjectHRUCharacteristics)
                .Where(x => x.ShareOCTAM2Tier2Scores);

            return projects.ToList();
        }

        public static List<ProjectModeledResultSummaryDto> ListByIDsAsModeledResultSummaryDtos(HippocampDbContext dbContext, List<int> projectIDs)
        {
            var treatmentBMPModeledResultByProject = ProjectNereidResults
                .GetTreatmentBMPModeledResultSimpleDtosByProjectIDs(dbContext, projectIDs)
                .GroupBy(x => x.ProjectID);

            var projectModeledResultSummaryDtos = new List<ProjectModeledResultSummaryDto>();

            foreach (var treatmentBMPGroup in treatmentBMPModeledResultByProject)
            {
                var project = dbContext.Projects.Include(x => x.Organization)
                    .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                    .SingleOrDefault(x => x.ProjectID == treatmentBMPGroup.Key);

                var projectHRUCharacteristics = dbContext.ProjectHRUCharacteristics
                    .Where(x => x.ProjectID == treatmentBMPGroup.Key);
                var area = projectHRUCharacteristics.Sum(x => x.Area);
                var imperviousAcres = projectHRUCharacteristics.Sum(x => x.ImperviousAcres);

                projectModeledResultSummaryDtos.Add(new ProjectModeledResultSummaryDto() 
                    {
                        ProjectName = project?.ProjectName,
                        Jurisdiction = project?.StormwaterJurisdiction.Organization.OrganizationName,
                        Organization = project?.Organization.OrganizationName,
                        AcresTreated = area,
                        ImperviousAcresTreated = imperviousAcres,

                        WetWeatherInflow = treatmentBMPGroup.Sum(x => x.WetWeatherInflow),
                        WetWeatherTreated = treatmentBMPGroup.Sum(x => x.WetWeatherTreated),
                        WetWeatherRetained = treatmentBMPGroup.Sum(x => x.WetWeatherRetained),
                        WetWeatherUntreated = treatmentBMPGroup.Sum(x => x.WetWeatherUntreated),
                        WetWeatherTSSRemoved = treatmentBMPGroup.Sum(x => x.WetWeatherTSSRemoved),
                        WetWeatherTNRemoved = treatmentBMPGroup.Sum(x => x.WetWeatherTNRemoved),
                        WetWeatherTPRemoved = treatmentBMPGroup.Sum(x => x.WetWeatherTPRemoved),
                        WetWeatherFCRemoved = treatmentBMPGroup.Sum(x => x.WetWeatherFCRemoved),
                        WetWeatherTCuRemoved = treatmentBMPGroup.Sum(x => x.WetWeatherTCuRemoved),
                        WetWeatherTPbRemoved = treatmentBMPGroup.Sum(x => x.WetWeatherTPbRemoved),
                        WetWeatherTZnRemoved = treatmentBMPGroup.Sum(x => x.WetWeatherTZnRemoved),
                        WetWeatherTSSInflow = treatmentBMPGroup.Sum(x => x.WetWeatherTSSInflow),
                        WetWeatherTNInflow = treatmentBMPGroup.Sum(x => x.WetWeatherTNInflow),
                        WetWeatherTPInflow = treatmentBMPGroup.Sum(x => x.WetWeatherTPInflow),
                        WetWeatherFCInflow = treatmentBMPGroup.Sum(x => x.WetWeatherFCInflow),
                        WetWeatherTCuInflow = treatmentBMPGroup.Sum(x => x.WetWeatherTCuInflow),
                        WetWeatherTPbInflow = treatmentBMPGroup.Sum(x => x.WetWeatherTPbInflow),
                        WetWeatherTZnInflow = treatmentBMPGroup.Sum(x => x.WetWeatherTZnInflow),

                        SummerDryWeatherInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherInflow),
                        SummerDryWeatherTreated = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTreated),
                        SummerDryWeatherRetained = treatmentBMPGroup.Sum(x => x.SummerDryWeatherRetained),
                        SummerDryWeatherUntreated = treatmentBMPGroup.Sum(x => x.SummerDryWeatherUntreated),
                        SummerDryWeatherTSSRemoved = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTSSRemoved),
                        SummerDryWeatherTNRemoved = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTNRemoved),
                        SummerDryWeatherTPRemoved = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTPRemoved),
                        SummerDryWeatherFCRemoved = treatmentBMPGroup.Sum(x => x.SummerDryWeatherFCRemoved),
                        SummerDryWeatherTCuRemoved = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTCuRemoved),
                        SummerDryWeatherTPbRemoved = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTPbRemoved),
                        SummerDryWeatherTZnRemoved = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTZnRemoved),
                        SummerDryWeatherTSSInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTSSInflow),
                        SummerDryWeatherTNInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTNInflow),
                        SummerDryWeatherTPInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTPInflow),
                        SummerDryWeatherFCInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherFCInflow),
                        SummerDryWeatherTCuInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTCuInflow),
                        SummerDryWeatherTPbInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTPbInflow),
                        SummerDryWeatherTZnInflow = treatmentBMPGroup.Sum(x => x.SummerDryWeatherTZnInflow),

                        WinterDryWeatherInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherInflow),
                        WinterDryWeatherTreated = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTreated),
                        WinterDryWeatherRetained = treatmentBMPGroup.Sum(x => x.WinterDryWeatherRetained),
                        WinterDryWeatherUntreated = treatmentBMPGroup.Sum(x => x.WinterDryWeatherUntreated),
                        WinterDryWeatherTSSRemoved = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTSSRemoved),
                        WinterDryWeatherTNRemoved = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTNRemoved),
                        WinterDryWeatherTPRemoved = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTPRemoved),
                        WinterDryWeatherFCRemoved = treatmentBMPGroup.Sum(x => x.WinterDryWeatherFCRemoved),
                        WinterDryWeatherTCuRemoved = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTCuRemoved),
                        WinterDryWeatherTPbRemoved = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTPbRemoved),
                        WinterDryWeatherTZnRemoved = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTZnRemoved),
                        WinterDryWeatherTSSInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTSSInflow),
                        WinterDryWeatherTNInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTNInflow),
                        WinterDryWeatherTPInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTPInflow),
                        WinterDryWeatherFCInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherFCInflow),
                        WinterDryWeatherTCuInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherFCInflow),
                        WinterDryWeatherTPbInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTPbInflow),
                        WinterDryWeatherTZnInflow = treatmentBMPGroup.Sum(x => x.WinterDryWeatherTZnInflow), 
                        
                        DryWeatherInflow = treatmentBMPGroup.Sum(x => x.DryWeatherInflow),
                        DryWeatherTreated = treatmentBMPGroup.Sum(x => x.DryWeatherTreated),
                        DryWeatherRetained = treatmentBMPGroup.Sum(x => x.DryWeatherRetained),
                        DryWeatherUntreated = treatmentBMPGroup.Sum(x => x.DryWeatherUntreated),
                        DryWeatherTSSRemoved = treatmentBMPGroup.Sum(x => x.DryWeatherTSSRemoved),
                        DryWeatherTNRemoved = treatmentBMPGroup.Sum(x => x.DryWeatherTNRemoved),
                        DryWeatherTPRemoved = treatmentBMPGroup.Sum(x => x.DryWeatherTPRemoved),
                        DryWeatherFCRemoved = treatmentBMPGroup.Sum(x => x.DryWeatherFCRemoved),
                        DryWeatherTCuRemoved = treatmentBMPGroup.Sum(x => x.DryWeatherTCuRemoved),
                        DryWeatherTPbRemoved = treatmentBMPGroup.Sum(x => x.DryWeatherTPbRemoved),
                        DryWeatherTZnRemoved = treatmentBMPGroup.Sum(x => x.DryWeatherTZnRemoved),
                        DryWeatherTSSInflow = treatmentBMPGroup.Sum(x => x.DryWeatherTSSInflow),
                        DryWeatherTNInflow = treatmentBMPGroup.Sum(x => x.DryWeatherTNInflow),
                        DryWeatherTPInflow = treatmentBMPGroup.Sum(x => x.DryWeatherTPInflow),
                        DryWeatherFCInflow = treatmentBMPGroup.Sum(x => x.DryWeatherFCInflow),
                        DryWeatherTCuInflow = treatmentBMPGroup.Sum(x => x.DryWeatherFCInflow),
                        DryWeatherTPbInflow = treatmentBMPGroup.Sum(x => x.DryWeatherTPbInflow),
                        DryWeatherTZnInflow = treatmentBMPGroup.Sum(x => x.DryWeatherTZnInflow),

                        TotalInflow = treatmentBMPGroup.Sum(x => x.TotalInflow),
                        TotalTreated = treatmentBMPGroup.Sum(x => x.TotalTreated),
                        TotalRetained = treatmentBMPGroup.Sum(x => x.TotalRetained),
                        TotalUntreated = treatmentBMPGroup.Sum(x => x.TotalUntreated),
                        TotalTSSRemoved = treatmentBMPGroup.Sum(x => x.TotalTSSRemoved),
                        TotalTNRemoved = treatmentBMPGroup.Sum(x => x.TotalTNRemoved),
                        TotalTPRemoved = treatmentBMPGroup.Sum(x => x.TotalTPRemoved),
                        TotalFCRemoved = treatmentBMPGroup.Sum(x => x.TotalFCRemoved),
                        TotalTCuRemoved = treatmentBMPGroup.Sum(x => x.TotalTCuRemoved),
                        TotalTPbRemoved = treatmentBMPGroup.Sum(x => x.TotalTPbRemoved),
                        TotalTZnRemoved = treatmentBMPGroup.Sum(x => x.TotalTZnRemoved),
                        TotalTSSInflow = treatmentBMPGroup.Sum(x => x.TotalTSSInflow),
                        TotalTNInflow = treatmentBMPGroup.Sum(x => x.TotalTNInflow),
                        TotalTPInflow = treatmentBMPGroup.Sum(x => x.TotalTPInflow),
                        TotalFCInflow = treatmentBMPGroup.Sum(x => x.TotalFCInflow),
                        TotalTCuInflow = treatmentBMPGroup.Sum(x => x.TotalFCInflow),
                        TotalTPbInflow = treatmentBMPGroup.Sum(x => x.TotalTPbInflow),
                        TotalTZnInflow = treatmentBMPGroup.Sum(x => x.TotalTZnInflow)
                    }
                );
            }

            foreach (var projectID in projectIDs)
            {
                if (treatmentBMPModeledResultByProject.Any(x => x.Key == projectID))
                {
                    continue;
                }

                var project = dbContext.Projects.Include(x => x.Organization)
                    .Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization)
                    .SingleOrDefault(x => x.ProjectID == projectID);

                var projectHRUCharacteristics = dbContext.ProjectHRUCharacteristics
                    .Where(x => x.ProjectID == projectID);
                var area = projectHRUCharacteristics.Sum(x => x.Area);
                var imperviousAcres = projectHRUCharacteristics.Sum(x => x.ImperviousAcres);

                projectModeledResultSummaryDtos.Add(new ProjectModeledResultSummaryDto()
                {
                    ProjectName = project?.ProjectName,
                    Jurisdiction = project?.StormwaterJurisdiction.Organization.OrganizationName,
                    Organization = project?.Organization.OrganizationName,
                    AcresTreated = area,
                    ImperviousAcresTreated = imperviousAcres
                });
            }

            return projectModeledResultSummaryDtos;
        }
    }
}