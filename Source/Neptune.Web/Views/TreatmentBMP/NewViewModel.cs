using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class NewViewModel : EditLocationViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(Models.TreatmentBMP.FieldLengths.TreatmentBMPName)]
        public string TreatmentBMPName { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.Jurisdiction)]
        public int StormwaterJurisdictionID { get; set; }

        [Required(ErrorMessage = "Choose a BMP Type")]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentBMPType)]
        public int TreatmentBMPTypeID { get; set; }

        [DisplayName("Notes")]
        [StringLength(Models.TreatmentBMP.FieldLengths.Notes)]
        public string Notes { get; set; }

        [DisplayName("ID in System of Record")]
        [StringLength(Models.TreatmentBMP.FieldLengths.SystemOfRecordID)]
        public string SystemOfRecordID { get; set; }

        [DisplayName("Owner")]
        public int? OwnerOrganizationID { get; set; }

        [DisplayName("Year Built")]
        [Range(NeptuneDateUtilities.MinimumYear, 2050, ErrorMessage = "Please enter a valid year range")]
        public int? YearBuilt { get; set; }

        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.RequiredLifespanOfInstallation)]
        public int? TreatmentBMPLifespanTypeID { get; set; }

        [DisplayName("Lifespan End Date")]
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.RequiredFieldVisitsPerYear)]
        [Range(0, Int32.MaxValue, ErrorMessage = "Required Field Visits Per Year cannot be negative")]
        public int? RequiredFieldVisitsPerYear { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.RequiredPostStormFieldVisitsPerYear)]
        [Range(0, Int32.MaxValue, ErrorMessage = "Required Post Storm Field Visits Per Year cannot be negative")]
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TrashCaptureStatus)]
        public int? TrashCaptureStatusTypeID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.SizingBasis)]
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

        public NewViewModel(Models.TreatmentBMP treatmentBMP)
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

        public override void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            base.UpdateModel(treatmentBMP, currentPerson);

            treatmentBMP.TreatmentBMPName = TreatmentBMPName;
            treatmentBMP.Notes = Notes;
            treatmentBMP.RequiredFieldVisitsPerYear = RequiredFieldVisitsPerYear;
            treatmentBMP.RequiredPostStormFieldVisitsPerYear = RequiredPostStormFieldVisitsPerYear;
            treatmentBMP.TrashCaptureStatusTypeID = TrashCaptureStatusTypeID.GetValueOrDefault(); // will never be null due to RequiredAttribute
            treatmentBMP.SizingBasisTypeID = SizingBasisTypeID.GetValueOrDefault(); // will never be null due to RequiredAttribute

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMP.TreatmentBMPID))
            {
                treatmentBMP.StormwaterJurisdictionID = StormwaterJurisdictionID;
                treatmentBMP.TreatmentBMPTypeID = TreatmentBMPTypeID;

                var treatmentBmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x =>
                    x.TreatmentBMPTypeID == TreatmentBMPTypeID);
                foreach (var a in treatmentBmpType.TreatmentBMPTypeAssessmentObservationTypes.Where(x => x.TreatmentBMPAssessmentObservationType.GetHasBenchmarkAndThreshold() && x.DefaultThresholdValue.HasValue && x.DefaultBenchmarkValue.HasValue))
                {
                    var treatmentBmpBenchmarkAndThreshold =
                        new Models.TreatmentBMPBenchmarkAndThreshold(treatmentBMP,
                            a, treatmentBmpType,
                            a.TreatmentBMPAssessmentObservationType,
                            a.DefaultBenchmarkValue ?? 0,
                            a.DefaultThresholdValue ?? 0);
                    treatmentBMP.TreatmentBMPBenchmarkAndThresholds.Add(treatmentBmpBenchmarkAndThreshold);
                }
            }

            treatmentBMP.SystemOfRecordID = SystemOfRecordID;
            if (OwnerOrganizationID.HasValue)
            {
                treatmentBMP.OwnerOrganizationID = OwnerOrganizationID.Value;
            }
            else
            {
                var stormwaterJurisdiction =
                    HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(treatmentBMP
                        .StormwaterJurisdictionID);
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
            var treatmentBmPsWithSameName = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => x.TreatmentBMPName == TreatmentBMPName);
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
