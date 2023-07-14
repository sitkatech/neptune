using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
                ProjectStatusID = (int)ProjectStatusEnum.Draft,
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

        public static void Update(HippocampDbContext dbContext, Project project, ProjectUpsertDto projectUpsertDto, int personID)
        {
            project.ProjectName = projectUpsertDto.ProjectName;
            project.OrganizationID = projectUpsertDto.OrganizationID.Value;
            project.StormwaterJurisdictionID = projectUpsertDto.StormwaterJurisdictionID.Value;
            project.PrimaryContactPersonID = projectUpsertDto.PrimaryContactPersonID.Value;
            project.ProjectDescription = projectUpsertDto.ProjectDescription;
            project.AdditionalContactInformation = projectUpsertDto.AdditionalContactInformation;
            project.DoesNotIncludeTreatmentBMPs = projectUpsertDto.DoesNotIncludeTreatmentBMPs;
            project.CalculateOCTAM2Tier2Scores = projectUpsertDto.CalculateOCTAM2Tier2Scores;
            SetUpdatePersonAndDate(dbContext, project.ProjectID, personID);

            if (projectUpsertDto.ShareOCTAM2Tier2Scores && !project.ShareOCTAM2Tier2Scores)
            {
                project.OCTAM2Tier2ScoresLastSharedDate = DateTime.UtcNow;
            }
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

        public static void DeleteProjectNereidResultsAndGrantScores(HippocampDbContext dbContext, int projectID)
        {
            var projectNereidResults = dbContext.ProjectNereidResults.Where(x => x.ProjectID == projectID).ToList();
            dbContext.ProjectNereidResults.RemoveRange(projectNereidResults);

            var project = dbContext.Projects.Single(x => x.ProjectID == projectID);
            project.OCTAWatersheds = null;
            project.PollutantVolume = null;
            project.PollutantMetals = null;
            project.PollutantBacteria = null;
            project.PollutantNutrients = null;
            project.PollutantTSS = null;
            project.TPI = null;
            project.SEA = null;
            project.DryWeatherWQLRI = null;
            project.WetWeatherWQLRI = null;

            dbContext.SaveChanges();
        }

        public static Project CreateCopy(HippocampDbContext dbContext, Project projectToCopy, int createPersonID)
        {
            var dateCreated = DateTime.UtcNow;
            
            var newProject = new Project()
            {
                ProjectName = $"{projectToCopy.ProjectName} - Copy {dateCreated}",
                OrganizationID = projectToCopy.OrganizationID,
                StormwaterJurisdictionID = projectToCopy.StormwaterJurisdictionID,
                ProjectStatusID = (int)ProjectStatusEnum.Draft,
                PrimaryContactPersonID = createPersonID,
                CreatePersonID = createPersonID,
                DateCreated = dateCreated,
                ProjectDescription = projectToCopy.ProjectDescription,
                AdditionalContactInformation = projectToCopy.AdditionalContactInformation,
                DoesNotIncludeTreatmentBMPs = projectToCopy.DoesNotIncludeTreatmentBMPs,
                CalculateOCTAM2Tier2Scores = projectToCopy.CalculateOCTAM2Tier2Scores,
                ShareOCTAM2Tier2Scores = false
            };

            dbContext.Projects.Add(newProject);
            dbContext.SaveChanges();

            var treatmentBMPsToCopy = dbContext.TreatmentBMPs.Where(x => x.ProjectID == projectToCopy.ProjectID).ToList();

            var newTreatmentBMPs = treatmentBMPsToCopy.Select(x => new TreatmentBMP()
            {
                ProjectID = newProject.ProjectID,
                TreatmentBMPName = $"{x.TreatmentBMPName} - Copy {dateCreated}",
                TreatmentBMPTypeID = x.TreatmentBMPTypeID,
                LocationPoint = x.LocationPoint,
                StormwaterJurisdictionID = x.StormwaterJurisdictionID,
                Notes = x.Notes,
                OwnerOrganizationID = x.OwnerOrganizationID,
                InventoryIsVerified = x.InventoryIsVerified,
                LocationPoint4326 = x.LocationPoint4326,
                TrashCaptureStatusTypeID = x.TrashCaptureStatusTypeID,
                SizingBasisTypeID = x.SizingBasisTypeID
            }).ToList();

            dbContext.TreatmentBMPs.AddRange(newTreatmentBMPs);
            dbContext.SaveChanges();

            var newTreatmentBMPIDsByCopiedTreatmentBMPIDs = treatmentBMPsToCopy
                .Select(x => new 
                {
                    copiedTreatmentBMPID = x.TreatmentBMPID,
                    newTreatmentBMpID = newTreatmentBMPs.Single(y => y.TreatmentBMPName.StartsWith(x.TreatmentBMPName)).TreatmentBMPID
                }).ToDictionary(x => x.copiedTreatmentBMPID, x => x.newTreatmentBMpID);

            var treatmentBMPIDsToCopy = treatmentBMPsToCopy.Select(x => x.TreatmentBMPID).ToList();

            var newModelingAttributes = dbContext.TreatmentBMPModelingAttributes
                .Where(x => treatmentBMPIDsToCopy.Contains(x.TreatmentBMPID)).AsEnumerable()
                .Select(x => new TreatmentBMPModelingAttribute()
                {
                    TreatmentBMPID = newTreatmentBMPIDsByCopiedTreatmentBMPIDs[x.TreatmentBMPID],
                    UpstreamTreatmentBMPID = x.UpstreamTreatmentBMPID,
                    AverageDivertedFlowrate = x.AverageDivertedFlowrate,
                    AverageTreatmentFlowrate = x.AverageTreatmentFlowrate,
                    DesignDryWeatherTreatmentCapacity = x.DesignDryWeatherTreatmentCapacity,
                    DesignLowFlowDiversionCapacity = x.DesignLowFlowDiversionCapacity,
                    DesignMediaFiltrationRate = x.DesignMediaFiltrationRate,
                    DiversionRate = x.DiversionRate,
                    DrawdownTimeforWQDetentionVolume = x.DrawdownTimeforWQDetentionVolume,
                    EffectiveFootprint = x.EffectiveFootprint,
                    EffectiveRetentionDepth = x.EffectiveRetentionDepth,
                    InfiltrationDischargeRate = x.InfiltrationDischargeRate,
                    InfiltrationSurfaceArea = x.InfiltrationSurfaceArea,
                    MediaBedFootprint = x.MediaBedFootprint,
                    PermanentPoolorWetlandVolume = x.PermanentPoolorWetlandVolume,
                    RoutingConfigurationID = x.RoutingConfigurationID,
                    StorageVolumeBelowLowestOutletElevation = x.StorageVolumeBelowLowestOutletElevation,
                    SummerHarvestedWaterDemand = x.SummerHarvestedWaterDemand,
                    TimeOfConcentrationID = x.TimeOfConcentrationID,
                    DrawdownTimeForDetentionVolume = x.DrawdownTimeForDetentionVolume,
                    TotalEffectiveBMPVolume = x.TotalEffectiveBMPVolume,
                    TotalEffectiveDrywellBMPVolume = x.TotalEffectiveDrywellBMPVolume,
                    TreatmentRate = x.TreatmentRate,
                    UnderlyingHydrologicSoilGroupID = x.UnderlyingHydrologicSoilGroupID,
                    UnderlyingInfiltrationRate = x.UnderlyingInfiltrationRate,
                    WaterQualityDetentionVolume = x.WaterQualityDetentionVolume,
                    WettedFootprint = x.WettedFootprint,
                    WinterHarvestedWaterDemand = x.WinterHarvestedWaterDemand,
                    MonthsOfOperationID = x.MonthsOfOperationID,
                    DryWeatherFlowOverrideID = x.DryWeatherFlowOverrideID
                });
            
            dbContext.TreatmentBMPModelingAttributes.AddRange(newModelingAttributes);

            var newDelineations = dbContext.Delineations
                .Where(x => treatmentBMPIDsToCopy.Contains(x.TreatmentBMPID)).AsEnumerable()

                .Select(x => new Delineation()
                {
                    TreatmentBMPID = newTreatmentBMPIDsByCopiedTreatmentBMPIDs[x.TreatmentBMPID],
                    DelineationGeometry = x.DelineationGeometry,
                    DelineationGeometry4326 = x.DelineationGeometry4326,
                    DelineationTypeID = x.DelineationTypeID,
                    IsVerified = x.IsVerified,
                    VerifiedByPersonID = x.VerifiedByPersonID,
                    DateLastModified = DateTime.UtcNow,
                    HasDiscrepancies = x.HasDiscrepancies
                }).ToList();
                
            dbContext.Delineations.AddRange(newDelineations);
            dbContext.SaveChanges();

            return newProject;
        }

        public static List<TreatmentBMPHRUCharacteristicsSummarySimpleDto> GetTreatmentBMPHRUCharacteristicSimplesForProject(HippocampDbContext dbContext, int projectID)
        {
            var projectTreatmentBMPs = dbContext.Projects
                .Include(x => x.TreatmentBMPs)
                .ThenInclude(x => x.Delineation)
                .Include(x => x.TreatmentBMPs)
                .ThenInclude(x => x.TreatmentBMPType)
                .Where(x => x.ProjectID == projectID).SelectMany(x => x.TreatmentBMPs).ToList();

            if (!projectTreatmentBMPs.Any())
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
            return GetProjectsImpl(dbContext).Where(x => x.ShareOCTAM2Tier2Scores).ToList();
        }

        public static List<int> ListOCTAM2Tier2ProjectIDs(HippocampDbContext dbContext)
        {
            return dbContext.Projects.AsNoTracking().Where(x => x.ShareOCTAM2Tier2Scores).Select(x => x.ProjectID).ToList();
        }

        public static List<ProjectModeledResultSummaryDto> ListByIDsAsModeledResultSummaryDtos(HippocampDbContext dbContext, List<int> projectIDs)
        {
            var projects = dbContext.Projects.Where(x => projectIDs.Contains(x.ProjectID)).ToList();
            var projectLoadReducingResultsGroups = ProjectLoadReducingResults
                .ListByProjectIDs(dbContext, projectIDs)
                .GroupBy(x => x.ProjectID);

            var allProjectHRUCharacteristics = dbContext.ProjectHRUCharacteristics
                .Where(x => projectIDs.Contains(x.ProjectID)).ToList();
            var projectModeledResultSummaryDtos = new List<ProjectModeledResultSummaryDto>();

            foreach (var projectLoadReducingResultsGroup in projectLoadReducingResultsGroups)
            {
                var projectID = projectLoadReducingResultsGroup.Key;
                var project = projects.SingleOrDefault(x => x.ProjectID == projectID);
                var projectHRUCharacteristics = allProjectHRUCharacteristics.Where(x => x.ProjectID == projectID).ToList();
                var area = projectHRUCharacteristics.Any() ? projectHRUCharacteristics.Sum(x => x.Area) : (double?)null;
                var imperviousAcres = projectHRUCharacteristics.Any() ? projectHRUCharacteristics.Sum(x => x.ImperviousAcres) : (double?)null;

                var projectModeledResultSummaryDto = new ProjectModeledResultSummaryDto()
                {
                    ProjectName = projectLoadReducingResultsGroup.First().ProjectName,
                    Jurisdiction = projectLoadReducingResultsGroup.First().JurisdictionName,
                    Organization = projectLoadReducingResultsGroup.First().OrganizationName,
                    AcresTreated = area ?? 0,
                    ImperviousAcresTreated = imperviousAcres ?? 0,

                    WetWeatherInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherInflow ?? 0),
                    WetWeatherTreated = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTreated ?? 0),
                    WetWeatherRetained = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherRetained ?? 0),
                    WetWeatherUntreated = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherUntreated ?? 0),
                    WetWeatherTSSReduced = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTSSReduced ?? 0),
                    WetWeatherTNReduced = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTNReduced ?? 0),
                    WetWeatherTPReduced = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTPReduced ?? 0),
                    WetWeatherFCReduced = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherFCReduced ?? 0),
                    WetWeatherTCuReduced = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTCuReduced ?? 0),
                    WetWeatherTPbReduced = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTPbReduced ?? 0),
                    WetWeatherTZnReduced = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTZnReduced ?? 0),
                    WetWeatherTSSInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTSSInflow ?? 0),
                    WetWeatherTNInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTNInflow ?? 0),
                    WetWeatherTPInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTPInflow ?? 0),
                    WetWeatherFCInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherFCInflow ?? 0),
                    WetWeatherTCuInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTCuInflow ?? 0),
                    WetWeatherTPbInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTPbInflow ?? 0),
                    WetWeatherTZnInflow = projectLoadReducingResultsGroup.Sum(x => x.WetWeatherTZnInflow ?? 0),

                    DryWeatherInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherInflow),
                    DryWeatherTreated = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTreated),
                    DryWeatherRetained = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherRetained),
                    DryWeatherUntreated = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherUntreated),
                    DryWeatherTSSReduced = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTSSReduced),
                    DryWeatherTNReduced = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTNReduced),
                    DryWeatherTPReduced = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTPReduced),
                    DryWeatherFCReduced = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherFCReduced),
                    DryWeatherTCuReduced = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTCuReduced),
                    DryWeatherTPbReduced = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTPbReduced),
                    DryWeatherTZnReduced = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTZnReduced),
                    DryWeatherTSSInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTSSInflow),
                    DryWeatherTNInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTNInflow),
                    DryWeatherTPInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTPInflow),
                    DryWeatherFCInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherFCInflow),
                    DryWeatherTCuInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherFCInflow),
                    DryWeatherTPbInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTPbInflow),
                    DryWeatherTZnInflow = projectLoadReducingResultsGroup.Sum(x => x.DryWeatherTZnInflow),

                    TotalInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalInflow),
                    TotalTreated = projectLoadReducingResultsGroup.Sum(x => x.TotalTreated),
                    TotalRetained = projectLoadReducingResultsGroup.Sum(x => x.TotalRetained),
                    TotalUntreated = projectLoadReducingResultsGroup.Sum(x => x.TotalUntreated),
                    TotalTSSReduced = projectLoadReducingResultsGroup.Sum(x => x.TotalTSSReduced),
                    TotalTNReduced = projectLoadReducingResultsGroup.Sum(x => x.TotalTNReduced),
                    TotalTPReduced = projectLoadReducingResultsGroup.Sum(x => x.TotalTPReduced),
                    TotalFCReduced = projectLoadReducingResultsGroup.Sum(x => x.TotalFCReduced),
                    TotalTCuReduced = projectLoadReducingResultsGroup.Sum(x => x.TotalTCuReduced),
                    TotalTPbReduced = projectLoadReducingResultsGroup.Sum(x => x.TotalTPbReduced),
                    TotalTZnReduced = projectLoadReducingResultsGroup.Sum(x => x.TotalTZnReduced),
                    TotalTSSInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalTSSInflow),
                    TotalTNInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalTNInflow),
                    TotalTPInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalTPInflow),
                    TotalFCInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalFCInflow),
                    TotalTCuInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalFCInflow),
                    TotalTPbInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalTPbInflow),
                    TotalTZnInflow = projectLoadReducingResultsGroup.Sum(x => x.TotalTZnInflow)
                };

                projectModeledResultSummaryDto.SEA = project.SEA;
                projectModeledResultSummaryDto.TPI = project.TPI;
                projectModeledResultSummaryDto.DryWeatherWQLRI = project.DryWeatherWQLRI;
                projectModeledResultSummaryDto.WetWeatherWQLRI = project.WetWeatherWQLRI;

                projectModeledResultSummaryDtos.Add(projectModeledResultSummaryDto);
            }

            return projectModeledResultSummaryDtos;
        }
        public static void SetUpdatePersonAndDate(HippocampDbContext dbContext, int projectID, int personID)
        {
            var project = dbContext.Projects.Single(x => x.ProjectID == projectID);
            project.UpdatePersonID = personID;
            project.DateUpdated = DateTime.UtcNow;
        }
    }
}