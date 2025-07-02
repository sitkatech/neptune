using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class UploadSimplifiedBMPsViewData : NeptuneViewData
    {
        public string WqmpsUploadUrl { get; }
        public string WqmpIndexUrl { get; }
        public List<string> ErrorList { get; }
        public string UploadTemplateUrl { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }

        public UploadSimplifiedBMPsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<string> errorList, EFModels.Entities.NeptunePage neptunePage, string wqmpsUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "Simplified BMPs Bulk Upload";
            EntityName = $"Data Hub";
            EntityUrl = SitkaRoute<DataHubController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            WqmpsUploadUrl = wqmpsUploadUrl;
            WqmpIndexUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            ErrorList = errorList;
            UploadTemplateUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.SimplifiedBMPBulkUploadTemplate());
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
        }
    }
}