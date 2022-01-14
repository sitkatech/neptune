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
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [Required]
        [DisplayName("WQMP Name")]
        [StringLength(Models.WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName)]
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

        [DisplayName("Date of Construction")]
        public DateTime? DateOfContruction { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.HydromodificationApplies)]
        public int? HydromodificationAppliesTypeID { get; set; }

        [DisplayName("Permit Term")]
        public int? WaterQualityManagementPlanPermitTermID { get; set; }

        [DisplayName("Hydrologic Subarea")]
        public int? HydrologicSubareaID { get; set; }

        [DisplayName("Record Number")]
        [StringLength(Models.WaterQualityManagementPlan.FieldLengths.RecordNumber)]
        public string RecordNumber { get; set; }

        [DisplayName("Recorded WQMP Area (Acres)")]
        public decimal? RecordedWQMPAreaInAcres { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TrashCaptureStatus)]
        public int? TrashCaptureStatusTypeID { get; set; }

        [DisplayName("Trash Capture Effectiveness")]
        [Range(1, 99, ErrorMessage = "The Trash Effectiveness must be between 1 and 99, if the score is 100 please select Full")]
        public int? TrashCaptureEffectiveness { get; set; }


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
            HydromodificationAppliesTypeID = waterQualityManagementPlan.HydromodificationAppliesTypeID;
            WaterQualityManagementPlanPermitTermID = waterQualityManagementPlan.WaterQualityManagementPlanPermitTermID;
            HydrologicSubareaID = waterQualityManagementPlan.HydrologicSubareaID;
            RecordNumber = waterQualityManagementPlan.RecordNumber;
            RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres;
            TrashCaptureStatusTypeID = waterQualityManagementPlan.TrashCaptureStatusTypeID;
            TrashCaptureEffectiveness = waterQualityManagementPlan.TrashCaptureEffectiveness;
        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.TrashCaptureStatusTypeID = TrashCaptureStatusTypeID.GetValueOrDefault(); // never null due to RequiredAttribute
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
            waterQualityManagementPlan.HydromodificationAppliesTypeID = HydromodificationAppliesTypeID;
            waterQualityManagementPlan.WaterQualityManagementPlanPermitTermID = WaterQualityManagementPlanPermitTermID;
            waterQualityManagementPlan.HydrologicSubareaID = HydrologicSubareaID;
            waterQualityManagementPlan.RecordNumber = RecordNumber;
            waterQualityManagementPlan.RecordedWQMPAreaInAcres = RecordedWQMPAreaInAcres;
            waterQualityManagementPlan.TrashCaptureEffectiveness = TrashCaptureEffectiveness;
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
