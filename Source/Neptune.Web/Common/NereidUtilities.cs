using Neptune.Web.Areas.Modeling.NereidModels;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Common
{
    public static class NereidUtilities
    {
        public static Graph BuildRSBNetworkGraph(DatabaseEntities dbContext)
        {
            var nodes = new List<Node>();
            var edges = new List<Edge>();

            MakeRSBNodesAndEdges(dbContext, out var rsbEdges, out var rsbNodes);
            nodes.AddRange(rsbNodes);
            edges.AddRange(rsbEdges);

            
            var graph = new Graph(true, nodes, edges);
            return graph;
        }

        public static Graph BuildNetworkGraph(DatabaseEntities dbContext)
        {
            var nodes = new List<Node>();
            var edges = new List<Edge>();

            MakeRSBNodesAndEdges(dbContext, out var rsbEdges, out var rsbNodes);
            nodes.AddRange(rsbNodes);
            edges.AddRange(rsbEdges);

            MakeDistributedBMPNodesAndEdges(dbContext, out var distributedBMPEdges, out var distributedBMPNodes);


            var graph = new Graph(true, nodes, edges);
            return graph;
        }


        private static void MakeRSBNodesAndEdges(DatabaseEntities dbContext, out List<Edge> rsbEdges, out List<Node> rsbNodes)
        {
            rsbNodes = dbContext.RegionalSubbasins
                .Select(x => new Node {ID = x.OCSurveyCatchmentID.ToString()}).ToList();


            rsbEdges = dbContext.RegionalSubbasins
                .Where(x => x.OCSurveyDownstreamCatchmentID != null).Select(x =>
                    new Edge()
                    {
                        SourceID = x.OCSurveyCatchmentID.ToString(), TargetID = x.OCSurveyDownstreamCatchmentID.ToString()
                    }).ToList();
        }

        private static void MakeDistributedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> o, out List<Node> o1)
        {
            throw new System.NotImplementedException();
        }
        
        private static void MakeDistributedDelineationNodesAndEdges(DatabaseEntities dbContext, out List<Edge> o, out List<Node> o1)
        {
            throw new System.NotImplementedException();
        }
        
        private static void MakeUpstreamBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> o, out List<Node> o1)
        {
            throw new System.NotImplementedException();
        }

        private static void MakeWQMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> o, out List<Node> o1)
        {
            throw new System.NotImplementedException();
        }

        private static void MakeCentralizedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> o,
            out List<Node> o1)
        {
            throw new System.NotImplementedException();
        }
    }
}