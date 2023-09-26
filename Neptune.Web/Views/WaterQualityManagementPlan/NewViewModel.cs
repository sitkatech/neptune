using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewViewModel : EditViewModel, IValidatableObject
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

        public override void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.StormwaterJurisdictionID =
                StormwaterJurisdictionID ?? ModelObjectHelpers.NotYetAssignedID;
            base.UpdateModel(waterQualityManagementPlan);
        }

        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            if (dbContext.WaterQualityManagementPlans.Any(x =>
                    x.WaterQualityManagementPlanName == WaterQualityManagementPlanName))
            {
                yield return new SitkaValidationResult<NewViewModel, string>("Name is already in use.", m => m.WaterQualityManagementPlanName);
            }
        }
    }
}
