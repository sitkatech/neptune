using Neptune.EFModels.Entities;

namespace Neptune.EFModels.Nereid;

public class SolutionSummary
{
    public int NodesProcessed { get; set; }
    public List<string> MissingNodeIDs { get; set; }
    public bool Failed { get; set; }
    public string ExceptionMessage { get; set; }
    public string InnerExceptionStackTrace { get; set; }
    public SolutionRequestObject FailingRequest { get; set; }
    public SolutionResponseObject FailureResponse { get; set; }
    public long SolveTime { get; set; }
}