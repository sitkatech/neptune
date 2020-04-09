using Neptune.Web.Areas.Modeling.NereidModels;
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

            //MakeCentralizedBMPNodesAndEdges(dbContext, out var centralizedBMPEdges, out var centralizedBMPNodes, edges);
            //nodes.AddRange(centralizedBMPNodes);
            //edges.AddRange(centralizedBMPEdges);

            var graph = new Graph(true, nodes, edges);
            return graph;
        }


        private static void MakeRSBNodesAndEdges(DatabaseEntities dbContext, out List<Edge> rsbEdges, out List<Node> rsbNodes)
        {
            rsbNodes = dbContext.RegionalSubbasins.Where(x=>x.IsInLSPCBasin == true)
                .Select(x => new Node { ID = "RSB_" + x.OCSurveyCatchmentID }).ToList();
            
            rsbEdges = dbContext.RegionalSubbasins.Where(x => x.IsInLSPCBasin == true)
                .Where(x => x.OCSurveyDownstreamCatchmentID != null).Select(x =>
                    new Edge()
                    {
                        SourceID = "RSB_" + x.OCSurveyCatchmentID,
                        TargetID = "RSB_" + x.OCSurveyDownstreamCatchmentID
                    }).ToList();
        }

        private static void MakeDistributedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> distributedBMPEdges, out List<Node> distributedBMPNodes)
        {
            distributedBMPNodes = dbContext.vNereidTreatmentBMPRegionalSubbasins.Select(x => new Node() { ID = "BMP_" + x.TreatmentBMPID }).ToList();
            distributedBMPEdges = dbContext.vNereidTreatmentBMPRegionalSubbasins.Select(x => new Edge()
            {
                SourceID = "BMP_" + x.TreatmentBMPID,
                TargetID = "RSB_" + x.OCSurveyCatchmentID
            }).ToList();
        }

        private static void MakeDistributedDelineationNodesAndEdges(DatabaseEntities dbContext, out List<Edge> delineationEdges, out List<Node> delineationNodes)
        {
            delineationNodes = dbContext.Delineations.Where(x => x.DelineationTypeID == DelineationType.Distributed.DelineationTypeID)
                .Select(x => new Node()
                {
                    ID = "Delineation_" + x.DelineationID
                }).ToList();

            delineationEdges = dbContext.Delineations.Where(x => x.DelineationTypeID == DelineationType.Distributed.DelineationTypeID)
                .Select(x => new Edge()
                {
                    SourceID = "Delineation_" + x.DelineationID,
                    TargetID = "BMP_" + x.TreatmentBMPID
                }).ToList();
        }

        private static void MakeUpstreamBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> colocationEdges, out List<Node> colocationNodes)
        {
            // we only need to add the upstream nodes, because any bmp that's not an upstream node has already been added
            
            colocationNodes = dbContext.vNereidBMPColocations
                .Select(x => new Node()
                {
                    ID = "BMP_" + x.UpstreamBMPID
                }).ToList();

            colocationEdges = dbContext.vNereidBMPColocations
                .Select(x => new Edge()
                {
                    SourceID = "BMP_" + x.UpstreamBMPID,
                    TargetID = "BMP_" + x.DownstreamBMPID
                }).ToList();
        }

        private static void MakeWQMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> wqmpEdges, out List<Node> wqmpNodes)
        {
            var wqmpRSBPairings = dbContext.LoadGeneratingUnits.Include(x=>x.RegionalSubbasin)
                .Where(x => x.WaterQualityManagementPlan != null && x.RegionalSubbasinID != null)
                .Select(x => new {x.WaterQualityManagementPlanID, x.RegionalSubbasinID, x.RegionalSubbasin.OCSurveyCatchmentID}).Distinct().ToList();

            wqmpNodes = wqmpRSBPairings.Select(x => new Node
            {
                ID = "WQMP_" + x.WaterQualityManagementPlanID + "_RSB_" + x.OCSurveyCatchmentID
            }).ToList();

            wqmpEdges = wqmpRSBPairings.Select(x => new Edge
            {
                SourceID = "WQMP_" + x.WaterQualityManagementPlanID + "_RSB_" + x.OCSurveyCatchmentID,
                TargetID = "RSB_" + x.OCSurveyCatchmentID
            }).ToList();
        }

        private static void MakeCentralizedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> centralizedBMPEdges,
            out List<Node> centralizedBMPNodes, List<Edge> existingEdges)
        {
            centralizedBMPNodes = new List<Node>();
            centralizedBMPEdges = new List<Edge>();
            /*
             * store current rsb target as ‘og_target’
All other rsbs with the current rsb as their target must now point to the centralized bmp id (i.e. replace all in target column)
Current rsb target equals centralized bmp node id
centralized bmp target = ‘og_target’
by doing this last, we can ensure that all other nodes in the table correctly have their ‘target’ attribute reset if they should drain to a centralized bmp. Doing this last means we are including distributed facility nodes and wqmp sites in the corrective action of the second step of the centralized node insertion.
             */
            foreach (var rsbCentralizedBMPPairing in dbContext.vNereidRegionalSubbasinCentralizedBMPs.ToList())
            {
                var newCentralizedBMPNodeID = "BMP_" + rsbCentralizedBMPPairing.TreatmentBMPID;
                var existingRSBNodeID = "RSB_" + rsbCentralizedBMPPairing.RegionalSubbasinID;
                
                
                // Find Edge with this RSB as its Source. Store its Target as ‘og_target’
                // If this is null, it means this RSB is at the end of the flow network.
                // We'll create an edge from RSB to BMP, but we won't create an edge from BMP to anywhere
                var edgeWithThisNodeAsSource = existingEdges.SingleOrDefault(x=>x.SourceID == existingRSBNodeID);
                if (edgeWithThisNodeAsSource != null)
                {
                    // Current rsb target equals centralized bmp node id
                    edgeWithThisNodeAsSource.TargetID = newCentralizedBMPNodeID;
                }
                else
                {
                    centralizedBMPEdges.Add(new Edge
                        {SourceID = existingRSBNodeID, TargetID = newCentralizedBMPNodeID});
                }

                var ogTargetID = edgeWithThisNodeAsSource?.TargetID;

                // Find all Edges that point to this RSB. Set their Target to the new Centralized BMP Node
                foreach (var edge in existingEdges.Where(x=>x.TargetID == existingRSBNodeID))
                {
                    edge.TargetID = newCentralizedBMPNodeID;
                }


                // Centralized BMP gets an edge pointing to "og_target"
                centralizedBMPNodes.Add(new Node{ID = newCentralizedBMPNodeID});
                // see above
                if (ogTargetID != null)
                {
                    centralizedBMPEdges.Add(new Edge() {SourceID = newCentralizedBMPNodeID, TargetID = ogTargetID});
                }
            }
        }
    }
}
