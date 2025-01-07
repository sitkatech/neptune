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
        var treatmentBMPDataHub = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.BMPDataHub);
        var delineationDataHub = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.DelineationDataHub);
        var fieldVisitDataHub = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.FieldVisitDataHub);
        var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMPDataHub, delineationDataHub, fieldVisitDataHub);
        return RazorView<Index, IndexViewData>(viewData);
    }

}