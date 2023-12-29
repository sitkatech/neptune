using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [Required]
        [DisplayName("WQMP Name")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName)]
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
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactName)]
        public string MaintenanceContactName { get; set; }

        [DisplayName("Organization")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactOrganization)]
        public string MaintenanceContactOrganization { get; set; }

        [DisplayName("Address Line 1")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactAddress1)]
        public string MaintenanceContactAddress1 { get; set; }

        [DisplayName("Address Line 2")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactAddress2)]
        public string MaintenanceContactAddress2 { get; set; }

        [DisplayName("City")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactCity)]
        public string MaintenanceContactCity { get; set; }

        [DisplayName("State")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactState)]
        public string MaintenanceContactState { get; set; }

        [DisplayName("Zip")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactZip)]
        public string MaintenanceContactZip { get; set; }

        [DisplayName("Phone")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.MaintenanceContactPhone)]
        public string MaintenanceContactPhone { get; set; }

        [DisplayName("Date of Construction")]
        public DateTime? DateOfConstruction { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.HydromodificationApplies)]
        public int? HydromodificationAppliesTypeID { get; set; }

        [DisplayName("Permit Term")]
        public int? WaterQualityManagementPlanPermitTermID { get; set; }

        [DisplayName("Hydrologic Subarea")]
        public int? HydrologicSubareaID { get; set; }

        [DisplayName("Record Number")]
        [StringLength(EFModels.Entities.WaterQualityManagementPlan.FieldLengths.RecordNumber)]
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

        public EditViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
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
            DateOfConstruction = waterQualityManagementPlan.DateOfContruction;
            HydromodificationAppliesTypeID = waterQualityManagementPlan.HydromodificationAppliesTypeID;
            WaterQualityManagementPlanPermitTermID = waterQualityManagementPlan.WaterQualityManagementPlanPermitTermID;
            HydrologicSubareaID = waterQualityManagementPlan.HydrologicSubareaID;
            RecordNumber = waterQualityManagementPlan.RecordNumber;
            RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres;
            TrashCaptureStatusTypeID = waterQualityManagementPlan.TrashCaptureStatusTypeID;
            TrashCaptureEffectiveness = waterQualityManagementPlan.TrashCaptureEffectiveness;
        }

        public virtual void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
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
            waterQualityManagementPlan.DateOfContruction = DateOfConstruction;
            waterQualityManagementPlan.HydromodificationAppliesTypeID = HydromodificationAppliesTypeID;
            waterQualityManagementPlan.WaterQualityManagementPlanPermitTermID = WaterQualityManagementPlanPermitTermID;
            waterQualityManagementPlan.HydrologicSubareaID = HydrologicSubareaID;
            waterQualityManagementPlan.RecordNumber = RecordNumber;
            waterQualityManagementPlan.RecordedWQMPAreaInAcres = RecordedWQMPAreaInAcres;
            waterQualityManagementPlan.TrashCaptureEffectiveness = TrashCaptureEffectiveness;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            if (dbContext.WaterQualityManagementPlans.AsNoTracking().Any(x =>
                    x.WaterQualityManagementPlanName == WaterQualityManagementPlanName &&
                    x.WaterQualityManagementPlanID != WaterQualityManagementPlanID))
            {
                yield return new SitkaValidationResult<EditViewModel, string>("Name is already in use.", m => m.WaterQualityManagementPlanName);
            }
        }
    }
}
