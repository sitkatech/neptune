using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

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
        public ContentResult TestNereidNetworkValidator()
        {
            var networkValidatorUrl = $"{NeptuneWebConfiguration.NereidUrl}/network/validate";
            var postResultContentAsStringResult = HttpClient.PostAsync(networkValidatorUrl, new StringContent("{\"directed\": true, \"nodes\": [{\"id\": \"A\"}, {\"id\": \"B\"} ], \"edges\": [{\"source\": \"A\", \"target\": \"B\"} ] }")).Result.Content.ReadAsStringAsync().Result;
            return Content(postResultContentAsStringResult);
        }
    }
}