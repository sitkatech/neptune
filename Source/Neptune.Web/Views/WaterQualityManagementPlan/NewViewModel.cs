using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewViewModel : EditViewModel
    {
        [Required]
        [DisplayName("Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewViewModel()
        {
        }

        public NewViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan) : base(waterQualityManagementPlan)
        {
            StormwaterJurisdictionID = waterQualityManagementPlan.StormwaterJurisdictionID;
        }

        public override void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.StormwaterJurisdictionID =
                StormwaterJurisdictionID ?? ModelObjectHelpers.NotYetAssignedID;
            base.UpdateModels(waterQualityManagementPlan);
        }

        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Any(x =>
                x.WaterQualityManagementPlanName == WaterQualityManagementPlanName))
            {
                yield return new SitkaValidationResult<NewViewModel, string>("Name is already in use.", m => m.WaterQualityManagementPlanName);
            }
        }
    }
}
