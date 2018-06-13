using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [Required]
        [DisplayName("Name")]
        [MaxLength(Models.WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName)]
        public string WaterQualityManagementPlanName { get; set; }

        [Required]
        [DisplayName("Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }

        [Required]
        [DisplayName("Priority")]
        public int? WaterQualityManagementPlanPriorityID { get; set; }

        [Required]
        [DisplayName("Status")]
        public int? WaterQualityManagementPlanStatusID { get; set; }

        [Required]
        [DisplayName("Development Type")]
        public int? WaterQualityManagementPlanDevelopmentTypeID { get; set; }

        [Required]
        [DisplayName("Land Use")]
        public int? WaterQualityManagementPlanLandUseID { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            WaterQualityManagementPlanName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            StormwaterJurisdictionID = waterQualityManagementPlan.StormwaterJurisdictionID;
            WaterQualityManagementPlanPriorityID = waterQualityManagementPlan.WaterQualityManagementPlanPriorityID;
            WaterQualityManagementPlanStatusID = waterQualityManagementPlan.WaterQualityManagementPlanStatusID;
            WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlan.WaterQualityManagementPlanDevelopmentTypeID;
            WaterQualityManagementPlanLandUseID = waterQualityManagementPlan.WaterQualityManagementPlanLandUseID;
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.WaterQualityManagementPlanName = WaterQualityManagementPlanName;
            waterQualityManagementPlan.StormwaterJurisdictionID =
                StormwaterJurisdictionID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlan.WaterQualityManagementPlanPriorityID =
                WaterQualityManagementPlanPriorityID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlan.WaterQualityManagementPlanStatusID =
                WaterQualityManagementPlanStatusID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlan.WaterQualityManagementPlanDevelopmentTypeID =
                WaterQualityManagementPlanDevelopmentTypeID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlan.WaterQualityManagementPlanLandUseID =
                WaterQualityManagementPlanLandUseID ?? ModelObjectHelpers.NotYetAssignedID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Any(x =>
                x.WaterQualityManagementPlanName == WaterQualityManagementPlanName &&
                x.WaterQualityManagementPlanID != WaterQualityManagementPlanID))
            {
                yield return new SitkaValidationResult<EditViewModel, string>("Name is already in use.", m => m.WaterQualityManagementPlanName);
            }
        }
    }
}
