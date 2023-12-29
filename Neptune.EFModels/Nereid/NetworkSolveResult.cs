using Neptune.EFModels.Entities;

namespace Neptune.EFModels.Nereid;

public class NetworkSolveResult
{
    public NetworkSolveResult(IEnumerable<NereidResult> nereidResults, Graph graph, List<string> missingNodeIDs)
    {
        NereidResults = nereidResults;
        MissingNodeIDs = missingNodeIDs;
        NodesProcessed = graph.Nodes.Count(x => x.Results != null);
    }

    public NetworkSolveResult()
    {
    }

    public IEnumerable<NereidResult> NereidResults { get; set; }
    public int NodesProcessed { get; set; }
    public List<string> MissingNodeIDs { get; set; }
}