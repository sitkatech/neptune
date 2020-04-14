using Hangfire;
using Neptune.Web.Areas.Modeling.Models.Nereid;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;
using Edge = Neptune.Web.Areas.Modeling.Models.Nereid.Edge;
using Node = Neptune.Web.Areas.Modeling.Models.Nereid.Node;

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
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = stopwatch.Elapsed;
            stopwatch.Stop();

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

        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult SolutionSequence()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/solution_sequence";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = stopwatch.Elapsed;
            stopwatch.Stop();

            var solutionSequenceRequestObject = new NereidSolutionSequenceRequestObject(graph);

            var subgraphCallStartTime = DateTime.Now;
            var responseObject =
                RunJobAtNereid<NereidSolutionSequenceRequestObject, SolutionSequenceResult>(solutionSequenceRequestObject,
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
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var buildLoadingInputStartTime = stopwatch.Elapsed;
            var vNereidLoadingInputs = HttpRequestStorage.DatabaseEntities.vNereidLoadingInputs.Where(x => regionalSubbasinsForTest.Contains(x.RegionalSubbasinID)).ToList();
            var landSurfaceLoadingRequest = new LandSurfaceLoadingRequest(vNereidLoadingInputs);
            var buildLoadingInputEndTime = stopwatch.Elapsed;
            stopwatch.Stop();

            var responseObject = RunJobAtNereid<LandSurfaceLoadingRequest, object>(landSurfaceLoadingRequest, landSurfaceLoadingUrl, out var responseContent);
            
            var returnValue = new
            {
                SubgraphResult = responseContent,
                SubgraphCallElapsedTime = (buildLoadingInputEndTime - buildLoadingInputStartTime).Milliseconds,
            };

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult TreatmentSiteTable()
        {
            var waterQualityManagementPlanNodes = NereidUtilities.GetWaterQualityManagementPlanNodes(HttpRequestStorage.DatabaseEntities);

            var treatmentSites = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans
                .SelectMany(x => x.QuickBMPs).Join(
                    waterQualityManagementPlanNodes, x => x.WaterQualityManagementPlanID,
                    x => x.WaterQualityManagementPlanID, (bmp, node) => new {bmp, node}).ToList().Select(x =>
                    new TreatmentSite
                    {
                        NodeID = NereidUtilities.WaterQualityManagementPlanNodeID(x.node.WaterQualityManagementPlanID,
                            x.node.RegionalSubbasinID),
                        AreaPercentage = x.bmp.PercentOfSiteTreated,
                        CapturedPercentage = x.bmp.PercentCaptured,
                        RetainedPercentage = x.bmp.PercentRetained,
                        FacilityType = x.bmp.TreatmentBMPType.TreatmentBMPModelingType.TreatmentBMPModelingTypeName
                    });

            return Json(new {TreatmentSites = treatmentSites}, JsonRequestBehavior.AllowGet);
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
