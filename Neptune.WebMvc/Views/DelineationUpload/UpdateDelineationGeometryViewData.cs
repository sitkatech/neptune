using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.DelineationUpload
{
    public class UpdateDelineationGeometryViewData : NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        public string ApprovedGisUploadUrl { get; }

        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }

        public UpdateDelineationGeometryViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, string newGisUploadUrl, string approvedGisUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            NewGisUploadUrl = newGisUploadUrl;
            ApprovedGisUploadUrl = approvedGisUploadUrl;
            EntityName = FieldDefinitionType.Delineation.FieldDefinitionTypeDisplayName;
            PageTitle = "Update Delineation Geometry";

            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);

        }
    }
}
