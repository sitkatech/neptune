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

        /// <summary>
        /// Manually fire a re-calculation of the LGU layer.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult TriggerLGURun()
        {
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunLoadGeneratingUnitRefreshJob(null));
            return Content("LGU refresh will run in the background");
        }

        /// <summary>
        /// Manually fire a run of the HRU statistics job.
        /// This does not recalculate the LGU layer or discard existing HRU statistics.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult TriggerHRURun()
        {
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunHRURefreshJob());
            return Content("HRU refresh will run in the background");
        }

        /// <summary>
        /// Build the entire Network Graph and send it to the Nereid network/validate endpoint.
        /// Can be used to diagnose issues in the network data, the network builder, or the Nereid validator.
        /// Confirms that we are building one of the four inputs to the watershed/solve endpoint correctly.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult ValidateNetworkGraph()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/validate";
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = stopwatch.Elapsed;
            stopwatch.Stop();

            var validateCallStartTime = DateTime.Now;
            var networkValidatorResult = NereidUtilities.RunJobAtNereid<Graph, NetworkValidatorResult>(graph, networkValidatorUrl, out _, HttpClient);

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

        /// <summary>
        /// Build the network graph and run a test case against the Nereid network/subgraph endpoint.
        /// This confirms that we can retrieve the minimal subgraph needed to solve any given node.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
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

            var subgraphResult = NereidUtilities.RunJobAtNereid<NereidSubgraphRequestObject, SubgraphResult>(subgraphRequestObject,
                networkValidatorUrl, out _, HttpClient);
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

        /// <summary>
        /// Builds the network graph and runs a test case against the Nereid network/solution_sequence endpoint.
        /// This confirms that the infrastructure for subdividing large solution jobs is working.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
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

            var subgraphCallStartTime = stopwatch.Elapsed;
            var solutionSequenceResult = NereidUtilities.RunJobAtNereid<NereidSolutionSequenceRequestObject, SolutionSequenceResult>(solutionSequenceRequestObject,
                    networkValidatorUrl, out _, HttpClient);
            var subgraphCallEndTime = stopwatch.Elapsed;
            
            var returnValue = new
            {
                SubgraphResult = solutionSequenceResult.Data,
                BuildGraphElapsedTime = (buildGraphEndTime - buildGraphStartTime).Milliseconds,
                SubgraphCallElapsedTime = (subgraphCallEndTime - subgraphCallStartTime).Milliseconds,
                NodeCount = graph.Nodes.Count,
                EdgeCount = graph.Edges.Count,
            };

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Runs a test case against the Nereid land_surface/loading endpoint for a small list of RSBs.
        /// Confirms that we are building one of the four inputs to the watershed/solve endpoint correctly.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
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

            var unused = NereidUtilities.RunJobAtNereid<LandSurfaceLoadingRequest, object>(landSurfaceLoadingRequest, landSurfaceLoadingUrl, out var responseContent, HttpClient);
            
            var returnValue = new
            {
                SubgraphResult = responseContent,
                SubgraphCallElapsedTime = (buildLoadingInputEndTime - buildLoadingInputStartTime).Milliseconds,
            };

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Builds and displays the treatment_site table, one of the four inputs to the Nereid watershed/solve endpoint.
        /// Thre is no validator for this; the only test fixture available is to manually confirm the schema and the data.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult TreatmentSiteTable()
        {
            var waterQualityManagementPlanNodes = NereidUtilities.GetWaterQualityManagementPlanNodes(HttpRequestStorage.DatabaseEntities);

            var list = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans
                .SelectMany(x => x.QuickBMPs.Where(y=>y.TreatmentBMPType.IsAnalyzedInModelingModule)).Join(
                    waterQualityManagementPlanNodes, x => x.WaterQualityManagementPlanID,
                    x => x.WaterQualityManagementPlanID, (bmp, node) => new {bmp, node}).ToList();

            var treatmentSites = list.Select(x =>
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

        /// <summary>
        /// Runs a test case against the Nereid treatment_facility/validate endpoint.
        /// Confirms that we are building one of the four inputs to the Nereid watershed/solve endpoint correctly.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult TreatmentFacility()
        {
            var treatmentFacilityUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/treatment_facility/validate?state=ca&region=soc";

            var treatmentFacilities = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ModeledTreatmentBMPs()
                .Select(x => x.ToTreatmentFacility()).ToList();

            var treatmentFacilityTable = new TreatmentFacilityTable() { TreatmentFacilities = treatmentFacilities};

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            NereidUtilities.RunJobAtNereid<TreatmentFacilityTable, object>(treatmentFacilityTable, treatmentFacilityUrl,
                out var responseContent, HttpClient);
            var stopwatchElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            return Json(
                new
                {
                    rpcTime = stopwatchElapsedMilliseconds,
                    responseContent,
                    requestContent = JsonConvert.SerializeObject(treatmentFacilityTable)
                }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Runs a very small test case against the Nereid watershed/solve endpoint.
        /// Builds the complete network graph, then hits the subgraph endpoint with
        /// a node that we know has a small upstream. Uses the return value to construct
        /// the input and then fires that against the solution endpoint.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult SolutionTestCase()
        {
            throw new NotImplementedException();
        }
    }
}
