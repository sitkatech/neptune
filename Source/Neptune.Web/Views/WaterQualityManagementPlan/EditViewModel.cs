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
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [Required]
        [DisplayName("WQMP Name")]
        [MaxLength(Models.WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName)]
        public string WaterQualityManagementPlanName { get; set; }

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

        [DisplayName("Date of Contruction")]
        public DateTime? DateOfContruction { get; set; }

        [DisplayName("Hydromodification Applies")]
        public bool? HydromodificationApplies { get; set; }

        [DisplayName("Permit Term")]
        public int? WaterQualityManagementPlanPermitTermID { get; set; }

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
            DateOfContruction = waterQualityManagementPlan.DateOfContruction;
            HydromodificationApplies = waterQualityManagementPlan.HydromodificationApplies;
            WaterQualityManagementPlanPermitTermID = waterQualityManagementPlan.WaterQualityManagementPlanPermitTermID;
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.WaterQualityManagementPlanName = WaterQualityManagementPlanName;
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
            waterQualityManagementPlan.DateOfContruction = DateOfContruction;
            waterQualityManagementPlan.HydromodificationApplies = HydromodificationApplies;
            waterQualityManagementPlan.WaterQualityManagementPlanPermitTermID = WaterQualityManagementPlanPermitTermID;
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
