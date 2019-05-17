using System.Globalization;
using System.Web;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TrashGeneratingUnit
{
    public class TrashGeneratingUnitGridSpec : GridSpec<Models.TrashGeneratingUnit>
    {
        public TrashGeneratingUnitGridSpec()
        {

            Add("Trash Generating Unit ID", x => x.TrashGeneratingUnitID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x =>
            {
                if (x.LandUseBlock == null)
                {
                    return "No data provided";
                }

                return x.LandUseBlock?.PriorityLandUseType?.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use";
            }, 140, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Governing OVTA Area", x => x.OnlandVisualTrashAssessmentArea?.GetDisplayNameAsDetailUrlNoPermissionCheck() ?? new HtmlString(""), 255, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Governing Treatment BMP", x => x.TreatmentBMP?.GetDisplayNameAsUrl() ?? new HtmlString(""), 190, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Stormwater Jurisdiction", x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString(""), 170, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Area", x => ((x.TrashGeneratingUnitGeometry.Area ?? 0) * DbSpatialHelper.SqlGeometryAreaToAcres).ToString("F2", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add("Last Updated", x => x.LastUpdateDate, 120,DhtmlxGridColumnFormatType.DateTime);
        }

    }
}
