using System.Globalization;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.LandUseBlock
{
    public class LandUseBlockGridSpec : GridSpec<EFModels.Entities.LandUseBlock>
    {
        public LandUseBlockGridSpec(LinkGenerator linkGenerator)
        {
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            Add("Block ID", x => x.LandUseBlockID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add(FieldDefinitionType.LandUseType.ToGridHeaderString(), x => x.PriorityLandUseType?.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use", 140, DhtmlxGridColumnFilterType.Text);
            Add("Stormwater Jurisdiction", x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.StormwaterJurisdiction.GetOrganizationDisplayName()), 170, DhtmlxGridColumnFilterType.Html);
            Add("Block Area", x => (x.LandUseBlockGeometry.Area * Constants.SquareMetersToAcres).ToString("F2", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add(FieldDefinitionType.TrashGenerationRate.ToGridHeaderString(), x => x.TrashGenerationRate.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.LandUseDescription.ToGridHeaderString(), x => x.LandUseDescription, 100, DhtmlxGridColumnFilterType.Text);
            Add("Median Household Income Residential", x => x.MedianHouseholdIncomeResidential.ToStringCurrency(), 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Median Household Income Retail", x => x.MedianHouseholdIncomeRetail.ToStringCurrency(), 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Trash Results Area", x =>
                    (x.TrashGeneratingUnits.Sum(y => y.TrashGeneratingUnitGeometry.Area) *
                     Constants.SquareMetersToAcres).ToString("F2", CultureInfo.InvariantCulture), 100,
                DhtmlxGridColumnFilterType.Numeric);
            Add("Permit Type", x => x.PermitType.PermitTypeDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use for TGR", x => x.LandUseForTGR, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
        }

    }
}
