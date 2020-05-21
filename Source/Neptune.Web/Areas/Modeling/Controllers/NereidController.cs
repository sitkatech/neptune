using Hangfire;
using Neptune.Web.Areas.Modeling.Models.Nereid;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Node = Neptune.Web.Areas.Modeling.Models.Nereid.Node;
using SolutionResponseObject = Neptune.Web.Areas.Modeling.Models.Nereid.SolutionResponseObject;

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

            var validateCallStartTime = stopwatch.Elapsed;
            var networkValidatorResult = NereidUtilities.RunJobAtNereid<Graph, NetworkValidatorResult>(graph, networkValidatorUrl, out _, HttpClient);

            var validateCallEndTime = stopwatch.Elapsed;

            stopwatch.Stop();

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
            var subgraphUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/subgraph";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = stopwatch.Elapsed;

            var subgraphRequestObject = new NereidSubgraphRequestObject(graph, new List<Node> { new Node("BMP_39") });
            var subgraphCallStartTime = stopwatch.Elapsed;

            var subgraphResult = NereidUtilities.RunJobAtNereid<NereidSubgraphRequestObject, SubgraphResult>(subgraphRequestObject,
                subgraphUrl, out _, HttpClient);
            var subgraphCallEndTime = stopwatch.Elapsed;

            stopwatch.Stop();

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
            var solutionSequenceUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/solution_sequence?min_branch_size=12";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);
            var buildGraphEndTime = stopwatch.Elapsed;

            var solutionSequenceRequestObject = new SolutionSequenceRequest(graph);

            var subgraphCallStartTime = stopwatch.Elapsed;
            var solutionSequenceResult = NereidUtilities.RunJobAtNereid<SolutionSequenceRequest, SolutionSequenceResult>(solutionSequenceRequestObject,
                    solutionSequenceUrl, out var responseContent, HttpClient);
            var subgraphCallEndTime = stopwatch.Elapsed;

            stopwatch.Stop();
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
                .SelectMany(x => x.QuickBMPs.Where(y => y.TreatmentBMPType.IsAnalyzedInModelingModule)).Join(
                    waterQualityManagementPlanNodes, x => x.WaterQualityManagementPlanID,
                    x => x.WaterQualityManagementPlanID, (bmp, node) => new { bmp, node }).ToList();

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

            return Json(new { TreatmentSites = treatmentSites }, JsonRequestBehavior.AllowGet);
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

            var treatmentFacilities = NereidUtilities.ModelingTreatmentBMPs(HttpRequestStorage.DatabaseEntities)
                .ToList().Where(x => x.IsFullyParameterized())
                .Select(x => x.ToTreatmentFacility()).ToList();

            var treatmentFacilityTable = new TreatmentFacilityTable() { TreatmentFacilities = treatmentFacilities };

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
        /// Runs a test case against the Nereid treatment_facility/validate endpoint.
        /// Specifically tests that "NoTreatment" BMPs are being handled correctly by Nereid validator. I.e., they should pass validation automatically
        /// Confirms that we are building one of the four inputs to the Nereid watershed/solve endpoint correctly.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult NoTreatmentFacility()
        {
            var treatmentFacilityUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/treatment_facility/validate?state=ca&region=soc";

            var treatmentFacilities = HttpRequestStorage.DatabaseEntities.TreatmentBMPs
                .Where(x => x.TreatmentBMPID == 9974).ToList().Select(x => x.ToTreatmentFacility()).ToList();

            var treatmentFacilityTable = new TreatmentFacilityTable() { TreatmentFacilities = treatmentFacilities };
            bool failed = false;
            string responseContent = null;
            try
            {
                NereidUtilities.RunJobAtNereid<TreatmentFacilityTable, object>(treatmentFacilityTable,
                    treatmentFacilityUrl,
                    out responseContent, HttpClient);
            }
            catch (Exception)
            {
                failed = true;
            }

            return Json(
                new
                {
                    firstCallFailed = failed,
                    responseContent,
                    requestContent = JsonConvert.SerializeObject(treatmentFacilityTable)
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SitkaAdminFeature]
        public JsonResult GetTreatmentBMPResult(int treatmentBMPID)
        {
            var fullResponse = HttpRequestStorage.DatabaseEntities.NereidResults.SingleOrDefault(x=>x.TreatmentBMPID == treatmentBMPID)?.FullResponse;
            var jObject = JObject.Parse(fullResponse);
            var keyValue = jObject.ToKeyValue();
            return Json(keyValue, JsonRequestBehavior.AllowGet);
            //return Json(jObject, JsonRequestBehavior.AllowGet);
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);

            // this subgraph is 23 nodes deep
            var single = graph.Nodes.Single(x => x.ID == "RSB_42");
            var subgraph = graph.GetUpstreamSubgraph(single);

            var allLoadingInputs = HttpRequestStorage.DatabaseEntities.vNereidLoadingInputs.ToList();
            var allModelingBMPs = NereidUtilities.ModelingTreatmentBMPs(HttpRequestStorage.DatabaseEntities).ToList();
            var allWaterqualityManagementPlanNodes = NereidUtilities.GetWaterQualityManagementPlanNodes(HttpRequestStorage.DatabaseEntities).ToList();
            var allModelingQuickBMPs = HttpRequestStorage.DatabaseEntities.QuickBMPs.Include(x => x.TreatmentBMPType)
                .Where(x => x.PercentOfSiteTreated != null && x.TreatmentBMPType.IsAnalyzedInModelingModule).ToList();

            var responseContent = SolveSubgraph(subgraph, allLoadingInputs, allModelingBMPs, allWaterqualityManagementPlanNodes, allModelingQuickBMPs, out _);

            var stopwatchElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();

            return Json(new { elapsed = stopwatchElapsedMilliseconds, responseContent }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Runs Nereid on the entire SOC network graph using the solution sequence pattern.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SitkaAdminFeature]
        public ActionResult SolveSOC()
        {
            var solutionSequenceUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/solution_sequence?min_branch_size=12";

            var failed = false;
            var exceptionMessage = "";
            var stackTrace = "";
            var missingNodeIDs = new List<string>();

            var stopwatch = new Stopwatch();
            stopwatch.Start();


            var graph = NereidUtilities.BuildNetworkGraph(HttpRequestStorage.DatabaseEntities);

            var allLoadingInputs = HttpRequestStorage.DatabaseEntities.vNereidLoadingInputs.ToList();
            var allModelingBMPs = NereidUtilities.ModelingTreatmentBMPs(HttpRequestStorage.DatabaseEntities).ToList();
            var allwaterQualityManagementPlanNodes = NereidUtilities.GetWaterQualityManagementPlanNodes(HttpRequestStorage.DatabaseEntities).ToList();
            var allModelingQuickBMPs = HttpRequestStorage.DatabaseEntities.QuickBMPs.Include(x => x.TreatmentBMPType)
                .Where(x => x.PercentOfSiteTreated != null && x.TreatmentBMPType.IsAnalyzedInModelingModule).ToList();
            var prepareInputsFromDatabaseElapsedTime = stopwatch.ElapsedMilliseconds;

            var solutionSequenceResult = NereidUtilities.RunJobAtNereid<SolutionSequenceRequest, SolutionSequenceResult>(new SolutionSequenceRequest(graph), solutionSequenceUrl, out _, HttpClient);
            var prepareSolutionSequenceElapsedTime = stopwatch.ElapsedMilliseconds;

            try
            {
                foreach (var parallel in solutionSequenceResult.Data.SolutionSequence.Parallel)
                {
                    foreach (var series in parallel.Series)
                    {
                        var subgraphNodeIDs = series.Nodes.Select(x => x.ID).ToList();

                        // create the subgraph that has these nodes as its nodes and the appropriate edges
                        // appropriate edges = where target in nodes?
                        var subgraphNodes = graph.Nodes.Where(x => subgraphNodeIDs.Contains(x.ID)).ToList();
                        var subgraphEdges = graph.Edges.Where(x =>
                            subgraphNodeIDs.Contains(x.TargetID) && subgraphNodeIDs.Contains(x.SourceID)).ToList();

                        var subgraph = new Graph(true, subgraphNodes, subgraphEdges);

                        SolveSubgraph(subgraph, allLoadingInputs, allModelingBMPs, allwaterQualityManagementPlanNodes,
                            allModelingQuickBMPs, out var notFoundNodes);
                        missingNodeIDs.AddRange(notFoundNodes);
                    }
                }
            }
            catch (NereidException<SolutionRequestObject, SolutionResponseObject> nexc)
            {
                var elapsed = stopwatch.ElapsedMilliseconds;
                var data = new
                {
                    prepareInputsFromDatabaseElapsedTime,
                    prepareSolutionSequenceElapsedTime =
                        prepareSolutionSequenceElapsedTime - prepareInputsFromDatabaseElapsedTime,
                    solveElapsedTime = elapsed - prepareSolutionSequenceElapsedTime,
                    totalElapsedTime = elapsed,
                    nodesProcessed = graph.Nodes.Count(x => x.Results != null),
                    missingNodeIDs,
                    failed = true,
                    exceptionMessage = nexc.Message,
                    innerExceptionStackTrace = nexc.InnerException?.StackTrace,
                    failingRequest = nexc.Request,
                    failureResponse = nexc.Response
                };

                return Content(JsonConvert.SerializeObject(data));
            }
            catch (Exception exception)
            {
                failed = true;
                exceptionMessage = exception.Message;
                stackTrace = exception.StackTrace;
            }

            var totalElapsedTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();

            var nereidResults = graph.Nodes.Where(x=>x.Results!= null).Select(x => new NereidResult(x.Results.ToString())
            {
                TreatmentBMPID = x.TreatmentBMPID, DelineationID = x.Delineation?.DelineationID,
                NodeID = x.ID,
                RegionalSubbasinID = x.RegionalSubbasin?.RegionalSubbasinID,
                WaterQualityManagementPlanID = x.WaterQualityManagementPlan?.WaterQualityManagementPlanID
            });

            HttpRequestStorage.DatabaseEntities.NereidResults.AddRange(nereidResults);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return Json(
                new
                {
                    prepareInputsFromDatabaseElapsedTime,
                    prepareSolutionSequenceElapsedTime =
                        prepareSolutionSequenceElapsedTime - prepareInputsFromDatabaseElapsedTime,
                    solveElapsedTime = totalElapsedTime - prepareSolutionSequenceElapsedTime,
                    totalElapsedTime,
                    nodesProcessed = graph.Nodes.Count(x => x.Results != null),
                    missingNodeIDs,
                    failed,
                    exceptionMessage,
                    stackTrace
                }, JsonRequestBehavior.AllowGet);
        }

        private static NereidResult<SolutionResponseObject> SolveSubgraph(Graph subgraph, List<vNereidLoadingInput> allLoadingInputs, List<TreatmentBMP> allModelingBMPs, List<WaterQualityManagementPlanNode> allWaterqualityManagementPlanNodes, List<QuickBMP> allModelingQuickBMPs, out List<string> notFoundNodes)
        {
            notFoundNodes = new List<string>();

            // Now I need to get the land_surface, treatment_facility, and treatment_site tables for this request.
            // these are going to look very much like the various calls made throughout the testing methods, but filtered
            // to the subgraph. Fortunately, we've added metadata to the nodes to help us do the filtration

            var delineationToIncludeIDs = subgraph.Nodes.Where(x => x.Delineation != null).Select(x => x.Delineation.DelineationID)
                .Distinct().ToList();
            var regionalSubbasinToIncludeIDs = subgraph.Nodes.Where(x => x.RegionalSubbasin != null)
                .Select(x => x.RegionalSubbasin.RegionalSubbasinID).Distinct().ToList();
            var waterQualityManagementPlanToIncludeIDs = subgraph.Nodes.Where(x => x.WaterQualityManagementPlan != null)
                .Select(x => x.WaterQualityManagementPlan.WaterQualityManagementPlanID).Distinct().ToList();
            var treatmentBMPToIncludeIDs = subgraph.Nodes.Where(x => x.TreatmentBMPID != null)
                .Select(x => x.TreatmentBMPID.Value).Distinct().ToList();

            var landSurfaces = allLoadingInputs.Where(x =>
                delineationToIncludeIDs.Contains(x.DelineationID.GetValueOrDefault()) ||
                regionalSubbasinToIncludeIDs.Contains(x.RegionalSubbasinID) ||
                waterQualityManagementPlanToIncludeIDs.Contains(x.WaterQualityManagementPlanID.GetValueOrDefault())
            ).ToList().Select(x => new LandSurface(x)).ToList();

            var treatmentFacilities = allModelingBMPs
                .Where(x => treatmentBMPToIncludeIDs.Contains(x.TreatmentBMPID))
                .Select(x => x.ToTreatmentFacility()).ToList();

            var filteredQuickBMPs = allModelingQuickBMPs
                .Where(x => waterQualityManagementPlanToIncludeIDs.Contains(x.WaterQualityManagementPlanID)).ToList();
            var filteredWQMPNodes = allWaterqualityManagementPlanNodes.Where(y =>
                waterQualityManagementPlanToIncludeIDs.Contains(y.WaterQualityManagementPlanID) &&
                regionalSubbasinToIncludeIDs.Contains(y.RegionalSubbasinID) // ignore parts that live in RSBs outside our solve area.
                ).ToList();

            var treatmentSites = filteredQuickBMPs
                .Join(
                    filteredWQMPNodes, x => x.WaterQualityManagementPlanID,
                    x => x.WaterQualityManagementPlanID, (bmp, node) => new { bmp, node })
                .Select(x =>
                new TreatmentSite
                {
                    NodeID = NereidUtilities.WaterQualityManagementPlanNodeID(x.node.WaterQualityManagementPlanID,
                        x.node.RegionalSubbasinID),
                    AreaPercentage = x.bmp.PercentOfSiteTreated,
                    CapturedPercentage = x.bmp.PercentCaptured ?? 0,
                    RetainedPercentage = x.bmp.PercentRetained ?? 0,
                    FacilityType = x.bmp.TreatmentBMPType.TreatmentBMPModelingType.TreatmentBMPModelingTypeName
                }).ToList();

            //ValidateForTesting(subgraph, landSurfaces, treatmentFacilities, treatmentSites);

            var solveUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/watershed/solve?state=ca&region=soc";

            var solutionRequestObject = new SolutionRequestObject()
            {
                Graph = subgraph,
                LandSurfaces = landSurfaces,
                TreatmentFacilities = treatmentFacilities,
                TreatmentSites = treatmentSites,
                PreviousResults = subgraph.Nodes.Where(x => x.Results != null).Select(x => x.Results).ToList()
            };
            NereidResult<SolutionResponseObject> results = null;
            try
            {
                results = NereidUtilities.RunJobAtNereid<SolutionRequestObject, SolutionResponseObject>(
                    solutionRequestObject, solveUrl,
                    out var responseContent, HttpClient);
            }
            catch (Exception e)
            {
                throw new NereidException<SolutionRequestObject, SolutionResponseObject>(e.Message, e)
                {
                    Request = solutionRequestObject,
                    Response = results?.Data
                };
            }

            if (results?.Data.Errors != null && results.Data.Errors.Count > 0)
            {
                throw new NereidException<SolutionRequestObject, SolutionResponseObject>
                { Request = solutionRequestObject, Response = results.Data };
            }

            foreach (var dataLeafResult in results.Data.Results)
            {
                var node = subgraph.Nodes.SingleOrDefault(x => x.ID == dataLeafResult["node_id"].ToString());
                if (node == null)
                {
                    //throw new NereidException<SolutionRequestObject, SolutionResponseObject>
                    //    ($"Found Node ID {dataLeafResult["node_id"]} in response... Does not exist on graph...")
                    //    { Request = solutionRequestObject, Response = results.Data };

                    // this is an edge case that should only happen if an RSB in the SOC area has
                    // its downstream catchment outside the SOC area for some reason.
                    // if that truly is the only time this happens, then I'm not worried about it.
                    // But, if it happens under other circumstances, I'm very worried about it.
                    notFoundNodes.Add(dataLeafResult["node_id"].ToString());
                }
                else
                {
                    node.Results = dataLeafResult;
                }
            }

            // don't need to store the leaf results. They're either data we already have, or just loading summaries
            foreach (var dataLeafResult in results.Data.LeafResults)
            {
                var node = subgraph.Nodes.SingleOrDefault(x => x.ID == dataLeafResult["node_id"].ToString());
                if (node == null)
                {
                    notFoundNodes.Add(dataLeafResult["node_id"].ToString());
                }
                else
                {
                    node.Results = dataLeafResult;
                }
            }

            return results;
        }

        private static void ValidateForTesting(Graph subgraph, List<LandSurface> landSurfaces, List<TreatmentFacility> treatmentFacilities, List<TreatmentSite> treatmentSites)
        {
            // validate input objects -- not strictly necessary, just for testing purposes
            var landSurfaceLoadingUrl =
                $"{NeptuneWebConfiguration.NereidUrl}/api/v1/land_surface/loading?details=true&state=ca&region=soc";
            var treatmentFacilityUrl =
                $"{NeptuneWebConfiguration.NereidUrl}/api/v1/treatment_facility/validate?state=ca&region=soc";
            var treatmentSiteUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/treatment_site/validate?state=ca&region=soc";
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/validate";

            var landSurfaceLoadingRequest = new LandSurfaceLoadingRequest { LandSurfaces = landSurfaces };
            var landSurfaceResponseObject =
                NereidUtilities.RunJobAtNereid<LandSurfaceLoadingRequest, GenericNeriedResponse>(
                    landSurfaceLoadingRequest, landSurfaceLoadingUrl, out var loadingResponse, HttpClient);

            var treatmentFacilityTable = new TreatmentFacilityTable { TreatmentFacilities = treatmentFacilities };
            var treatmentFacilityResponseObject =
                NereidUtilities.RunJobAtNereid<TreatmentFacilityTable, GenericNeriedResponse>(treatmentFacilityTable,
                    treatmentFacilityUrl, out var treatmentFacilityResponse, HttpClient);

            var networkValidatorResult = NereidUtilities.RunJobAtNereid<Graph, NetworkValidatorResult>(subgraph,
                networkValidatorUrl, out var networkValidatorResponse, HttpClient);

            var treatmentSiteTable = new TreatmentSiteTable { treatment_sites = treatmentSites };
            var treatmentSiteResponseObject =
                NereidUtilities.RunJobAtNereid<TreatmentSiteTable, GenericNeriedResponse>(treatmentSiteTable,
                    treatmentSiteUrl, out var treatmentSiteResponse, HttpClient);
        }
    }

    internal class NereidException<TReq, TResp> : Exception
    {
        public NereidException()
        { }

        public NereidException(string s) : base(s)
        {
        }

        public NereidException(string s, Exception exception) : base(s, exception)
        {
        }

        public TReq Request { get; set; }
        public TResp Response { get; set; }
    }

    public class TreatmentSiteTable
    {
        public List<TreatmentSite> treatment_sites { get; set; }
    }
}
