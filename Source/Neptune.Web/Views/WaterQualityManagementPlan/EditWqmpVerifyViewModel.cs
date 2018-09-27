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

        public int WaterQualityManagementPlanID { get; set; }
        public int? WaterQualityManagementPlanVerifyID { get; set;  }
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }
        public int WaterQualityManagementPlanVisitStatusID { get; set; }
        //public int? WaterQualityManagementPlanDocumentID { get; set; }
        public int WaterQualityManagementPlanVerifyStatusID { get; set; }
        public string EnforcementOrFollowupActions { get; set; }


        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpVerifyViewModel()
        {
        }

        public EditWqmpVerifyViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            WaterQualityManagementPlanVerifyID = ModelObjectHelpers.NotYetAssignedID;
        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID = WaterQualityManagementPlanVerifyID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlanVerify.EnforcementOrFollowupActions = EnforcementOrFollowupActions;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
    }
}
