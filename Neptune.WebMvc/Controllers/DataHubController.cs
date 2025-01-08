using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Views.DataHub;
using Index = Neptune.WebMvc.Views.DataHub.Index;


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
        var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePages);
        return RazorView<Index, IndexViewData>(viewData);
    }

}