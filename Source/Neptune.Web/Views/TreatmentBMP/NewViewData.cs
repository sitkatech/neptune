using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class NewViewData : NeptuneViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypeSelectListItems { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }
        public string TreatmentBMPIndexUrl { get; }
        public IEnumerable<SelectListItem> OwnerOrganizationSelectListItems { get; }
        public Shared.Location.EditLocationViewData EditLocationViewData { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPLifespanTypes { get; }
        public IEnumerable<SelectListItem> TrashCaptureStatusTypes { get; }

        public NewViewData(Person currentPerson,
            Models.TreatmentBMP treatmentBMP,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            IEnumerable<Models.TreatmentBMPType> treatmentBMPTypes,
            List<Models.Organization> organizations,
            Shared.Location.EditLocationViewData editLocationViewData,
            IEnumerable<Models.WaterQualityManagementPlan> waterQualityManagementPlans,
            IEnumerable<TreatmentBMPLifespanType> treatmentBMPLifespanTypes, List<TrashCaptureStatusType> trashCaptureStatusTypes)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            EditLocationViewData = editLocationViewData;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;
            if (treatmentBMP != null)
            {
                SubEntityName = treatmentBMP.TreatmentBMPName;
                SubEntityUrl = treatmentBMP.GetDetailUrl();
                TreatmentBMP = treatmentBMP;
            }
            PageTitle = $"New {Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabel()}";

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

        private IEnumerable<SelectListItem> BuildWaterQualityPlanSelectList(
            IEnumerable<Models.WaterQualityManagementPlan> waterQualityManagementPlans)
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
