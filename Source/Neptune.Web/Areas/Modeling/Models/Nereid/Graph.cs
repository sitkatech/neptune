﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class Graph
    {
        [JsonProperty("directed")]
        public bool Directed { get; set; }
        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; }
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }

        public Graph(bool directed, List<Node> nodes, List<Edge> edges)
        {
            Directed = directed;
            Nodes = nodes;
            Edges = edges;
        }

        public Graph() { }
    }

    public static class GraphExtensionMethods
    {
        public static Graph GetUpstreamSubgraph(this Graph graph, Node node)
        {
            var edgesToAdd = graph.Edges.Where(x=>x.TargetID == node.ID).ToList();

            List<Edge> subgraphEdges = new List<Edge>();
            List<Node> subgraphNodes = new List<Node>(){node};
            while (edgesToAdd.Any())
            {
                subgraphEdges.AddRange(edgesToAdd);
                var sourceNodeIDs = edgesToAdd.Select(x => x.SourceID).ToList();
                var sourceNodesToAdd = graph.Nodes.Where(x => sourceNodeIDs.Contains(x.ID)).ToList();
                subgraphNodes.AddRange(sourceNodesToAdd);

                edgesToAdd = graph.Edges.Where(x => sourceNodeIDs.Contains(x.TargetID)).ToList();
            }

            return new Graph(true, subgraphNodes, subgraphEdges);
        }
    }
}