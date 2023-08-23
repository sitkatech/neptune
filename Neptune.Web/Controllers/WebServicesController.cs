using Neptune.Web.Views.WebServices;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Controllers
{
    public class WebServicesController : NeptuneBaseController<WebServicesController>
    {
        public WebServicesController(NeptuneDbContext dbContext, ILogger<WebServicesController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

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