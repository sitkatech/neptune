using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class NewViewModel : EditLocationViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(EFModels.Entities.TreatmentBMP.FieldLengths.TreatmentBMPName)]
        public string TreatmentBMPName { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.Jurisdiction)]
        public int StormwaterJurisdictionID { get; set; }

        [Required(ErrorMessage = "Choose a BMP Type")]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TreatmentBMPType)]
        public int TreatmentBMPTypeID { get; set; }

        [DisplayName("Notes")]
        [StringLength(EFModels.Entities.TreatmentBMP.FieldLengths.Notes)]
        public string Notes { get; set; }

        [DisplayName("ID in System of Record")]
        [StringLength(EFModels.Entities.TreatmentBMP.FieldLengths.SystemOfRecordID)]
        public string SystemOfRecordID { get; set; }

        [DisplayName("Owner")]
        public int? OwnerOrganizationID { get; set; }

        [DisplayName("Year Built")]
        public int? YearBuilt { get; set; }

        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.RequiredLifespanOfInstallation)]
        public int? TreatmentBMPLifespanTypeID { get; set; }

        [DisplayName("Lifespan End Date")]
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.RequiredFieldVisitsPerYear)]
        [Range(0, Int32.MaxValue, ErrorMessage = "Required Field Visits Per Year cannot be negative")]
        public int? RequiredFieldVisitsPerYear { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.RequiredPostStormFieldVisitsPerYear)]
        [Range(0, Int32.MaxValue, ErrorMessage = "Required Post Storm Field Visits Per Year cannot be negative")]
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TrashCaptureStatus)]
        public int? TrashCaptureStatusTypeID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.SizingBasis)]
        public int? SizingBasisTypeID { get; set; }

        [DisplayName("Trash Capture Effectiveness")]
        [Range(1, 99, ErrorMessage = "The Trash Effectiveness must be between 1 and 99, if the score is 100 please select Full")]
        public int? TrashCaptureEffectiveness { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewViewModel()
        {
        }

        public NewViewModel(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            TreatmentBMPName = treatmentBMP.TreatmentBMPName;
            StormwaterJurisdictionID = treatmentBMP.StormwaterJurisdictionID;
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID;
            Notes = treatmentBMP.Notes;
            SystemOfRecordID = treatmentBMP.SystemOfRecordID;
            OwnerOrganizationID = treatmentBMP.OwnerOrganizationID;
            YearBuilt = treatmentBMP.YearBuilt;
            WaterQualityManagementPlanID = treatmentBMP.WaterQualityManagementPlanID;
            TreatmentBMPLifespanTypeID = treatmentBMP.TreatmentBMPLifespanTypeID;
            TreatmentBMPLifespanEndDate = treatmentBMP.TreatmentBMPLifespanEndDate;
            RequiredFieldVisitsPerYear = treatmentBMP.RequiredFieldVisitsPerYear;
            RequiredPostStormFieldVisitsPerYear = treatmentBMP.RequiredPostStormFieldVisitsPerYear;
            TrashCaptureStatusTypeID = treatmentBMP.TrashCaptureStatusTypeID;
            SizingBasisTypeID = treatmentBMP.SizingBasisTypeID;
            TrashCaptureEffectiveness = treatmentBMP.TrashCaptureEffectiveness;
        }

        public override void UpdateModel(NeptuneDbContext dbContext, EFModels.Entities.TreatmentBMP treatmentBMP,
            Person currentPerson)
        {
            SetTreatmentBMPLocationAndPointInPolygonData(dbContext, treatmentBMP);

            treatmentBMP.TreatmentBMPName = TreatmentBMPName;
            treatmentBMP.Notes = Notes;
            treatmentBMP.RequiredFieldVisitsPerYear = RequiredFieldVisitsPerYear;
            treatmentBMP.RequiredPostStormFieldVisitsPerYear = RequiredPostStormFieldVisitsPerYear;
            treatmentBMP.TrashCaptureStatusTypeID = TrashCaptureStatusTypeID.GetValueOrDefault(); // will never be null due to RequiredAttribute
            treatmentBMP.SizingBasisTypeID = SizingBasisTypeID.GetValueOrDefault(); // will never be null due to RequiredAttribute

            treatmentBMP.StormwaterJurisdictionID = StormwaterJurisdictionID;
            treatmentBMP.TreatmentBMPTypeID = TreatmentBMPTypeID;

            var treatmentBmpType = dbContext.TreatmentBMPTypes.Single(x =>
                x.TreatmentBMPTypeID == TreatmentBMPTypeID);
            foreach (var a in treatmentBmpType.TreatmentBMPTypeAssessmentObservationTypes.Where(x =>
                         x.TreatmentBMPAssessmentObservationType.GetHasBenchmarkAndThreshold() &&
                         x.DefaultThresholdValue.HasValue && x.DefaultBenchmarkValue.HasValue))
            {
                var treatmentBmpBenchmarkAndThreshold =
                    new EFModels.Entities.TreatmentBMPBenchmarkAndThreshold()
                    {
                        TreatmentBMP = treatmentBMP,
                        TreatmentBMPTypeAssessmentObservationType = a,
                        TreatmentBMPType = treatmentBmpType,
                        TreatmentBMPAssessmentObservationType = a.TreatmentBMPAssessmentObservationType,
                        BenchmarkValue = a.DefaultBenchmarkValue ?? 0,
                        ThresholdValue = a.DefaultThresholdValue ?? 0
                    };
                treatmentBMP.TreatmentBMPBenchmarkAndThresholds.Add(treatmentBmpBenchmarkAndThreshold);
            }

            treatmentBMP.SystemOfRecordID = SystemOfRecordID;
            if (OwnerOrganizationID.HasValue)
            {
                treatmentBMP.OwnerOrganizationID = OwnerOrganizationID.Value;
            }
            else
            {
                var stormwaterJurisdiction = StormwaterJurisdictions.GetByID(dbContext, treatmentBMP.StormwaterJurisdictionID);
                treatmentBMP.OwnerOrganizationID = stormwaterJurisdiction.OrganizationID;
            }

            treatmentBMP.YearBuilt = YearBuilt;
            treatmentBMP.WaterQualityManagementPlanID = WaterQualityManagementPlanID;

            treatmentBMP.TreatmentBMPLifespanTypeID = TreatmentBMPLifespanTypeID;
            treatmentBMP.TreatmentBMPLifespanEndDate = TreatmentBMPLifespanTypeID == TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID ? TreatmentBMPLifespanEndDate : null;
            treatmentBMP.TrashCaptureEffectiveness = TrashCaptureStatusTypeID == TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID ? TrashCaptureEffectiveness : null;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            var treatmentBmPsWithSameName = dbContext.TreatmentBMPs.AsNoTracking().Where(x => x.TreatmentBMPName == TreatmentBMPName);
            if (treatmentBmPsWithSameName.Any(x => x.TreatmentBMPID != TreatmentBMPID))
            {
                yield return new SitkaValidationResult<NewViewModel, string>("A BMP with this name already exists.", x => x.TreatmentBMPName);
            }

            if (TreatmentBMPLifespanTypeID == TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID &&
                !TreatmentBMPLifespanEndDate.HasValue)
            {
                yield return new SitkaValidationResult<EditViewModel, DateTime?>(
                    "The Lifespan End Date must be set if the Lifespan Type is Fixed End Date.",
                    x => x.TreatmentBMPLifespanEndDate);
            }
        }
    }
}
