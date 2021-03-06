﻿using System.Collections.Generic;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class SolutionSequenceRequest
    {
        [JsonProperty("directed")]
        public bool Directed { get; set; }

        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }

        public SolutionSequenceRequest()
        {
            Directed = true;
        }

        public SolutionSequenceRequest(Graph graph)
        {
            Edges = graph.Edges;
            Directed = true;
        }
    }

    public class GenericNeriedResponse
    {
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }
    }
}