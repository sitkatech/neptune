using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.DataHub;
using Index = Neptune.WebMvc.Views.DataHub.Index;
using IndexViewData = Neptune.WebMvc.Views.DataHub.IndexViewData;


namespace Neptune.WebMvc.Controllers;

public class DataHubController : NeptuneBaseController<DataHubController>
{
    public DataHubController(NeptuneDbContext dbContext, ILogger<DataHubController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
    {
    }

    [HttpGet]
    public ViewResult Index()
    {
        var neptunePages = _dbContext.NeptunePages.ToList();
        var allMethods = FindAttributedMethods(typeof(PowerBIController), typeof(WebServiceNameAndDescriptionAttribute));
        var serviceDocumentationList = allMethods.Select(c => new WebServiceDocumentation(c, _dbContext, _linkGenerator)).OrderBy(x => x.Name).ToList();
        var webServiceAccessToken = new WebServiceToken(_dbContext, CurrentPerson.WebServiceAccessToken.ToString());
        var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePages, webServiceAccessToken, serviceDocumentationList);
        return RazorView<Index, IndexViewData>(viewData);
    }

}