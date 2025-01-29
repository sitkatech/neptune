using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class UploadWqmpBoundaryFromAPNsViewData : NeptuneViewData
    {
        public string WqmpsUploadUrl { get; }
        public string WqmpIndexUrl { get; }
        public List<string> ErrorList { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }

        public UploadWqmpBoundaryFromAPNsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<string> errorList, EFModels.Entities.NeptunePage neptunePage, string wqmpsUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "WQMP Boundary from APN List";
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            WqmpsUploadUrl = wqmpsUploadUrl;
            WqmpIndexUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            ErrorList = errorList;
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
        }
    }
}