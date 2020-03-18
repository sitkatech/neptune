using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;
using Hangfire;
using Neptune.Web.Areas.Modeling.NereidModels;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunLoadGeneratingUnitRefreshJob(CurrentPerson.PersonID, null));
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
                },new List<Edge>
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
        public JsonResult ValidateRSBNetwork()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/api/v1/network/validate";

            var nodes = HttpRequestStorage.DatabaseEntities.RegionalSubbasins
                .Select(x => new Node {ID= x.OCSurveyCatchmentID.ToString() }).ToList();

            var edges = HttpRequestStorage.DatabaseEntities.RegionalSubbasins
                .Where(x => x.OCSurveyDownstreamCatchmentID != null).Select(x =>
                    new Edge(){ SourceID = x.OCSurveyCatchmentID.ToString(), TargetID = x.OCSurveyDownstreamCatchmentID.ToString() }).ToList();

            var graph = new Graph(true, nodes, edges);

            var serializedGraph = JsonConvert.SerializeObject(graph);
            var stringContent = new StringContent(serializedGraph);
            var postResultContentAsStringResult = HttpClient.PostAsync(networkValidatorUrl, stringContent).Result.Content.ReadAsStringAsync().Result;

            var deserializeObject = JsonConvert.DeserializeObject<NereidResult<NetworkValidatorResult>>(postResultContentAsStringResult);

            var executing = deserializeObject.Status == NereidJobStatus.STARTED;
            var resultRoute = deserializeObject.ResultRoute;

            NetworkValidatorResult networkValidatorResult = new NetworkValidatorResult();

            while (executing)
            {
                var stringResponse = HttpClient.GetAsync($"{NeptuneWebConfiguration.NereidUrl}{resultRoute}").Result.Content.ReadAsStringAsync().Result;

                var continuePollingResponse =
                    JsonConvert.DeserializeObject<NereidResult<NetworkValidatorResult>>(stringResponse);

                if (continuePollingResponse.Status != NereidJobStatus.STARTED)
                {
                    executing = false;
                    networkValidatorResult = JsonConvert.DeserializeObject<NetworkValidatorResult>(stringResponse);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            return Json(networkValidatorResult, JsonRequestBehavior.AllowGet);
        }
    }
}

namespace Neptune.Web.Areas.Modeling.NereidModels
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
        [JsonProperty("result")]
        public T Result { get; set; }
        [JsonProperty("result_route")]
        public string ResultRoute { get; set; }
    }
    
    public class NetworkValidatorResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public enum NereidJobStatus
    {
        STARTED,
        SUCCESS,
        valid
    }
}