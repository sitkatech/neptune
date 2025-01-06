using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.Home;
using Neptune.WebMvc.Views.Shared.JurisdictionControls;

namespace Neptune.WebMvc.Controllers;

public class DataHubController : NeptuneBaseController<DataHubController>
{
    public DataHubController(NeptuneDbContext dbContext, ILogger<DataHubController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
    {
    }

    [HttpGet]
    public ViewResult Index()
    {
        return null;
    }

}