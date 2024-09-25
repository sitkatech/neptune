using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.EFModels.Nereid;
using Neptune.Jobs.Hangfire;
using Neptune.Jobs.Services;

namespace Neptune.API.Controllers
{
    public class NereidController : SitkaController<NereidController>
    {
        private readonly NereidService _nereidService;

        public NereidController(NeptuneDbContext dbContext,
            ILogger<NereidController> logger,
            KeystoneService keystoneService,
            IOptions<NeptuneConfiguration> neptuneConfiguration,
            NereidService nereidService) : base(dbContext, logger, keystoneService, neptuneConfiguration)
        {
            _nereidService = nereidService;
        }

        [HttpGet]
        [SitkaAdminFeature]
        public IActionResult DeltaSolve()
        {
            BackgroundJob.Enqueue<DeltaSolveJob>(x => x.RunJob());
            return Ok("En-queued");
        }

        [HttpGet("nereid/health")]
        public async Task<IActionResult> HealthCheck()
        {
            var healthCheck = await _nereidService.HealthCheck();
            return Ok(healthCheck);
        }

        [HttpGet("nereid/config")]
        public async Task<IActionResult> ConfigCheck()
        {
            var configCheck = await _nereidService.ConfigCheck();
            return Ok(configCheck);
        }

        /// <summary>
        /// Build the entire Network Graph and send it to the Nereid network/validate endpoint.
        /// Can be used to diagnose issues in the network data, the network builder, or the Nereid validator.
        /// Confirms that we are building one of the four inputs to the watershed/solve endpoint correctly.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/validate")]
        [SitkaAdminFeature]
        public async Task<IActionResult> ValidateNetworkGraph()
        {
            var networkValidatorUrl = "api/v1/network/validate";
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = _nereidService.BuildTotalNetworkGraph(_dbContext);
            var buildGraphEndTime = stopwatch.Elapsed;

            var validateCallStartTime = stopwatch.Elapsed;
            var networkValidatorResult = await _nereidService.RunJobAtNereid<Graph, NetworkValidatorResult>(graph, networkValidatorUrl, graph.Nodes, null);

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

            return Ok(returnValue);
        }

        /// <summary>
        /// Build the network graph and run a test case against the Nereid network/subgraph endpoint.
        /// This confirms that we can retrieve the minimal subgraph needed to solve any given node.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/subgraph")]
        [SitkaAdminFeature]
        public async Task<IActionResult> Subgraph()
        {
            var subgraphUrl = "api/v1/network/subgraph";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = _nereidService.BuildTotalNetworkGraph(_dbContext);
            var buildGraphEndTime = stopwatch.Elapsed;

            var subgraphRequestObject = new NereidSubgraphRequestObject(graph, new List<Node> { new Node("BMP_39") });
            var subgraphCallStartTime = stopwatch.Elapsed;

            var subgraphResult = await _nereidService.RunJobAtNereid<NereidSubgraphRequestObject, SubgraphResult>(subgraphRequestObject,
                subgraphUrl, subgraphRequestObject.Nodes, null);
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

            return Ok(returnValue);
        }

        /// <summary>
        /// Builds the network graph and runs a test case against the Nereid network/solution_sequence endpoint.
        /// This confirms that the infrastructure for subdividing large solution jobs is working.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/solution-sequence")]
        [SitkaAdminFeature]
        public async Task<IActionResult> SolutionSequence()
        {
            var solutionSequenceUrl = "api/v1/network/solution_sequence?min_branch_size=12";
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var buildGraphStartTime = stopwatch.Elapsed;
            var graph = _nereidService.BuildTotalNetworkGraph(_dbContext);
            var buildGraphEndTime = stopwatch.Elapsed;

            var solutionSequenceRequestObject = new SolutionSequenceRequest(graph);

            var subgraphCallStartTime = stopwatch.Elapsed;
            var solutionSequenceResult = await _nereidService.RunJobAtNereid<SolutionSequenceRequest, SolutionSequenceResult>(solutionSequenceRequestObject, solutionSequenceUrl, graph.Nodes, null);
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

            return Ok(returnValue);
        }

        /// <summary>
        /// Runs a test case against the Nereid land_surface/loading endpoint for a small list of RSBs.
        /// Confirms that we are building one of the four inputs to the watershed/solve endpoint correctly.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/land-surface-loading")]
        [SitkaAdminFeature]
        public async Task<IActionResult> Loading()
        {
            var landSurfaceLoadingUrl = "api/v1/land_surface/loading?details=true&state=ca&region=oc";
            var regionalSubbasinsForTest = new List<int> { 2377,12394 };
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var buildLoadingInputStartTime = stopwatch.Elapsed;
            var vNereidLoadingInputs = _dbContext.vNereidLoadingInputs.Where(x => regionalSubbasinsForTest.Contains(x.RegionalSubbasinID)).ToList();
            var landSurfaceLoadingRequest = new LandSurfaceLoadingRequest(vNereidLoadingInputs, false);
            var buildLoadingInputEndTime = stopwatch.Elapsed;
            stopwatch.Stop();

            var responseObject = await _nereidService.RunJobAtNereid<LandSurfaceLoadingRequest, object>(landSurfaceLoadingRequest, landSurfaceLoadingUrl, new List<Node>(), null);

            var returnValue = new
            {
                SubgraphResult = responseObject,
                SubgraphCallElapsedTime = (buildLoadingInputEndTime - buildLoadingInputStartTime).Milliseconds,
            };

            return Ok(returnValue);
        }

        /// <summary>
        /// Runs a test case against the Nereid land_surface/loading endpoint for a small list of RSBs.
        /// Confirms that we are building one of the four inputs to the watershed/solve endpoint correctly.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/land-surface-loading-baseline")]
        [SitkaAdminFeature]
        public async Task<IActionResult> BaselineLoading()
        {
            var landSurfaceLoadingUrl = "api/v1/land_surface/loading?details=true&state=ca&region=oc";
            var regionalSubbasinsForTest = new List<int> { 2377,12394 };
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var buildLoadingInputStartTime = stopwatch.Elapsed;
            var vNereidLoadingInputs = _dbContext.vNereidLoadingInputs.Where(x => regionalSubbasinsForTest.Contains(x.RegionalSubbasinID)).ToList();
            var landSurfaceLoadingRequest = new LandSurfaceLoadingRequest(vNereidLoadingInputs, true);
            var buildLoadingInputEndTime = stopwatch.Elapsed;
            stopwatch.Stop();

            var responseObject = await _nereidService.RunJobAtNereid<LandSurfaceLoadingRequest, object>(landSurfaceLoadingRequest, landSurfaceLoadingUrl, new List<Node>(), null);

            var returnValue = new
            {
                LoadingRequest = landSurfaceLoadingRequest,
                LoadingResult = responseObject,
                SubgraphCallElapsedTime = (buildLoadingInputEndTime - buildLoadingInputStartTime).Milliseconds,
            };

            return Ok(returnValue);
        }

        /// <summary>
        /// Runs a test case against the Nereid treatment_facility/validate endpoint.
        /// Confirms that we are building one of the four inputs to the Nereid watershed/solve endpoint correctly.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/treatment-facility-validate")]
        [SitkaAdminFeature]
        public async Task<IActionResult> TreatmentFacility()
        {
            var modelingTreatmentBMPs = TreatmentBMPs.ListModelingTreatmentBMPs(_dbContext);
            var delineations = vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext);

            var modelBasins = _dbContext.ModelBasins.AsNoTracking().ToDictionary(x => x.ModelBasinID, x => x.ModelBasinKey);
            var precipitationZones = _dbContext.PrecipitationZones.AsNoTracking().ToDictionary(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);

            var treatmentFacilities = modelingTreatmentBMPs
                .Where(x => x.IsFullyParameterized(delineations[x.TreatmentBMPID]))
                .Select(x => x.ToTreatmentFacility(delineations, false, modelBasins, precipitationZones)).ToList();

            var treatmentFacilityTable = new TreatmentFacilityTable() { TreatmentFacilities = treatmentFacilities };

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var treatmentFacilityUrl = "api/v1/treatment_facility/validate?state=ca&region=oc";
            var responseObject = await _nereidService.RunJobAtNereid<TreatmentFacilityTable, object>(treatmentFacilityTable, treatmentFacilityUrl, new List<Node>(), null);
            var stopwatchElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            return Ok(new
            {
                ElapsedTime = stopwatchElapsedMilliseconds,
                ResponseObject = responseObject,
                RequestContent = treatmentFacilityTable
            });
        }

        /// <summary>
        /// Runs a test case against the Nereid treatment_facility/validate endpoint.
        /// Confirms that we are building one of the four inputs to the Nereid watershed/solve endpoint correctly.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/treatment-facility-validate/{treatmentBMPID}")]
        [SitkaAdminFeature]
        public async Task<IActionResult> ValidateTreatmentFacility([FromRoute] int treatmentBMPID)
        {
            const string treatmentFacilityUrl = "api/v1/treatment_facility/validate?state=ca&region=oc";
            var modelBasins = _dbContext.ModelBasins.AsNoTracking().ToDictionary(x => x.ModelBasinID, x => x.ModelBasinKey);
            var precipitationZones = _dbContext.PrecipitationZones.AsNoTracking().ToDictionary(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);
            var delineations = vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext);

            var treatmentFacility = TreatmentBMPs.GetByID(_dbContext, treatmentBMPID).ToTreatmentFacility(delineations, true, modelBasins, precipitationZones);

            var treatmentFacilityTable = new TreatmentFacilityTable() { TreatmentFacilities = new List<TreatmentFacility> {treatmentFacility} };

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var responseObject = await _nereidService.RunJobAtNereid<TreatmentFacilityTable, object>(treatmentFacilityTable, treatmentFacilityUrl, new List<Node>(), null);
            var stopwatchElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            return Ok(
                new
                {
                    ElapsedTime = stopwatchElapsedMilliseconds,
                    ResponseObject = responseObject,
                    RequestContent = treatmentFacilityTable
                });
        }

        /// <summary>
        /// Runs a test case against the Nereid treatment_facility/validate endpoint.
        /// Specifically tests that "NoTreatment" BMPs are being handled correctly by Nereid validator. I.e., they should pass validation automatically
        /// Confirms that we are building one of the four inputs to the Nereid watershed/solve endpoint correctly.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/no-treatment-facility-validate")]
        [SitkaAdminFeature]
        public async Task<IActionResult> NoTreatmentFacility()
        {
            const string treatmentFacilityUrl = "api/v1/treatment_facility/validate?state=ca&region=oc";
            var modelBasins = _dbContext.ModelBasins.AsNoTracking().ToDictionary(x => x.ModelBasinID, x => x.ModelBasinKey);
            var precipitationZones = _dbContext.PrecipitationZones.AsNoTracking().ToDictionary(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);
            var delineations = vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext);
            var treatmentFacilities = _dbContext.TreatmentBMPs
                .Where(x => x.TreatmentBMPID == 9974).ToList().Select(x => x.ToTreatmentFacility(delineations, true, modelBasins, precipitationZones)).ToList();

            var treatmentFacilityTable = new TreatmentFacilityTable() { TreatmentFacilities = treatmentFacilities };
            try
            {
                var responseObject = await _nereidService.RunJobAtNereid<TreatmentFacilityTable, object>(treatmentFacilityTable,
                    treatmentFacilityUrl, new List<Node>(), null);
                return Ok(
                    new
                    {
                        FirstCallFailed = false,
                        ResponseObject = responseObject,
                        RequestContent = treatmentFacilityTable
                    });
            }
            catch (Exception)
            {
                return Ok(
                    new
                    {
                        FirstCallFailed = true,
                        RequestContent = treatmentFacilityTable
                    });
            }
        }


        /// <summary>
        /// Builds and displays the treatment_site table, one of the four inputs to the Nereid watershed/solve endpoint.
        /// There is no validator for this; the only test fixture available is to manually confirm the schema and the data.
        /// Available only to Sitka Admins
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/treatment-sites")]
        [SitkaAdminFeature]
        public IActionResult TreatmentSiteTable()
        {
            var waterQualityManagementPlanNodes = _nereidService.GetWaterQualityManagementPlanNodes(_dbContext);

            var list = _dbContext.QuickBMPs.Include(x => x.TreatmentBMPType).AsNoTracking()
                .Where(y => y.TreatmentBMPType.IsAnalyzedInModelingModule).ToList().Join(
                    waterQualityManagementPlanNodes, x => x.WaterQualityManagementPlanID,
                    x => x.WaterQualityManagementPlanID, (bmp, node) => new { bmp, node }).ToList();

            var treatmentSites = list.Select(x =>
                new TreatmentSite
                {
                    NodeID = NereidUtilities.WaterQualityManagementPlanTreatmentNodeID(x.node.WaterQualityManagementPlanID,
                        x.node.RegionalSubbasinID),
                    AreaPercentage = x.bmp.PercentOfSiteTreated,
                    CapturedPercentage = x.bmp.PercentCaptured,
                    RetainedPercentage = x.bmp.PercentRetained,
                    FacilityType = x.bmp.TreatmentBMPType.TreatmentBMPModelingType.TreatmentBMPModelingTypeName
                }).ToList();

            var treatmentSiteTable = new TreatmentSiteTable(){TreatmentSites = treatmentSites};

            return Ok(treatmentSiteTable);
        }

        [HttpGet("nereid/treatment-facilities")]
        [SitkaAdminFeature]
        public IActionResult TreatmentFacilityTable()
        {
            var modelBasins = _dbContext.ModelBasins.AsNoTracking().ToDictionary(x => x.ModelBasinID, x => x.ModelBasinKey);
            var precipitationZones = _dbContext.PrecipitationZones.AsNoTracking().ToDictionary(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);
            var delineations = vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext);
            var treatmentFacilities = TreatmentBMPs.ListModelingTreatmentBMPs(_dbContext)
                .ToList()
                .Select(x => x.ToTreatmentFacility(delineations, false, modelBasins, precipitationZones)).ToList();

            var treatmentFacilityTable = new TreatmentFacilityTable() { TreatmentFacilities = treatmentFacilities };
            return Ok(treatmentFacilityTable);
        }


        [HttpGet("nereid/land-surface-table")]
        [SitkaAdminFeature]
        public IActionResult LandSurfaceTable()
        {
            var vNereidLoadingInputs = _dbContext.vNereidLoadingInputs.ToList();
            var landSurfaceLoadingRequest = new LandSurfaceLoadingRequest(vNereidLoadingInputs, false);
            return Ok(landSurfaceLoadingRequest);
        }

        [HttpGet("nereid/total-network-graph")]
        [SitkaAdminFeature]
        public IActionResult NetworkTable()
        {
            var graph = _nereidService.BuildTotalNetworkGraph(_dbContext);
            return Ok(graph);
        }

        /// <summary>
        /// Runs a very small test case against the Nereid watershed/solve endpoint.
        /// Builds the complete network graph, then hits the subgraph endpoint with
        /// a node that we know has a small upstream. Uses the return value to construct
        /// the input and then fires that against the solution endpoint.
        /// Available only to Sitka Admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/solution-test-case")]
        [SitkaAdminFeature]
        public async Task<IActionResult> SolutionTestCase()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var graph = _nereidService.BuildTotalNetworkGraph(_dbContext);

            // this subgraph is 23 nodes deep
            var single = graph.Nodes.Single(x => x.ID == "RSB_42");
            var subgraph = graph.GetUpstreamSubgraph(single);

            var allLoadingInputs = _dbContext.vNereidLoadingInputs.ToList();
            var allModelingBMPs = TreatmentBMPs.ListModelingTreatmentBMPs(_dbContext).ToList();
            var allWQMPNodes = _nereidService.GetWaterQualityManagementPlanNodes(_dbContext).ToList();
            var allModelingQuickBMPs = _dbContext.QuickBMPs.AsNoTracking().Include(x => x.TreatmentBMPType).Include(x => x.WaterQualityManagementPlan)
                .Where(x => x.PercentOfSiteTreated != null && x.TreatmentBMPType.IsAnalyzedInModelingModule).ToList();

            var modelBasins = _dbContext.ModelBasins.AsNoTracking().ToDictionary(x => x.ModelBasinID, x => x.ModelBasinKey);
            var precipitationZones = _dbContext.PrecipitationZones.AsNoTracking().ToDictionary(x => x.PrecipitationZoneID, x => x.DesignStormwaterDepthInInches);

            var delineations = vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext);
            var responseContent = await _nereidService.SolveSubgraph(subgraph, allLoadingInputs, allModelingBMPs, allWQMPNodes, allModelingQuickBMPs, true, modelBasins, precipitationZones, delineations, null, null);

            var stopwatchElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();

            return Ok(new { ElapsedTime = stopwatchElapsedMilliseconds, ResponseContent = responseContent });
        }

        /// <summary>
        /// Runs Nereid on the entire OC network graph using the solution sequence pattern.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/solve-oc")]
        [SitkaAdminFeature]
        public async Task<IActionResult> SolveOC()
        {
            try
            {
                var networkSolveResult = await _nereidService.TotalNetworkSolve(_dbContext, true);
                var solutionSummary = new SolutionSummary()
                {
                    NodesProcessed = networkSolveResult.NodesProcessed,
                    MissingNodeIDs = networkSolveResult.MissingNodeIDs,
                    Failed = false
                };
                return Ok(solutionSummary);
            }
            catch (NereidException<SolutionRequestObject, SolutionResponseObject> nereidException)
            {
                var solutionSummary = new SolutionSummary
                {
                    NodesProcessed = 0,
                    MissingNodeIDs = new List<string>(),
                    Failed = true,
                    ExceptionMessage = nereidException.Message,
                    InnerExceptionStackTrace = nereidException.InnerException?.StackTrace,
                    FailingRequest = nereidException.Request,
                    FailureResponse = nereidException.Response
                };
                return Ok(solutionSummary);
            }
        }
        

        /// <summary>
        /// Kicks off a delta solve for whatever DirtyModelNodes are in the system. Does not mark the nodes as processed.
        /// Testing purposes only; Sitka Admins only.
        /// </summary>
        /// <returns></returns>
        [HttpGet("nereid/delta-solve-test")]
        [SitkaAdminFeature]
        public async Task<IActionResult> DeltaSolveTest()
        {
            var dirtyModelNodes = _dbContext.DirtyModelNodes.ToList();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var networkSolveResult = await _nereidService.DeltaSolve(_dbContext, dirtyModelNodes, true);
                var deltaSolveTime = stopwatch.ElapsedMilliseconds;
                stopwatch.Stop();

                var solutionSummary = new SolutionSummary()
                {
                    SolveTime = deltaSolveTime,
                    NodesProcessed = networkSolveResult.NodesProcessed,
                    MissingNodeIDs = networkSolveResult.MissingNodeIDs,
                    Failed = false
                };
                return Ok(solutionSummary);
            }
            catch (NereidException<SolutionRequestObject, SolutionResponseObject> nereidException)
            {
                var solutionSummary = new SolutionSummary
                {
                    NodesProcessed = 0,
                    MissingNodeIDs = new List<string>(),
                    Failed = true,
                    ExceptionMessage = nereidException.Message,
                    InnerExceptionStackTrace = nereidException.InnerException?.StackTrace,
                    FailingRequest = nereidException.Request,
                    FailureResponse = nereidException.Response
                };
                return Ok(solutionSummary);
            }
        }
    }
}
