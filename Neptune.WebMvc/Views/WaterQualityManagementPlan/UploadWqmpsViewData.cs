using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class UploadWqmpsViewData : NeptuneViewData
    {
        public string WqmpsUploadUrl { get; }
        public string WqmpIndexUrl { get; }
        public List<string> ErrorList { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }
        public UploadWqmpsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<string> errorList, EFModels.Entities.NeptunePage neptunePage, string wqmpsUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "WQMP Bulk Upload";
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            WqmpsUploadUrl = wqmpsUploadUrl;
            WqmpIndexUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            ErrorList = errorList;
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);

        }
    }
}