using System;
using Neptune.Web.Areas.Modeling.NereidModels;
using Neptune.Web.Models;
using System.Collections.Generic;
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

            var graph = new Graph(true, nodes, edges);
            return graph;
        }


        private static void MakeRSBNodesAndEdges(DatabaseEntities dbContext, out List<Edge> rsbEdges, out List<Node> rsbNodes)
        {
            rsbNodes = dbContext.RegionalSubbasins.Where(x=>x.IsInLSPCBasin == true)
                .Select(x => new Node { ID = "RSB_" + x.RegionalSubbasinID }).ToList();
            
            rsbEdges = dbContext.RegionalSubbasins
                .Where(x => x.OCSurveyDownstreamCatchmentID != null).Select(x =>
                    new Edge()
                    {
                        SourceID = "RSB_" + x.RegionalSubbasinID,
                        TargetID = "RSB_" + x.RegionalSubbasinID
                    }).ToList();
        }

        private static void MakeDistributedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> distributedBMPEdges, out List<Node> distributedBMPNodes)
        {
            distributedBMPNodes = dbContext.vNereidTreatmentBMPRegionalSubbasins.Select(x => new Node() { ID = "BMP_" + x.TreatmentBMPID }).ToList();
            distributedBMPEdges = dbContext.vNereidTreatmentBMPRegionalSubbasins.Select(x => new Edge()
            {
                SourceID = "BMP_" + x.TreatmentBMPID,
                TargetID = "RSB_" + x.RegionalSubbasinID
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
            var wqmpRSBPairings = dbContext.LoadGeneratingUnits
                .Where(x => x.WaterQualityManagementPlan != null && x.RegionalSubbasinID != null)
                .Select(x => new {x.WaterQualityManagementPlanID, x.RegionalSubbasinID}).Distinct().ToList();

            wqmpNodes = wqmpRSBPairings.Select(x => new Node
            {
                ID = "WQMP_" + x.WaterQualityManagementPlanID + "_RSB_" + x.RegionalSubbasinID
            }).ToList();

            wqmpEdges = wqmpRSBPairings.Select(x => new Edge
            {
                SourceID = "WQMP_" + x.WaterQualityManagementPlanID + "_RSB_" + x.RegionalSubbasinID,
                TargetID = "RSB_" + x.RegionalSubbasinID
            }).ToList();
        }

        private static void MakeCentralizedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> o,
            out List<Node> o1)
        {
            throw new System.NotImplementedException();
        }
    }
}