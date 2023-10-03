using Microsoft.AspNetCore.Html;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationOverlapsDelineationGridSpec : GridSpec<EFModels.Entities.Delineation>
    {
        public DelineationOverlapsDelineationGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var delineationMapUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            Add("", x => UrlTemplate.MakeHrefString(delineationMapUrlTemplate.ParameterReplace(x.DelineationID), "View", new Dictionary<string, string> {{"class", "gridButton"}}), 60, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMP.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.Area.ToGridHeaderString("Area (ac)"), x => x.DelineationGeometry.Area * Constants.SquareMetersToAcres, 100);
            Add("Date of Last Delineation Modification", x => x.DateLastModified, 130);
            Add("Date of Last Delineation Verification", x => x.DateLastVerified, 130);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.TreatmentBMP.StormwaterJurisdictionID), x.TreatmentBMP.StormwaterJurisdiction.Organization.OrganizationName), 150);
            Add(FieldDefinitionType.Area.ToGridHeaderString("Area of Overlap (ac)"), x => x?.DelineationOverlapDelineations.Sum(y => y.OverlappingGeometry.Area) * Constants.SquareMetersToAcres, 120);
            Add("Overlapping Delineations", x => new HtmlString(string.Join(", ", x.DelineationOverlapDelineations.OrderBy(y => y.OverlappingDelineation.TreatmentBMP.TreatmentBMPName).Select(y =>
                UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(y.Delineation.TreatmentBMPID), y.OverlappingDelineation.TreatmentBMP.TreatmentBMPName)))), 240, DhtmlxGridColumnFilterType.Html);
        }
    }
}   