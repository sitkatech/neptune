using Neptune.Web.Areas.Modeling.Models.Nereid;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Neptune.Web.Common
{
    public static class NereidUtilities
    {
        public static Graph BuildNetworkGraph(DatabaseEntities dbContext)
        {
            var nodes = new List<Node>();
            var edges = new List<Edge>();

            MakeRSBNodesAndEdges(dbContext, out var rsbEdges, out var rsbNodes);
            nodes.AddRange(rsbNodes);
            edges.AddRange(rsbEdges);

            MakeDistributedBMPNodesAndEdges(dbContext, out var distributedBMPEdges, out var distributedBMPNodes);
            nodes.AddRange(distributedBMPNodes);
            edges.AddRange(distributedBMPEdges);

            MakeDistributedDelineationNodesAndEdges(dbContext, out var delineationEdges, out var delineationNodes);
            nodes.AddRange(delineationNodes);
            edges.AddRange(delineationEdges);

            MakeUpstreamBMPNodesAndEdges(dbContext, out var colocationEdges, out var colocationNodes);
            nodes.AddRange(colocationNodes);
            edges.AddRange(colocationEdges);

            MakeWQMPNodesAndEdges(dbContext, out var wqmpEdges, out var wqmpNodes);
            nodes.AddRange(wqmpNodes);
            edges.AddRange(wqmpEdges);

            MakeCentralizedBMPNodesAndEdges(dbContext, out var centralizedBMPEdges, out var centralizedBMPNodes, edges);
            nodes.AddRange(centralizedBMPNodes);
            edges.AddRange(centralizedBMPEdges);

            var graph = new Graph(true, nodes, edges);
            return graph;
        }

        public static string RegionalSubbasinNodeID(RegionalSubbasin regionalSubbasin)
        {
            return "RSB_" + regionalSubbasin.OCSurveyCatchmentID;
        }
        public static string RegionalSubbasinNodeID(int regionalSubbasinCatchmentID)
        {
            return "RSB_" + regionalSubbasinCatchmentID;
        }

        public static string TreatmentBMPNodeID(TreatmentBMP treatmentBMP)
        {
            return "BMP_" + treatmentBMP.TreatmentBMPID;
        }

        public static string TreatmentBMPNodeID(int treatmentBMPID)
        {
            return "BMP_" + treatmentBMPID;
        }

        public static string WaterQualityManagementPlanNodeID(int waterQualityManagementPlanID,
            int regionalSubbasinCatchmentID)
        {
            return "WQMP_" + waterQualityManagementPlanID + "_RSB_" + regionalSubbasinCatchmentID;
        }

        public static string DelineationNodeID(Delineation delineation)
        {
            return "Delineation_" + delineation.DelineationID;
        }

        public static void MakeRSBNodesAndEdges(DatabaseEntities dbContext, out List<Edge> rsbEdges, out List<Node> rsbNodes)
        {
            var regionalSubbasinsInCoverage = dbContext.RegionalSubbasins.Where(x => x.IsInLSPCBasin == true).ToList();

            rsbNodes = regionalSubbasinsInCoverage
                .Select(x => new Node { ID = RegionalSubbasinNodeID(x) }).ToList();

            rsbEdges = regionalSubbasinsInCoverage
                .Where(x => x.OCSurveyDownstreamCatchmentID != null).Select(x =>
                    new Edge()
                    {
                        SourceID = RegionalSubbasinNodeID(x),
                        TargetID = RegionalSubbasinNodeID(x.OCSurveyDownstreamCatchmentID.Value)
                    }).ToList();
        }

        public static void MakeDistributedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> distributedBMPEdges, out List<Node> distributedBMPNodes)
        {
            var vNereidTreatmentBMPRegionalSubbasins = dbContext.vNereidTreatmentBMPRegionalSubbasins.ToList();
            distributedBMPNodes = vNereidTreatmentBMPRegionalSubbasins
                .Select(x => new Node() { ID = TreatmentBMPNodeID(x.TreatmentBMPID) }).ToList();

            distributedBMPEdges = vNereidTreatmentBMPRegionalSubbasins.Select(x => new Edge()
            {
                SourceID = TreatmentBMPNodeID(x.TreatmentBMPID),
                TargetID = RegionalSubbasinNodeID(x.OCSurveyCatchmentID)
            }).ToList();
        }

        public static void MakeDistributedDelineationNodesAndEdges(DatabaseEntities dbContext, out List<Edge> delineationEdges, out List<Node> delineationNodes)
        {
            var distributedDelineations = dbContext.Delineations
                .Where(x => x.DelineationTypeID == DelineationType.Distributed.DelineationTypeID).ToList();

            delineationNodes = distributedDelineations
                .Select(x => new Node()
                {
                    ID = DelineationNodeID(x)
                }).ToList();

            delineationEdges = distributedDelineations
                .Select(x => new Edge()
                {
                    SourceID = DelineationNodeID(x),
                    TargetID = TreatmentBMPNodeID(x.TreatmentBMP)
                }).ToList();
        }

        private static void MakeUpstreamBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> colocationEdges, out List<Node> colocationNodes)
        {
            // we only need to add the upstream nodes, because any bmp that's not an upstream node has already been added

            var bmpColocations = dbContext.vNereidBMPColocations.ToList();
            colocationNodes = bmpColocations
                .Select(x => new Node()
                {
                    ID = TreatmentBMPNodeID(x.UpstreamBMPID)
                }).ToList();

            colocationEdges = bmpColocations
                .Select(x => new Edge()
                {
                    SourceID = TreatmentBMPNodeID(x.UpstreamBMPID),
                    TargetID = TreatmentBMPNodeID(x.DownstreamBMPID)
                }).ToList();
        }

        private static void MakeWQMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> wqmpEdges, out List<Node> wqmpNodes)
        {
            var wqmpRSBPairings = GetWaterQualityManagementPlanNodes(dbContext).ToList();

            wqmpNodes = wqmpRSBPairings.Select(x => new Node
            {
                ID = WaterQualityManagementPlanNodeID(x.WaterQualityManagementPlanID, x.OCSurveyCatchmentID)
            }).ToList();

            wqmpEdges = wqmpRSBPairings.Select(x => new Edge
            {
                SourceID = WaterQualityManagementPlanNodeID(x.WaterQualityManagementPlanID, x.OCSurveyCatchmentID),
                TargetID = RegionalSubbasinNodeID(x.OCSurveyCatchmentID)
            }).ToList();
        }

        public static IQueryable<WaterQualityManagementPlanNode> GetWaterQualityManagementPlanNodes(DatabaseEntities dbContext)
        {
            return dbContext.LoadGeneratingUnits.Include(x => x.RegionalSubbasin)
                .Where(x => x.WaterQualityManagementPlan != null && x.RegionalSubbasinID != null)
                .Select(x => new WaterQualityManagementPlanNode
                {
                    WaterQualityManagementPlanID = x.WaterQualityManagementPlanID.Value,
                    RegionalSubbasinID = x.RegionalSubbasinID.Value,
                    OCSurveyCatchmentID = x.RegionalSubbasin.OCSurveyCatchmentID
                }).Distinct();
        }

        private static void MakeCentralizedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> centralizedBMPEdges,
            out List<Node> centralizedBMPNodes, List<Edge> existingEdges)
        {
            centralizedBMPNodes = new List<Node>();
            centralizedBMPEdges = new List<Edge>();

            // todo: We're selecting by RowNumber == 1 so the network isn't invalid when there are
            // multiple centralized delineations per one regional subbasin. In future, we'll need
            // to find something better to do about that case than ignore the additionals.

            foreach (var rsbCentralizedBMPPairing in dbContext.vNereidRegionalSubbasinCentralizedBMPs.Where(x => x.RowNumber == 1).ToList())
            {
                var newCentralizedBMPNodeID = TreatmentBMPNodeID(rsbCentralizedBMPPairing.TreatmentBMPID);
                var existingRSBNodeID = RegionalSubbasinNodeID(rsbCentralizedBMPPairing.OCSurveyCatchmentID);


                // Find Edge with this RSB as its Source. Store its Target as ‘og_target’
                // If this is null, it means this RSB is at the end of the flow network.
                // We'll create an edge from RSB to BMP, but we won't create an edge from BMP to anywhere
                var edgeWithThisNodeAsSource = existingEdges.SingleOrDefault(x => x.SourceID == existingRSBNodeID);
                var ogTargetID = edgeWithThisNodeAsSource?.TargetID;

                if (edgeWithThisNodeAsSource != null)
                {
                    // Current rsb target equals centralized bmp node id
                    edgeWithThisNodeAsSource.TargetID = newCentralizedBMPNodeID;
                }
                else
                {
                    centralizedBMPEdges.Add(new Edge
                    { SourceID = existingRSBNodeID, TargetID = newCentralizedBMPNodeID });
                }

                // Find all Edges that point to this RSB. Set their Target to the new Centralized BMP Node
                foreach (var edge in existingEdges.Where(x => x.TargetID == existingRSBNodeID))
                {
                    edge.TargetID = newCentralizedBMPNodeID;
                }


                // Centralized BMP gets an edge pointing to "og_target"
                centralizedBMPNodes.Add(new Node { ID = newCentralizedBMPNodeID });
                // see above
                if (ogTargetID != null)
                {
                    centralizedBMPEdges.Add(new Edge() { SourceID = newCentralizedBMPNodeID, TargetID = ogTargetID });
                }
            }
        }
    }

    public class WaterQualityManagementPlanNode
    {
        public int WaterQualityManagementPlanID { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
    }
}
