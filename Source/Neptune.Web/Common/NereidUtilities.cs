using Neptune.Web.Areas.Modeling.Controllers;
using Neptune.Web.Areas.Modeling.Models.Nereid;
using Neptune.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading;

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

            MakeCentralizedBMPNodesAndEdges(dbContext, out var centralizedBMPEdges, out var centralizedBMPNodes, edges);
            nodes.AddRange(centralizedBMPNodes);
            edges.AddRange(centralizedBMPEdges);

            var graph = new Graph(true, nodes, edges);
            return graph;
        }

        public static string RegionalSubbasinNodeID(RegionalSubbasin regionalSubbasin)
        {
            return "RSB_" + regionalSubbasin.OCSurveyCatchmentID;
        }
        public static string RegionalSubbasinNodeID(int regionalSubbasinCatchmentID)
        {
            return "RSB_" + regionalSubbasinCatchmentID;
        }

        public static string TreatmentBMPNodeID(TreatmentBMP treatmentBMP)
        {
            return "BMP_" + treatmentBMP.TreatmentBMPID;
        }

        public static string TreatmentBMPNodeID(int treatmentBMPID)
        {
            return "BMP_" + treatmentBMPID;
        }

        public static string WaterQualityManagementPlanNodeID(int waterQualityManagementPlanID,
            int regionalSubbasinCatchmentID)
        {
            return "WQMP_" + waterQualityManagementPlanID + "_RSB_" + regionalSubbasinCatchmentID;
        }

        public static string DelineationNodeID(Delineation delineation)
        {
            return "Delineation_" + delineation.DelineationID;
        }

        public static string LandSurfaceNodeID(vNereidLoadingInput loadGeneratingUnit)
        {
            // provisional delineations are tracked in the LGU layer, but do not contribute runoff
            // to their respective BMPs in the model therefore those LGUs should fall back to their
            // WQMP if exists, otherwise their RSB.
            if (loadGeneratingUnit.DelineationID != null && loadGeneratingUnit.DelineationIsVerified == true)
            {
                return DelineationNodeID(loadGeneratingUnit.DelineationID.Value);
            }

            if (loadGeneratingUnit.WaterQualityManagementPlanID != null)
            {
                return WaterQualityManagementPlanNodeID(loadGeneratingUnit.WaterQualityManagementPlanID.Value,
                    loadGeneratingUnit.OCSurveyCatchmentID);
            }

            return RegionalSubbasinNodeID(loadGeneratingUnit.OCSurveyCatchmentID);
        }

        private static string DelineationNodeID(int delineationID)
        {
            return "Delineation_" + delineationID;
        }

        public static void MakeRSBNodesAndEdges(DatabaseEntities dbContext, out List<Edge> rsbEdges, out List<Node> rsbNodes)
        {
            var regionalSubbasinsInCoverage = dbContext.RegionalSubbasins.Where(x => x.IsInLSPCBasin == true).ToList();

            rsbNodes = regionalSubbasinsInCoverage
                .Select(x => new Node { ID = RegionalSubbasinNodeID(x), RegionalSubbasinID = x.RegionalSubbasinID }).ToList();

            rsbEdges = regionalSubbasinsInCoverage
                .Where(x => x.OCSurveyDownstreamCatchmentID != null).Select(x =>
                    new Edge()
                    {
                        SourceID = RegionalSubbasinNodeID(x),
                        TargetID = RegionalSubbasinNodeID(x.OCSurveyDownstreamCatchmentID.Value)
                    }).ToList();
        }

        public static void MakeDistributedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> distributedBMPEdges, out List<Node> distributedBMPNodes)
        {
            var vNereidTreatmentBMPRegionalSubbasins = dbContext.vNereidTreatmentBMPRegionalSubbasins.ToList();
            distributedBMPNodes = vNereidTreatmentBMPRegionalSubbasins
                .Select(x => new Node() { ID = TreatmentBMPNodeID(x.TreatmentBMPID), TreatmentBMPID = x.TreatmentBMPID }).ToList();

            distributedBMPEdges = vNereidTreatmentBMPRegionalSubbasins.Select(x => new Edge()
            {
                SourceID = TreatmentBMPNodeID(x.TreatmentBMPID),
                TargetID = RegionalSubbasinNodeID(x.OCSurveyCatchmentID)
            }).ToList();
        }

        public static void MakeDistributedDelineationNodesAndEdges(DatabaseEntities dbContext, out List<Edge> delineationEdges, out List<Node> delineationNodes)
        {
            var distributedDelineations = dbContext.Delineations.Include(x=>x.TreatmentBMP)
                .Where(x => x.DelineationTypeID == DelineationType.Distributed.DelineationTypeID &&
                            // don't include the provisionals
                            x.IsVerified &&
                            // don't include delineations for non-modeled BMPs
                            x.TreatmentBMP.TreatmentBMPType.IsAnalyzedInModelingModule &&
                            x.TreatmentBMP.RegionalSubbasinID != null &&
                            x.TreatmentBMP.LSPCBasinID != null).ToList();

            delineationNodes = distributedDelineations
                .Select(x => new Node()
                {
                    ID = DelineationNodeID(x),
                    Delineation = x
                }).ToList();

            delineationEdges = distributedDelineations
                .Select(x => new Edge()
                {
                    SourceID = DelineationNodeID(x),
                    TargetID = TreatmentBMPNodeID(x.TreatmentBMP)
                }).ToList();
        }

        private static void MakeUpstreamBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> colocationEdges, out List<Node> colocationNodes)
        {
            // we only need to add the upstream nodes, because any bmp that's not an upstream node has already been added

            var bmpColocations = dbContext.vNereidBMPColocations.ToList();
            colocationNodes = bmpColocations
                .Select(x => new Node()
                {
                    ID = TreatmentBMPNodeID(x.UpstreamBMPID),
                    TreatmentBMPID = x.UpstreamBMPID
                }).ToList();

            colocationEdges = bmpColocations
                .Select(x => new Edge()
                {
                    SourceID = TreatmentBMPNodeID(x.UpstreamBMPID),
                    TargetID = TreatmentBMPNodeID(x.DownstreamBMPID)
                }).ToList();
        }

        private static void MakeWQMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> wqmpEdges, out List<Node> wqmpNodes)
        {
            var wqmpRSBPairings = GetWaterQualityManagementPlanNodes(dbContext).ToList();

            wqmpNodes = wqmpRSBPairings.Select(x => new Node
            {
                ID = WaterQualityManagementPlanNodeID(x.WaterQualityManagementPlanID, x.OCSurveyCatchmentID),
                WaterQualityManagementPlan = x,
                RegionalSubbasinID = x.RegionalSubbasinID

            }).ToList();

            wqmpEdges = wqmpRSBPairings.Select(x => new Edge
            {
                SourceID = WaterQualityManagementPlanNodeID(x.WaterQualityManagementPlanID, x.OCSurveyCatchmentID),
                TargetID = RegionalSubbasinNodeID(x.OCSurveyCatchmentID)
            }).ToList();
        }

        public static IQueryable<WaterQualityManagementPlanNode> GetWaterQualityManagementPlanNodes(DatabaseEntities dbContext)
        {
            return dbContext.LoadGeneratingUnits.Include(x => x.RegionalSubbasin)
                .Where(x => x.WaterQualityManagementPlan != null && x.RegionalSubbasinID != null)
                .Select(x => new WaterQualityManagementPlanNode
                {
                    WaterQualityManagementPlanID = x.WaterQualityManagementPlanID.Value,
                    RegionalSubbasinID = x.RegionalSubbasinID.Value,
                    OCSurveyCatchmentID = x.RegionalSubbasin.OCSurveyCatchmentID
                }).Distinct();
        }

        private static void MakeCentralizedBMPNodesAndEdges(DatabaseEntities dbContext, out List<Edge> centralizedBMPEdges,
            out List<Node> centralizedBMPNodes, List<Edge> existingEdges)
        {
            centralizedBMPNodes = new List<Node>();
            centralizedBMPEdges = new List<Edge>();

            // todo: We're selecting by RowNumber == 1 so the network isn't invalid when there are
            // multiple centralized delineations per one regional subbasin. In future, we'll need
            // to find something better to do about that case than ignore the additionals.

            foreach (var rsbCentralizedBMPPairing in dbContext.vNereidRegionalSubbasinCentralizedBMPs.Where(x => x.RowNumber == 1).ToList())
            {
                var newCentralizedBMPNodeID = TreatmentBMPNodeID(rsbCentralizedBMPPairing.TreatmentBMPID);
                var existingRSBNodeID = RegionalSubbasinNodeID(rsbCentralizedBMPPairing.OCSurveyCatchmentID);


                // Find Edge with this RSB as its Source. Store its Target as ‘og_target’
                // If this is null, it means this RSB is at the end of the flow network.
                // We'll create an edge from RSB to BMP, but we won't create an edge from BMP to anywhere
                var edgeWithThisNodeAsSource = existingEdges.SingleOrDefault(x => x.SourceID == existingRSBNodeID);
                var ogTargetID = edgeWithThisNodeAsSource?.TargetID;

                if (edgeWithThisNodeAsSource != null)
                {
                    // Current rsb target equals centralized bmp node id
                    edgeWithThisNodeAsSource.TargetID = newCentralizedBMPNodeID;
                }
                else
                {
                    centralizedBMPEdges.Add(new Edge
                    { SourceID = existingRSBNodeID, TargetID = newCentralizedBMPNodeID });
                }

                // Find all Edges that point to this RSB. Set their Target to the new Centralized BMP Node
                foreach (var edge in existingEdges.Where(x => x.TargetID == existingRSBNodeID))
                {
                    edge.TargetID = newCentralizedBMPNodeID;
                }


                // Centralized BMP gets an edge pointing to "og_target"
                centralizedBMPNodes.Add(new Node { ID = newCentralizedBMPNodeID, TreatmentBMPID = rsbCentralizedBMPPairing.TreatmentBMPID });
                // see above
                if (ogTargetID != null)
                {
                    centralizedBMPEdges.Add(new Edge() { SourceID = newCentralizedBMPNodeID, TargetID = ogTargetID });
                }
            }
        }

        public static IQueryable<TreatmentBMP> ModelingTreatmentBMPs(DatabaseEntities dbContext)
        {
            return dbContext.TreatmentBMPs
                .Where(x => x.RegionalSubbasinID!= null && x.LSPCBasinID != null && x.TreatmentBMPType.TreatmentBMPModelingTypeID != null);

        }
        
        public static NereidResult<TResp> RunJobAtNereid<TReq, TResp>(TReq nereidRequestObject, string nereidRequestUrl, out string responseContent, HttpClient httpClient)
        {
            NereidResult<TResp> responseObject = null;
            var serializedRequest = JsonConvert.SerializeObject(nereidRequestObject);
            var requestStringContent = new StringContent(serializedRequest);

            var postResultContentAsStringResult = httpClient.PostAsync(nereidRequestUrl, requestStringContent).Result
                .Content.ReadAsStringAsync().Result;

            var deserializeObject = JsonConvert.DeserializeObject<NereidResult<TResp>>(postResultContentAsStringResult);

            var executing = deserializeObject.Status == NereidJobStatus.STARTED;
            var resultRoute = deserializeObject.ResultRoute;

            responseContent = postResultContentAsStringResult;

            if (deserializeObject.Detail != null)
            {
                throw new Exception(deserializeObject.Detail.ToString());
            }

            if (!executing)
            {
                responseObject = deserializeObject;
            }
            while (executing)
            {
                var stringResponse = httpClient.GetAsync($"{NeptuneWebConfiguration.NereidUrl}{resultRoute}").Result.Content
                    .ReadAsStringAsync().Result;

                var continuePollingResponse =
                    JsonConvert.DeserializeObject<NereidResult<object>>(stringResponse);

                if (continuePollingResponse.Detail != null)
                {
                    throw new Exception(deserializeObject.Detail.ToString());
                }

                if (continuePollingResponse.Status != NereidJobStatus.STARTED && continuePollingResponse.Status != NereidJobStatus.PENDING)
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

        public static IEnumerable<NereidResult> TotalNetworkSolve(out string stackTrace,
            out List<string> missingNodeIDs, out Graph graph, DatabaseEntities dbContext, HttpClient httpClient)
        {
            stackTrace = "";
            missingNodeIDs = new List<string>();

            graph = NereidUtilities.BuildNetworkGraph(dbContext);

            var nereidResults = NetworkSolveImpl(missingNodeIDs, graph, dbContext, httpClient, false);

            dbContext.Database.ExecuteSqlCommand(
                $"EXEC dbo.pDeleteNereidResults");
            dbContext.NereidResults.AddRange(nereidResults);
            // this is a relatively hefty set, so boost the timeout way beyond reasonable to make absolutely sure it doesn't die out on us.
            dbContext.Database.CommandTimeout = 600;
            dbContext.SaveChangesWithNoAuditing();

            return nereidResults;
        }

        public static IEnumerable<NereidResult> DeltaSolve(out string stackTrace,
            out List<string> missingNodeIDs, out Graph graph, DatabaseEntities dbContext,
            HttpClient httpClient, List<DirtyModelNode> dirtyModelNodes)
        {
            stackTrace = "";
            missingNodeIDs = new List<string>();

            graph = BuildNetworkGraph(dbContext);

            var dirtyTreatmentBMPIDs = dirtyModelNodes.Where(x => x.TreatmentBMPID != null).Select(y => y.TreatmentBMPID).ToList();
            var dirtyDelineationIDs = dirtyModelNodes.Where(x => x.DelineationID != null).Select(y => y.DelineationID).ToList();
            var dirtyRegionalSubbasinIDs = dirtyModelNodes.Where(x => x.RegionalSubbasinID != null).Select(y => y.RegionalSubbasinID).ToList();
            var dirtyWaterQualityManagementPlanIDs = dirtyModelNodes.Where(x => x.WaterQualityManagementPlanID != null).Select(y => y.WaterQualityManagementPlanID).ToList();

            var dirtyGraphNodes = graph.Nodes.Where(x => dirtyTreatmentBMPIDs.Contains(x.TreatmentBMPID) ||
                                                         dirtyDelineationIDs.Contains(x.Delineation?.DelineationID) ||
                                                         dirtyRegionalSubbasinIDs
                                                             .Contains(x.RegionalSubbasinID) ||
                                                         dirtyWaterQualityManagementPlanIDs
                                                             .Contains(x.WaterQualityManagementPlan?
                                                                 .WaterQualityManagementPlanID)).ToList();

            var subgraphUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/subgraph";

            var subgraphRequestObject = new NereidSubgraphRequestObject(graph, dirtyGraphNodes);

            var subgraphResult = RunJobAtNereid<NereidSubgraphRequestObject, SubgraphResult>(subgraphRequestObject,
                subgraphUrl, out _, httpClient);
            List<Node> nodesForSubgraph;
            try
            {
                nodesForSubgraph = subgraphResult.Data.SubgraphNodes.SelectMany(x => x.Nodes).Distinct().ToList();
            }
            catch (Exception e)
            {
                throw new NereidException<NereidSubgraphRequestObject, SubgraphResult>(
                    $"Exception thrown accessing result of subgraph call. Status was {subgraphResult.Status}.", e);
            }

            var deltaSubgraph = MakeSubgraphFromParentGraphAndNodes(graph, nodesForSubgraph);

            var deltaNereidResults = NetworkSolveImpl(missingNodeIDs, deltaSubgraph, dbContext, httpClient, true);

            var existingNereidResults = dbContext.NereidResults.Local;
            existingNereidResults.MergeUpsert(deltaNereidResults, existingNereidResults, (old, novel) =>
            
                (old.TreatmentBMPID == novel.TreatmentBMPID) && (old.DelineationID == novel.DelineationID) &&
                    (old.RegionalSubbasinID == novel.RegionalSubbasinID) &&
                    (old.WaterQualityManagementPlanID == novel.WaterQualityManagementPlanID)
            , (old, novel) =>
            {
                old.FullResponse = novel.FullResponse;
                old.LastUpdate = novel.LastUpdate;
            });
                
            dbContext.DirtyModelNodes.DeleteDirtyModelNode(dirtyModelNodes);

            dbContext.Database.CommandTimeout = 600;
            dbContext.SaveChangesWithNoAuditing();

            return deltaNereidResults;
        }

        private static List<NereidResult> NetworkSolveImpl(List<string> missingNodeIDs, Graph graph, DatabaseEntities dbContext,
            HttpClient httpClient, bool sendPreviousResults)
        {
            var solutionSequenceUrl =
                $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/solution_sequence?min_branch_size=12";

            var allLoadingInputs = dbContext.vNereidLoadingInputs.ToList();
            var allModelingBMPs = NereidUtilities.ModelingTreatmentBMPs(dbContext).ToList();
            var allwaterQualityManagementPlanNodes =
                NereidUtilities.GetWaterQualityManagementPlanNodes(dbContext).ToList();
            var allModelingQuickBMPs = dbContext.QuickBMPs.Include(x => x.TreatmentBMPType)
                .Where(x => x.PercentOfSiteTreated != null && x.TreatmentBMPType.IsAnalyzedInModelingModule).ToList();

            var solutionSequenceResult =
                NereidUtilities.RunJobAtNereid<SolutionSequenceRequest, SolutionSequenceResult>(
                    new SolutionSequenceRequest(graph), solutionSequenceUrl, out _, httpClient);

            // for the delta run, associate each node with its previous results
            if (sendPreviousResults)
            {
                var previousModelResults = dbContext.NereidResults.ToList();
                foreach (var node in graph.Nodes)
                {
                    var previousNodeResults = previousModelResults.SingleOrDefault(x =>
                        node.TreatmentBMPID == x.TreatmentBMPID &&
                        node.WaterQualityManagementPlan?.WaterQualityManagementPlanID ==
                        x.WaterQualityManagementPlanID &&
                        node.Delineation?.DelineationID == x.DelineationID &&
                        node.RegionalSubbasinID == x.RegionalSubbasinID)?.FullResponse;
                    if (previousNodeResults != null)
                    {
                        node.PreviousResults = JObject.Parse(previousNodeResults);
                    }
                }
            }

            foreach (var parallel in solutionSequenceResult.Data.SolutionSequence.Parallel)
            {
                foreach (var series in parallel.Series)
                {
                    var seriesNodes = series.Nodes;
                    var subgraph = MakeSubgraphFromParentGraphAndNodes(graph, seriesNodes);

                    SolveSubgraph(subgraph, allLoadingInputs, allModelingBMPs, allwaterQualityManagementPlanNodes,
                        allModelingQuickBMPs, out var notFoundNodes, httpClient);
                    missingNodeIDs.AddRange(notFoundNodes);
                }
            }

            var nereidResults = graph.Nodes.Where(x => x.Results != null).Select(x => new NereidResult(x.Results.ToString())
            {
                TreatmentBMPID = x.TreatmentBMPID,
                DelineationID = x.Delineation?.DelineationID,
                NodeID = x.ID,
                RegionalSubbasinID = x.RegionalSubbasinID,
                WaterQualityManagementPlanID = x.WaterQualityManagementPlan?.WaterQualityManagementPlanID,
                LastUpdate = DateTime.Now
            }).ToList();

            return nereidResults;
        }

        private static Graph MakeSubgraphFromParentGraphAndNodes(Graph graph, List<Node> nodes)
        {
            var subgraphNodeIDs = nodes.Select(x => x.ID).ToList();

            // create the subgraph that has these nodes as its nodes and the appropriate edges
            // appropriate edges = where target in nodes?
            var subgraphNodes = graph.Nodes.Where(x => subgraphNodeIDs.Contains(x.ID)).ToList();
            var subgraphEdges = graph.Edges.Where(x =>
                subgraphNodeIDs.Contains(x.TargetID) && subgraphNodeIDs.Contains(x.SourceID)).ToList();

            var subgraph = new Graph(true, subgraphNodes, subgraphEdges);
            return subgraph;
        }

        public static NereidResult<SolutionResponseObject> SolveSubgraph(Graph subgraph,
            List<vNereidLoadingInput> allLoadingInputs, List<TreatmentBMP> allModelingBMPs,
            List<WaterQualityManagementPlanNode> allWaterqualityManagementPlanNodes,
            List<QuickBMP> allModelingQuickBMPs, out List<string> notFoundNodes, HttpClient httpClient)
        {
            notFoundNodes = new List<string>();

            // Now I need to get the land_surface, treatment_facility, and treatment_site tables for this request.
            // these are going to look very much like the various calls made throughout the testing methods, but filtered
            // to the subgraph. Fortunately, we've added metadata to the nodes to help us do the filtration

            var delineationToIncludeIDs = subgraph.Nodes.Where(x => x.Delineation != null).Select(x => x.Delineation.DelineationID)
                .Distinct().ToList();
            var regionalSubbasinToIncludeIDs = subgraph.Nodes.Where(x => x.RegionalSubbasinID != null)
                .Select(x => x.RegionalSubbasinID).Distinct().ToList();
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
                PreviousResults = subgraph.Nodes.Where(x => x.PreviousResults != null).Select(x => x.PreviousResults).ToList()
            };
            NereidResult<SolutionResponseObject> results = null;
            try
            {
                results = NereidUtilities.RunJobAtNereid<SolutionRequestObject, SolutionResponseObject>(
                    solutionRequestObject, solveUrl,
                    out var responseContent, httpClient);
            }
            catch (Exception e)
            {
                throw new NereidException<SolutionRequestObject, SolutionResponseObject>(e.Message, e)
                {
                    Request = solutionRequestObject,
                    Response = results?.Data
                };
            }

            if (results?.Data.Errors != null && results.Data.Errors.Count > 0 && (results.Data.Results == null || results.Data.Results.Count == 0) )
            {
                throw new NereidException<SolutionRequestObject, SolutionResponseObject>
                    { Request = solutionRequestObject, Response = results.Data };
            }

            var previousResultsKeys = results.Data.PreviousResultsKeys;
            foreach (var dataLeafResult in results.Data.Results)
            {
                var node = subgraph.Nodes.SingleOrDefault(x => x.ID == dataLeafResult["node_id"].ToString());
                if (node == null)
                {
                    // this is an edge case that should only happen if an RSB in the SOC area has
                    // its downstream catchment outside the SOC area for some reason.
                    notFoundNodes.Add(dataLeafResult["node_id"].ToString());
                }
                else
                {
                    node.Results = dataLeafResult;

                    // track the smaller subset of results that need to be sent for subsequent calls
                    var previousResults = new JObject();
                    foreach (var key in previousResultsKeys)
                    {
                        var value = dataLeafResult[key];
                        previousResults.Add(key, value);
                    }

                    node.PreviousResults = previousResults;
                }
            }

            foreach (var dataLeafResult in results.Data.LeafResults)
            {
                var node = subgraph.Nodes.SingleOrDefault(x => x.ID == dataLeafResult["node_id"].ToString());
                if (node == null)
                {
                    notFoundNodes.Add(dataLeafResult["node_id"].ToString());
                }
                else
                {
                    // don't store the leaf results if already data at this node--most of the time these nodes are read-only
                    if (node.Results == null)
                    {
                        node.Results = dataLeafResult;
                    }
                }
            }

            return results;
        }

        public static void MarkTreatmentBMPDirty(TreatmentBMP treatmentBMP, DatabaseEntities dbContext)
        {
            var dirtyModelNode = new DirtyModelNode(DateTime.Now)
            {
                TreatmentBMPID = treatmentBMP.TreatmentBMPID
            };

            dbContext.DirtyModelNodes.Add(dirtyModelNode);

            dbContext.SaveChanges();
        }

        public static void MarkTreatmentBMPDirty(IEnumerable<TreatmentBMP> treatmentBmpsUpdated, DatabaseEntities dbContext)
        {
            var dirtyModelNodes = treatmentBmpsUpdated.Select(x=> new DirtyModelNode(DateTime.Now){TreatmentBMPID = x.TreatmentBMPID});

            dbContext.DirtyModelNodes.AddRange(dirtyModelNodes);
            
            dbContext.SaveChanges();
        }

        public static void MarkDownstreamNodeDirty(TreatmentBMP treatmentBMP, DatabaseEntities dbContext)
        {
            // if this bmp is an upstream, then its downstream node is, obviously...
            if (treatmentBMP.TreatmentBMPsWhereYouAreTheUpstreamBMP.Any())
            {
                MarkTreatmentBMPDirty(treatmentBMP.TreatmentBMPsWhereYouAreTheUpstreamBMP.ToList(), dbContext);
                return;
            }

            // otherwise, we're looking for either the Regional Subbasin or the Centralized BMP of the Regional Subbasin
            var regionalSubbasinID = treatmentBMP.RegionalSubbasinID;

            var centralizedBMP = dbContext.vNereidRegionalSubbasinCentralizedBMPs.SingleOrDefault(x => x.RegionalSubbasinID == regionalSubbasinID && x.RowNumber == 1);
            if (centralizedBMP != null)
            {
                MarkTreatmentBMPDirty(centralizedBMP, dbContext);
                return;
            }

            // no centralized BMPs there, just go ahead and mark the regional subbasin
            MarkRegionalSubbasinDirty(regionalSubbasinID, dbContext);

        }

        public static void MarkDownstreamNodeDirty(WaterQualityManagementPlan waterQualityManagementPlan, DatabaseEntities dbContext)
        {
            // otherwise, we're looking for either the Regional Subbasin or the Centralized BMP of the Regional Subbasin
            var regionalSubbasinIDs = waterQualityManagementPlan.LoadGeneratingUnits.Select(x => x.RegionalSubbasinID)
                .Distinct().ToList();

            var centralizedBMP = dbContext.vNereidRegionalSubbasinCentralizedBMPs.Where(x =>
                regionalSubbasinIDs.Contains(x.RegionalSubbasinID) && x.RowNumber == 1);

            foreach (var bmp in centralizedBMP)
            {
                MarkTreatmentBMPDirty(bmp, dbContext);
            }

            foreach (var regionalSubbasinID in regionalSubbasinIDs)
            {
                MarkRegionalSubbasinDirty(regionalSubbasinID, dbContext);
            }
        }

        private static void MarkRegionalSubbasinDirty(int? regionalSubbasinID, DatabaseEntities dbContext)
        {
           
            var dirtyModelNode = new DirtyModelNode(DateTime.Now)
            {
                RegionalSubbasinID = regionalSubbasinID
            };

            dbContext.DirtyModelNodes.Add(dirtyModelNode);

            dbContext.SaveChanges();
        }

        private static void MarkTreatmentBMPDirty(vNereidRegionalSubbasinCentralizedBMP treatmentBMP, DatabaseEntities dbContext)
        {
            var dirtyModelNode = new DirtyModelNode(DateTime.Now)
            {
                TreatmentBMPID = treatmentBMP.TreatmentBMPID
            };

            dbContext.DirtyModelNodes.Add(dirtyModelNode);

            dbContext.SaveChanges();
        }

        public static void MarkDelineationDirty(Delineation delineation, DatabaseEntities dbContext)
        {
            var dirtyModelNode = new DirtyModelNode(DateTime.Now)
            {
                DelineationID = delineation.DelineationID
            };

            dbContext.DirtyModelNodes.Add(dirtyModelNode);

            dbContext.SaveChanges();
        }

        public static void MarkWqmpDirty(WaterQualityManagementPlan waterQualityManagementPlan, DatabaseEntities dbContext)
        {
            var dirtyModelNode = new DirtyModelNode(DateTime.Now)
            {
                WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID
            };

            dbContext.DirtyModelNodes.Add(dirtyModelNode);

            dbContext.SaveChanges();
        }
    }

    public class WaterQualityManagementPlanNode
    {
        public int WaterQualityManagementPlanID { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
    }
}
