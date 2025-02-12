using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using System.Globalization;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Neptune.WebMvc.Views.Shared.ModeledPerformance;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class WqmpModelingOptionsViewData : NeptuneViewData
    {
        public WqmpModelingOptionsViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "WQMP Modeling Options";
            EntityName = $"WQMP";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                x => x.Index());

        }
    }
}
