using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.WebServices;
using Index = Neptune.Web.Views.WebServices.Index;

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
            var serviceDocumentationList = allMethods.Select(c => new WebServiceDocumentation(c, _dbContext, _linkGenerator)).OrderBy(x => x.Name).ToList();
            var webServiceAccessToken = new WebServiceToken(_dbContext, CurrentPerson.WebServiceAccessToken.ToString());
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, webServiceAccessToken, serviceDocumentationList);
            return RazorView<Index, IndexViewData>(viewData);
        }
    }
}