using System.Globalization;
using System.Linq;
using System.Web;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.LandUseBlock
{
    public class LandUseBlockGridSpec : GridSpec<Models.LandUseBlock>
    {
        public LandUseBlockGridSpec()
        {

            Add("Trash Generating Unit ID", x => x.LandUseBlockID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x => x.PriorityLandUseType?.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use", 140, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Stormwater Jurisdiction", x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString(""), 170, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Area", x => ((x.LandUseBlockGeometry.Area ?? 0) * DbSpatialHelper.SqlGeometryAreaToAcres).ToString("F2", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add("TGU Area", x =>
                    (x.TrashGeneratingUnits.Sum(y => y.TrashGeneratingUnitGeometry.Area ?? 0) *
                     DbSpatialHelper.SqlGeometryAreaToAcres).ToString("F2", CultureInfo.InvariantCulture), 100,
                DhtmlxGridColumnFilterType.Numeric);
            Add("Number of TGUs", x => x.TrashGeneratingUnits.Count.ToString("F2", CultureInfo.InvariantCulture), 100,
                DhtmlxGridColumnFilterType.Numeric);
            Add("Loading Rate", x => x.TrashGenerationRate.ToString(CultureInfo.InvariantCulture), 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
        }

    }
}
