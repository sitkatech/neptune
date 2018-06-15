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
    public class NewViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("WQMP Name")]
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
        
        [DisplayName("Approval Date")]
        public DateTime? ApprovalDate { get; set; }

        [DisplayName("Name")]
        public string MaintenanceContactName { get; set; }

        [DisplayName("Organization")]
        public string MaintenanceContactOrganization { get; set; }

        [DisplayName("Address Line 1")]
        public string MaintenanceContactAddress1 { get; set; }

        [DisplayName("Address Line 2")]
        public string MaintenanceContactAddress2 { get; set; }

        [DisplayName("City")]
        public string MaintenanceContactCity { get; set; }

        [DisplayName("State")]
        public string MaintenanceContactState { get; set; }

        [DisplayName("Zip")]
        public string MaintenanceContactZip { get; set; }

        [DisplayName("Phone")]
        public string MaintenanceContactPhone { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewViewModel()
        {
        }

        public NewViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            WaterQualityManagementPlanName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            StormwaterJurisdictionID = waterQualityManagementPlan.StormwaterJurisdictionID;
            WaterQualityManagementPlanPriorityID = waterQualityManagementPlan.WaterQualityManagementPlanPriorityID;
            WaterQualityManagementPlanStatusID = waterQualityManagementPlan.WaterQualityManagementPlanStatusID;
            WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlan.WaterQualityManagementPlanDevelopmentTypeID;
            WaterQualityManagementPlanLandUseID = waterQualityManagementPlan.WaterQualityManagementPlanLandUseID;
            ApprovalDate = waterQualityManagementPlan.ApprovalDate;
            MaintenanceContactName = waterQualityManagementPlan.MaintenanceContactName;
            MaintenanceContactOrganization = waterQualityManagementPlan.MaintenanceContactOrganization;
            MaintenanceContactAddress1 = waterQualityManagementPlan.MaintenanceContactAddress1;
            MaintenanceContactAddress2 = waterQualityManagementPlan.MaintenanceContactAddress2;
            MaintenanceContactCity = waterQualityManagementPlan.MaintenanceContactCity;
            MaintenanceContactState = waterQualityManagementPlan.MaintenanceContactState;
            MaintenanceContactZip = waterQualityManagementPlan.MaintenanceContactZip;
            MaintenanceContactPhone = waterQualityManagementPlan.MaintenanceContactPhone;
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
            waterQualityManagementPlan.ApprovalDate = ApprovalDate;
            waterQualityManagementPlan.MaintenanceContactName = MaintenanceContactName;
            waterQualityManagementPlan.MaintenanceContactOrganization = MaintenanceContactOrganization;
            waterQualityManagementPlan.MaintenanceContactAddress1 = MaintenanceContactAddress1;
            waterQualityManagementPlan.MaintenanceContactAddress2 = MaintenanceContactAddress2;
            waterQualityManagementPlan.MaintenanceContactCity = MaintenanceContactCity;
            waterQualityManagementPlan.MaintenanceContactState = MaintenanceContactState;
            waterQualityManagementPlan.MaintenanceContactZip = MaintenanceContactZip;
            waterQualityManagementPlan.MaintenanceContactPhone = MaintenanceContactPhone;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Any(x =>
                x.WaterQualityManagementPlanName == WaterQualityManagementPlanName))
            {
                yield return new SitkaValidationResult<NewViewModel, string>("Name is already in use.", m => m.WaterQualityManagementPlanName);
            }
        }
    }
}
