using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.WebServices;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Security;

namespace Neptune.Web.Controllers
{
    public class WebServicesController : NeptuneBaseController
    {

        [HttpGet]
        [JurisdictionEditFeature]
        public ViewResult Index()
        {
            var allMethods = FindAttributedMethods(typeof(PowerBIController), typeof(WebServiceNameAndDescriptionAttribute));
            var serviceDocumentationList = allMethods.Select(c => new WebServiceDocumentation(c)).OrderBy(c => c.Name).ToList();
            var webServiceAccessToken = new WebServiceToken(CurrentPerson.WebServiceAccessToken.ToString());
            var viewData = new IndexViewData(CurrentPerson, webServiceAccessToken, serviceDocumentationList);
            return RazorView<Index, IndexViewData>(viewData);
        }
    }
}