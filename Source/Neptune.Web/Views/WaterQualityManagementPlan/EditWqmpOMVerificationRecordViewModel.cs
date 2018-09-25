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
    public class EditWqmpOMVerificationRecordViewModel : FormViewModel, IValidatableObject
    {
        

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpOMVerificationRecordViewModel()
        {
        }

        public EditWqmpOMVerificationRecordViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {

        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
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
