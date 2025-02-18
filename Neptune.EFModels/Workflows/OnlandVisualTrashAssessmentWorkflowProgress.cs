using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Workflows;

public class OnlandVisualTrashAssessmentWorkflowProgress
{
    public enum OnlandVisualTrashAssessmentWorkflowStep
    {
        Instructions,
        InitiateOvta,
        RecordObservations,
        AddOrRemoveParcels,
        RefineAssessmentArea,
        ReviewAndFinalize
    }

    public static OnlandVisualTrashAssessmentWorkflowProgressDto GetProgress(OnlandVisualTrashAssessment OnlandVisualTrashAssessment)
    {
        return new OnlandVisualTrashAssessmentWorkflowProgressDto
        {
            OnlandVisualTrashAssessmentID = OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentID,
            OnlandVisualTrashAssessmentName = OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
            OnlandVisualTrashAssessmentStatus = OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatus.AsSimpleDto(),
            Steps = Enum.GetValuesAsUnderlyingType<OnlandVisualTrashAssessmentWorkflowStep>().Cast<OnlandVisualTrashAssessmentWorkflowStep>()
                .ToDictionary(x => x, y => new ProjectWorkflowProgress.WorkflowStepStatus()
                {
                    Completed = WorkflowStepComplete(OnlandVisualTrashAssessment, y),
                    Disabled = !WorkflowStepActive(OnlandVisualTrashAssessment, y),
                })
        };
    }

    public static bool CanSubmit(NeptuneDbContext dbContext, OnlandVisualTrashAssessment OnlandVisualTrashAssessment)
    {
        var steps = Enum.GetValuesAsUnderlyingType<OnlandVisualTrashAssessmentWorkflowStep>().Cast<OnlandVisualTrashAssessmentWorkflowStep>();
        foreach (var step in steps)
        {
            var stepComplete = WorkflowStepComplete(OnlandVisualTrashAssessment, step);
            if (!stepComplete) return false;
        }

        return true;
    }

    public static bool CanDelete(NeptuneDbContext dbContext, OnlandVisualTrashAssessment OnlandVisualTrashAssessment, Person currentUser)
    {
        return currentUser.CanEditJurisdiction(OnlandVisualTrashAssessment.StormwaterJurisdictionID, dbContext);
    }

    public static bool WorkflowStepComplete(OnlandVisualTrashAssessment OnlandVisualTrashAssessment, OnlandVisualTrashAssessmentWorkflowStep wellRegistryWorkflowStep)
    {
        switch (wellRegistryWorkflowStep)
        {
            case OnlandVisualTrashAssessmentWorkflowStep.Instructions:
                return true;
            case OnlandVisualTrashAssessmentWorkflowStep.InitiateOvta:
                return OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentID > 0;
            case OnlandVisualTrashAssessmentWorkflowStep.RecordObservations:
                break;
            case OnlandVisualTrashAssessmentWorkflowStep.AddOrRemoveParcels:
                break;
            case OnlandVisualTrashAssessmentWorkflowStep.RefineAssessmentArea:
                break;
            case OnlandVisualTrashAssessmentWorkflowStep.ReviewAndFinalize:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(wellRegistryWorkflowStep), wellRegistryWorkflowStep, null);
        }
        return false;
    }

    public static bool WorkflowStepActive(OnlandVisualTrashAssessment OnlandVisualTrashAssessment, OnlandVisualTrashAssessmentWorkflowStep wellRegistryWorkflowStep)
    {
        switch (wellRegistryWorkflowStep)
        {
            case OnlandVisualTrashAssessmentWorkflowStep.Instructions:
                return true;

            case OnlandVisualTrashAssessmentWorkflowStep.ReviewAndFinalize:
                return OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentID > 0;

            case OnlandVisualTrashAssessmentWorkflowStep.InitiateOvta:
                return OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentID == 0;
            case OnlandVisualTrashAssessmentWorkflowStep.RecordObservations:
                break;
            case OnlandVisualTrashAssessmentWorkflowStep.AddOrRemoveParcels:
                break;
            case OnlandVisualTrashAssessmentWorkflowStep.RefineAssessmentArea:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(wellRegistryWorkflowStep), wellRegistryWorkflowStep, null);
        }

        return false;
    }

    public class OnlandVisualTrashAssessmentWorkflowProgressDto
    {
        public int OnlandVisualTrashAssessmentID { get; set; }
        public string OnlandVisualTrashAssessmentName { get; set; }
        public OnlandVisualTrashAssessmentStatusSimpleDto OnlandVisualTrashAssessmentStatus { get; set; }
        public Dictionary<OnlandVisualTrashAssessmentWorkflowStep, ProjectWorkflowProgress.WorkflowStepStatus> Steps { get; set; }
    }
}