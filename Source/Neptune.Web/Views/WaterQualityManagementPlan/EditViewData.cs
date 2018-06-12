using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<SelectListItem> PersonSelectListItems { get; }
        public IEnumerable<SelectListItem> OrganizationSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanPrioritySelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanStatusSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanDevelopmentTypeSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanLandUseSelectListItems { get; }

        public EditViewData(IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions, IEnumerable<Person> people,
            IEnumerable<Models.Organization> organizations)
        {
            StormwaterJurisdictionSelectListItems = stormwaterJurisdictions.OrderBy(x => x.OrganizationDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(),
                    x => x.OrganizationDisplayName);
            PersonSelectListItems =
                people.ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(), x => x.FullNameFirstLastAndOrg);
            OrganizationSelectListItems =
                organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(), x => x.DisplayName);
            WaterQualityManagementPlanPrioritySelectListItems = WaterQualityManagementPlanPriority.All
                .OrderBy(x => x.SortOrder).ToSelectListWithEmptyFirstRow(
                    x => x.WaterQualityManagementPlanPriorityID.ToString(),
                    x => x.WaterQualityManagementPlanPriorityDisplayName);
            WaterQualityManagementPlanStatusSelectListItems = WaterQualityManagementPlanStatus.All
                .OrderBy(x => x.SortOrder).ToSelectListWithEmptyFirstRow(
                    x => x.WaterQualityManagementPlanStatusID.ToString(),
                    x => x.WaterQualityManagementPlanStatusDisplayName);
            WaterQualityManagementPlanDevelopmentTypeSelectListItems = WaterQualityManagementPlanDevelopmentType.All
                .OrderBy(x => x.SortOrder).ToSelectListWithEmptyFirstRow(
                    x => x.WaterQualityManagementPlanDevelopmentTypeID.ToString(),
                    x => x.WaterQualityManagementPlanDevelopmentTypeDisplayName);
            WaterQualityManagementPlanLandUseSelectListItems = WaterQualityManagementPlanLandUse.All
                .OrderBy(x => x.SortOrder).ToSelectListWithEmptyFirstRow(
                    x => x.WaterQualityManagementPlanLandUseID.ToString(),
                    x => x.WaterQualityManagementPlanLandUseDisplayName);
        }
    }
}
