using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.WebServices;
using Index = Neptune.WebMvc.Views.WebServices.Index;

namespace Neptune.WebMvc.Controllers
{
    public class WebServicesController : NeptuneBaseController<WebServicesController>
    {
        public WebServicesController(NeptuneDbContext dbContext, ILogger<WebServicesController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [JurisdictionEditFeature]
        public ViewResult Index()
        {
            var allMethods = FindAttributedMethods(typeof(PowerBIController), typeof(WebServiceNameAndDescriptionAttribute));
            var serviceDocumentationList = allMethods.Select(c => new WebServiceDocumentation(c, _dbContext, _linkGenerator)).OrderBy(x => x.Name).ToList();
            var webServiceAccessToken = new WebServiceToken(_dbContext, CurrentPerson.WebServiceAccessToken.ToString());
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, webServiceAccessToken, serviceDocumentationList);
            return RazorView<Index, IndexViewData>(viewData);
        }
    }
}