using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.Delineation
{
    public class MisalignedDelineationGridSpec : GridSpec<EFModels.Entities.Delineation>
    {
        public MisalignedDelineationGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var delineationMapUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(UrlTemplate.Parameter1Int)));
            Add("", x => UrlTemplate.MakeHrefString(delineationMapUrlTemplate.ParameterReplace(x.DelineationID), "View", new Dictionary<string, string> {{"class", "gridButton"}}), 60, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMP.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.DelineationType.ToGridHeaderString(), x => x.DelineationType.DelineationTypeDisplayName, 130, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.Area.ToGridHeaderString("Area (ac)"), x => x.DelineationGeometry.Area * Constants.SquareMetersToAcres, 100);
            Add("Date of Last Delineation Modification", x => x.DateLastModified, 160);
            Add("Date of Last Delineation Verification", x => x.DateLastVerified, 160);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.TreatmentBMP.StormwaterJurisdictionID), x.TreatmentBMP.StormwaterJurisdiction.Organization.OrganizationName), 150);
        }
    }
}