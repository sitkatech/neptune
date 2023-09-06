using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Common.Models;

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

        public NewViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan) : base(waterQualityManagementPlan)
        {
            StormwaterJurisdictionID = waterQualityManagementPlan.StormwaterJurisdictionID;
        }

        public override void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.StormwaterJurisdictionID =
                StormwaterJurisdictionID ?? ModelObjectHelpers.NotYetAssignedID;
            base.UpdateModel(waterQualityManagementPlan);
        }

        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
            //todo:
            //if (_dbContext.WaterQualityManagementPlans.Any(x =>
            //    x.WaterQualityManagementPlanName == WaterQualityManagementPlanName))
            //{
            //    yield return new SitkaValidationResult<NewViewModel, string>("Name is already in use.", m => m.WaterQualityManagementPlanName);
            //}
        }
    }
}
