using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Workflows;

public class ProjectWorkflowProgress
{
    public enum ProjectWorkflowStep
    {
        Instructions,
        BasicInfo,
        TreatmentBMPs,
        Delineations,
        ModeledPerformanceAndGrantMetrics,
        Attachments,
        ReviewAndShare,
    }

    public static ProjectWorkflowProgressDto GetProgress(Project project)
    {
        return new ProjectWorkflowProgressDto
        {
            ProjectID = project.ProjectID,
            StormwaterJurisdictionID = project.StormwaterJurisdictionID,
            ProjectStatus = project.ProjectStatus.AsSimpleDto(),
            Steps = Enum.GetValuesAsUnderlyingType<ProjectWorkflowStep>().Cast<ProjectWorkflowStep>()
                .ToDictionary(x => x, y => WorkflowStepComplete(project, y))
        };
    }

    public static bool CanSubmit(NeptuneDbContext dbContext, Project project)
    {
        var steps = Enum.GetValuesAsUnderlyingType<ProjectWorkflowStep>().Cast<ProjectWorkflowStep>();
        foreach (var step in steps)
        {
            var stepComplete = WorkflowStepComplete(project, step);
            if (!stepComplete) return false;
        }

        return true;
    }

    public static bool CanDelete(NeptuneDbContext dbContext, Project project, Person currentUser)
    {
        return currentUser.CanEditJurisdiction(project.StormwaterJurisdictionID, dbContext);
    }

    public static bool WorkflowStepComplete(Project project, ProjectWorkflowStep wellRegistryWorkflowStep)
    {
        switch (wellRegistryWorkflowStep)
        {
            case ProjectWorkflowStep.Instructions:
                return true;
            case ProjectWorkflowStep.ModeledPerformanceAndGrantMetrics:
                if (project.DoesNotIncludeTreatmentBMPs)
                {
                    return true;
                }

                var treatmentBMPIDsWithNereidResults = project.ProjectNereidResults.Where(x => x.TreatmentBMPID.HasValue).Select(x => x.TreatmentBMPID.Value).Distinct().ToList();
                var treatmentBMPIDs = project.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
                return new HashSet<int>(treatmentBMPIDs).SetEquals(treatmentBMPIDsWithNereidResults);
            case ProjectWorkflowStep.TreatmentBMPs:
                if (project.DoesNotIncludeTreatmentBMPs)
                {
                    return true;
                }
                return  project.TreatmentBMPs.Any() && project.TreatmentBMPs.All(x => !x.TreatmentBMPType.HasMissingModelingAttributes(x.TreatmentBMPModelingAttributeTreatmentBMP));
            case ProjectWorkflowStep.Delineations:
                if (project.DoesNotIncludeTreatmentBMPs)
                {
                    return true;
                }
                return project.TreatmentBMPs.Any() && project.TreatmentBMPs.All(x => x.Delineation != null);
            case ProjectWorkflowStep.BasicInfo:
                return !string.IsNullOrWhiteSpace(project.ProjectName)
                       && project is { OrganizationID: > 0, PrimaryContactPersonID: > 0, StormwaterJurisdictionID: > 0 };
            case ProjectWorkflowStep.ReviewAndShare:
                return true;
            case ProjectWorkflowStep.Attachments:
                return true;
            default:
                throw new ArgumentOutOfRangeException(nameof(wellRegistryWorkflowStep), wellRegistryWorkflowStep, null);
        }
    }

    public class ProjectWorkflowProgressDto
    {   
        public int ProjectID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public ProjectStatusSimpleDto ProjectStatus { get; set; }
        public Dictionary<ProjectWorkflowStep, bool> Steps { get; set; }
    }
}