using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpVerifyViewModel : FormViewModel, IValidatableObject
    {

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpVerifyViewModel()
        {
        }

        public EditWqmpVerifyViewModel(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if ()
            //{
                yield return new SitkaValidationResult<EditViewModel, string>("Name is already in use.", m => m.WaterQualityManagementPlanName);
            //}
        }
    }
}
