using System.Globalization;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.LandUseBlock
{
    public class LandUseBlockGridSpec : GridSpec<Models.LandUseBlock>
    {
        public LandUseBlockGridSpec()
        {

            Add("Block ID", x => x.LandUseBlockID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x => x.PriorityLandUseType?.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use", 140, DhtmlxGridColumnFilterType.Text);
            Add("Stormwater Jurisdiction", x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString(""), 170, DhtmlxGridColumnFilterType.Html);
            Add("Block Area", x => ((x.LandUseBlockGeometry.Area ?? 0) * DbSpatialHelper.SquareMetersToAcres).ToString("F2", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add(Models.FieldDefinition.LoadingRate.ToGridHeaderString(), x => x.TrashGenerationRate.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use Description", x => x.LandUseDescription, 100, DhtmlxGridColumnFilterType.Text);
            Add("Median Household Income Residential", x => x.MedianHouseholdIncomeResidential.ToStringCurrency(), 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Median Household Income Retail", x => x.MedianHouseholdIncomeRetail.ToStringCurrency(), 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Trash Results Area", x =>
                    (x.TrashGeneratingUnits.Sum(y => y.TrashGeneratingUnitGeometry.Area ?? 0) *
                     DbSpatialHelper.SquareMetersToAcres).ToString("F2", CultureInfo.InvariantCulture), 100,
                DhtmlxGridColumnFilterType.Numeric);
            Add("Permit Type", x => x.PermitType.PermitTypeDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
        }

    }
}
