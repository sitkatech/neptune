using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class NewViewData : NeptuneViewData
    {
        public string TreatmentBMPIndexUrl { get; }
        public Shared.Location.EditLocationViewData EditLocationViewData { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypeSelectListItems { get; }
        public IEnumerable<SelectListItem> OwnerOrganizationSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPLifespanTypes { get; }
        public IEnumerable<SelectListItem> TrashCaptureStatusTypes { get; }
        public IEnumerable<SelectListItem> SizingBasisTypes { get; }

        public NewViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            IEnumerable<EFModels.Entities.TreatmentBMPType> treatmentBMPTypes,
            IEnumerable<EFModels.Entities.Organization> organizations,
            Shared.Location.EditLocationViewData editLocationViewData,
            IEnumerable<EFModels.Entities.WaterQualityManagementPlan> waterQualityManagementPlans,
            IEnumerable<TreatmentBMPLifespanType> treatmentBMPLifespanTypes,
            IEnumerable<TrashCaptureStatusType> trashCaptureStatusTypes, IEnumerable<SizingBasisType> sizingBasisTypes)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EditLocationViewData = editLocationViewData;
            SizingBasisTypes = sizingBasisTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.SizingBasisTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.SizingBasisTypeDisplayName.ToString(CultureInfo.InvariantCulture));

            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;
            PageTitle = $"New {FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabel()}";

            TrashCaptureStatusTypes = trashCaptureStatusTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.TrashCaptureStatusTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.TrashCaptureStatusTypeDisplayName.ToString(CultureInfo.InvariantCulture));

            StormwaterJurisdictionSelectListItems = stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName()).ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.GetOrganizationDisplayName());
            TreatmentBMPTypeSelectListItems = treatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName).ToSelectListWithEmptyFirstRow(x => x.TreatmentBMPTypeID.ToString(CultureInfo.InvariantCulture), y => y.TreatmentBMPTypeName);
            OwnerOrganizationSelectListItems = organizations.OrderBy(x => x.GetDisplayName()).ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.GetDisplayName(), "Same as the BMP Jurisdiction");
            TreatmentBMPIndexUrl = treatmentBMPIndexUrl;
            WaterQualityManagementPlanSelectListItems = BuildWaterQualityPlanSelectList(waterQualityManagementPlans);

            TreatmentBMPLifespanTypes = treatmentBMPLifespanTypes.ToSelectListWithEmptyFirstRow(
                x => x.TreatmentBMPLifespanTypeID.ToString(CultureInfo.InvariantCulture), x => x.TreatmentBMPLifespanTypeDisplayName.ToString(CultureInfo.InvariantCulture), "Unknown");
        }

        private static IEnumerable<SelectListItem> BuildWaterQualityPlanSelectList(
            IEnumerable<EFModels.Entities.WaterQualityManagementPlan> waterQualityManagementPlans)
        {
            var selectListItems = waterQualityManagementPlans
                .ToSelectList(x => x.WaterQualityManagementPlanID.ToString(), x => x.WaterQualityManagementPlanName)
                .ToList();
            selectListItems.Insert(0,
                new SelectListItem { Text = "No Associated WQMP", Value = string.Empty });
            return selectListItems;
        }
    }
}
