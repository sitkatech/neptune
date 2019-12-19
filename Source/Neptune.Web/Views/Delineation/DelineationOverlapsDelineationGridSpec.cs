using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationOverlapsDelineationGridSpec : GridSpec<Models.Delineation>
    {
        public DelineationOverlapsDelineationGridSpec()
        {
            Add("", x => x.GetDetailUrlForGrid(), 60, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TreatmentBMP.ToGridHeaderString("Name"), x => UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMP.TreatmentBMPName), 170, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TreatmentBMPType.ToGridHeaderString("Type"), x => x.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Area.ToGridHeaderString("Area (acres)"), x => x.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres, 100);
            Add("Date of Last Delineation Modification", x => x.DateLastModified, 130);
            Add("Date of Last Delineation Verification", x => x.DateLastVerified, 130);
            Add(Models.FieldDefinition.Jurisdiction.ToGridHeaderString(), x => x.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName(), 150);
            Add(Models.FieldDefinition.Area.ToGridHeaderString("Area of Overlap (acres)"), x => x.DelineationOverlaps.Sum(y => y.OverlappingGeometry.Area) * DbSpatialHelper.SquareMetersToAcres, 120);
            Add("Overlapping Delineations", x => new HtmlString(string.Join(", ", x.DelineationOverlaps.Select(y =>
                UrlTemplate.MakeHrefString(TreatmentBMPModelExtensions.DetailUrlTemplate.ParameterReplace(y.Delineation.TreatmentBMPID), y.Delineation.TreatmentBMP.TreatmentBMPName)))), 240, DhtmlxGridColumnFilterType.Html);
        }
    }
}