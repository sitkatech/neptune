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

        public WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public int WaterQualityManagementPlanID { get; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpVerifyViewModel()
        {
        }

        public EditWqmpVerifyViewModel(int waterQualityManagementPlanID, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlanID;
            WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
        }

        public virtual void UpdateModels(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
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
