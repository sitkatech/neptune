﻿using Hangfire;
using Neptune.Web.Areas.Modeling.NereidModels;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;
using Edge = Neptune.Web.Areas.Modeling.NereidModels.Edge;
using Node = Neptune.Web.Areas.Modeling.NereidModels.Node;

namespace Neptune.Web.Areas.Modeling.Controllers
{
    public class NereidController : NeptuneBaseController
    {
        public static HttpClient HttpClient { get; set; }

        static NereidController()
        {
            HttpClient = new HttpClient();
        }

        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult TriggerLGURun()
        {
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunLoadGeneratingUnitRefreshJob(null));
            return Content("LGU refresh will run in the background");
        }

        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult TriggerHRURun()
        {
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunHRURefreshJob());
            return Content("HRU refresh will run in the background");
        }

        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult TestNereidNetworkValidator()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/validate";

            var graph = new Graph(true, new List<Node>
                {
                    new Node("A"),
                    new Node("B")
                }, new List<Edge>
                {
                    new Edge ("A", "B")
                }
            );

            var serializedGraph = JsonConvert.SerializeObject(graph);
            var stringContent = new StringContent(serializedGraph);
            var postResultContentAsStringResult = HttpClient.PostAsync(networkValidatorUrl, stringContent).Result.Content.ReadAsStringAsync().Result;
            return Content(postResultContentAsStringResult);
        }

        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult Validate()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/validate";

            var buildGraphStartTime = DateTime.Now;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = DateTime.Now;

            var validateCallStartTime = DateTime.Now;
            var networkValidatorResult =
                RunJobAtNereid<Graph, NetworkValidatorResult>(graph, networkValidatorUrl, out var responseContent);

            var validateCallEndTime = DateTime.Now;

            var returnValue = new
            {
                NetworkValidatorResult = networkValidatorResult,
                BuildGraphElapsedTime = (buildGraphEndTime - buildGraphStartTime).Milliseconds,
                ValidateGraphElapsedTime = (validateCallEndTime - validateCallStartTime).Milliseconds,
                NodeCount = graph.Nodes.Count,
                EdgeCount = graph.Edges.Count,
            };

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult Subgraph()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/subgraph";

            var buildGraphStartTime = DateTime.Now;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = DateTime.Now;

            var subgraphRequestObject = new NereidSubgraphRequestObject(graph, new List<Node> { new Node("BMP_39") });
            var subgraphCallStartTime = DateTime.Now;

            var subgraphResult = RunJobAtNereid<NereidSubgraphRequestObject, SubgraphResult>(subgraphRequestObject,
                networkValidatorUrl, out var responseContent);
            var subgraphCallEndTime = DateTime.Now;
            
            var returnValue = new
            {
                SubgraphResult = subgraphResult,
                BuildGraphElapsedTime = (buildGraphEndTime - buildGraphStartTime).Milliseconds,
                SubgraphCallElapsedTime = (subgraphCallEndTime - subgraphCallStartTime).Milliseconds,
                NodeCount = graph.Nodes.Count,
                EdgeCount = graph.Edges.Count,
            };

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        // todo: the POCOs for this haven't been written yet
        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult SolutionSequence()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/solution_sequence";

            var buildGraphStartTime = DateTime.Now;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = DateTime.Now;

            var solutionSequenceRequestObject = new NereidSolutionSequenceRequestObject(graph);

            var subgraphCallStartTime = DateTime.Now;
            var responseObject =
                RunJobAtNereid<NereidSolutionSequenceRequestObject, object>(solutionSequenceRequestObject,
                    networkValidatorUrl, out var responseContent);
            var subgraphCallEndTime = DateTime.Now;
            
            var returnValue = new
            {
                SubgraphResult = responseContent,
                BuildGraphElapsedTime = (buildGraphEndTime - buildGraphStartTime).Milliseconds,
                SubgraphCallElapsedTime = (subgraphCallEndTime - subgraphCallStartTime).Milliseconds,
                NodeCount = graph.Nodes.Count,
                EdgeCount = graph.Edges.Count,
            };

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult Loading()
        {
            var landSurfaceLoadingUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/land_surface/loading?details=true&state=ca&region=soc";

            var regionalSubbasinsForTest = new List<int> { 4283, 8029, 4153 };
            var vNereidLoadingInputs = HttpRequestStorage.DatabaseEntities.vNereidLoadingInputs.Where(x => regionalSubbasinsForTest.Contains(x.RegionalSubbasinID)).ToList();
            var landSurfaceLoadingRequest = new LandSurfaceLoadingRequest(vNereidLoadingInputs);

            var subgraphCallStartTime = DateTime.Now;
            var responseObject = RunJobAtNereid<LandSurfaceLoadingRequest, object>(landSurfaceLoadingRequest, landSurfaceLoadingUrl, out var responseContent);
            var subgraphCallEndTime = DateTime.Now;
            
            var returnValue = new
            {
                SubgraphResult = responseContent,
                SubgraphCallElapsedTime = (subgraphCallEndTime - subgraphCallStartTime).Milliseconds,
            };

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        private static NereidResult<TResp> RunJobAtNereid<TReq, TResp>(TReq nereidRequestObject, string nereidRequestUrl, out string responseContent)
        {
            NereidResult<TResp> responseObject = null;
            var serializedRequest = JsonConvert.SerializeObject(nereidRequestObject);
            var requestStringContent = new StringContent(serializedRequest);

            var postResultContentAsStringResult = HttpClient.PostAsync(nereidRequestUrl, requestStringContent).Result
                .Content.ReadAsStringAsync().Result;

            var deserializeObject = JsonConvert.DeserializeObject<NereidResult<TResp>>(postResultContentAsStringResult);

            var executing = deserializeObject.Status == NereidJobStatus.STARTED;
            var resultRoute = deserializeObject.ResultRoute;

            responseContent = postResultContentAsStringResult;
            if (!executing)
            {
                responseObject = deserializeObject;
            }
            while (executing)
            {
                var stringResponse = HttpClient.GetAsync($"{NeptuneWebConfiguration.NereidUrl}{resultRoute}").Result.Content
                    .ReadAsStringAsync().Result;

                var continuePollingResponse =
                    JsonConvert.DeserializeObject<NereidResult<object>>(stringResponse);

                if (continuePollingResponse.Status != NereidJobStatus.STARTED)
                {
                    executing = false;
                    responseContent = stringResponse;
                    responseObject = JsonConvert.DeserializeObject<NereidResult<TResp>>(responseContent);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            return responseObject;
        }
    }
}

namespace Neptune.Web.Areas.Modeling.NereidModels
{
    public class LandSurfaceLoadingRequest
    {
        [JsonProperty("land_surfaces")]
        public List<LandSurface> LandSurfaces { get; set; }

        public LandSurfaceLoadingRequest()
        {

        }

        public LandSurfaceLoadingRequest(List<vNereidLoadingInput> vNereidLoadingInputs)
        {
            LandSurfaces = vNereidLoadingInputs.Select(x => new LandSurface(x)).ToList();
        }
    }

    public class LandSurface
    {
        [JsonProperty("node_id")]
        public string NodeID { get; set; }
        [JsonProperty("surface_key")]
        public string SurfaceKey { get; set; }
        [JsonProperty("area_acres")]
        public double Area { get; set; }
        [JsonProperty("imp_area_acres")]
        public double ImperviousArea { get; set; }

        public LandSurface()
        {

        }

        public LandSurface(vNereidLoadingInput vNereidLoadingInput)
        {
            NodeID = vNereidLoadingInput.PrimaryKey.ToString();
            SurfaceKey =
                $"{vNereidLoadingInput.LSPCBasinKey}-{vNereidLoadingInput.HRUCharacteristicLandUseCodeName}-{vNereidLoadingInput.HydrologicSoilGroup}-{vNereidLoadingInput.SlopePercentage}";
            Area = vNereidLoadingInput.Area;
            ImperviousArea = vNereidLoadingInput.ImperviousAcres;
        }
    }

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

    public class NereidSolutionSequenceRequestObject
    {
        [JsonProperty("directed")]
        public bool Directed { get; set; }

        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }

        public NereidSolutionSequenceRequestObject()
        {
            Directed = true;
        }

        public NereidSolutionSequenceRequestObject(Graph graph)
        {
            Edges = graph.Edges;
            Directed = true;
        }
    }

    public class NereidSubgraphRequestObject
    {
        [JsonProperty("graph")]
        public Graph Graph { get; set; }
        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; }

        public NereidSubgraphRequestObject()
        {
        }

        public NereidSubgraphRequestObject(Graph graph, List<Node> nodes)
        {
            Graph = graph;
            Nodes = nodes;
        }
    }

    public class Edge
    {
        [JsonProperty("source")]
        public string SourceID { get; set; }
        [JsonProperty("target")]
        public string TargetID { get; set; }

        public Edge(string sourceID, string targetID)
        {
            SourceID = sourceID;
            TargetID = targetID;
        }

        public Edge()
        {

        }
    }

    public class Node
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        public Node(string id)
        {
            ID = id;
        }

        public Node()
        {
        }
    }

    public class NereidResult<T>
    {
        [JsonProperty("task_id")]
        public string TaskID { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NereidJobStatus Status { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("result_route")]
        public string ResultRoute { get; set; }
    }

    public class SubgraphResult
    {
        [JsonProperty("subgraph_nodes")]
        // these will actually just be returned as lists of Nodes, but that's fine--
        // --we can attach the appropriate edges later as needed
        public List<Graph> SubgraphNodes { get; set; }

    }

    public class NetworkValidatorResult
    {
        [JsonProperty("isvalid")]
        public string IsValid { get; set; }
        [JsonProperty("node_cycles")]
        public List<List<string>> NodeCycles { get; set; }
        [JsonProperty("edge_cycles")]
        public List<List<string>> EdgeCycles { get; set; }
        [JsonProperty("multiple_out_edges")]
        public List<List<string>> MultipleOutEdges { get; set; }
        [JsonProperty("duplicate_edges")]
        public List<List<string>> DuplicateEdges { get; set; }
    }

    public enum NereidJobStatus
    {
        STARTED,
        SUCCESS,
        valid
    }
}