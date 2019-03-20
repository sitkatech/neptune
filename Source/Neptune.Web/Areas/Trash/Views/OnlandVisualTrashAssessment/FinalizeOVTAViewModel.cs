using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [Required]
        public int? OnlandVisualTrashAssessmentID { get; set; }
        public bool? Finalize { get; set; }

        [Required]
        [StringLength(Models.OnlandVisualTrashAssessmentArea.FieldLengths.OnlandVisualTrashAssessmentAreaName)]
        [DisplayName("Assessment Area Name")]
        public string AssessmentAreaName { get; set; }

        [StringLength(Models.OnlandVisualTrashAssessment.FieldLengths.Notes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.OnlandVisualTrashAssessmentNotes)]
        public string Notes { get; set; }

        [Required]
        [DisplayName("Assessment Score")]
        public int? ScoreID { get; set; }

        [Required]
        public int? StormwaterJurisdictionID { get; set; }

        public List<PreliminarySourceIdentificationSimple> PreliminarySourceIdentifications { get; set; }

        public int? AssessmentAreaID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public FinalizeOVTAViewModel()
        {

        }

        public FinalizeOVTAViewModel(Models.OnlandVisualTrashAssessment ovta)
        {
            AssessmentAreaName = ovta.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName ?? ovta.DraftAreaName;
            ScoreID = ovta.OnlandVisualTrashAssessmentScoreID;
            Notes = ovta.Notes;
            StormwaterJurisdictionID = ovta.StormwaterJurisdictionID;
            AssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID;
            PreliminarySourceIdentifications = ovta.GetPreliminarySourceIdentificationSimples();
            OnlandVisualTrashAssessmentID = ovta.OnlandVisualTrashAssessmentID;

        }

        public void UpdateModel(Models.OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            ICollection<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> allOnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes)
        {
            if (Finalize.GetValueOrDefault())
            {
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = ScoreID;
                onlandVisualTrashAssessment.Notes = Notes;
                onlandVisualTrashAssessment.CompletedDate = DateTime.Now;

                // create the assessment area
                if (onlandVisualTrashAssessment.AssessingNewArea.GetValueOrDefault())
                {
                    var onlandVisualTrashAssessmentArea = new Models.OnlandVisualTrashAssessmentArea(AssessmentAreaName,
                        onlandVisualTrashAssessment.StormwaterJurisdiction, onlandVisualTrashAssessment.DraftGeometry);
                    HttpRequestStorage.DatabaseEntities.SaveChanges();

                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID =
                        onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID;
                    onlandVisualTrashAssessment.DraftGeometry = null;
                }

                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID =
                    OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID;

                HttpRequestStorage.DatabaseEntities.SaveChanges();

                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScoreID =
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.CalculateScoreFromBackingData()?
                        .OnlandVisualTrashAssessmentScoreID;
            }
            else
            {
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = ScoreID;
                onlandVisualTrashAssessment.Notes = Notes;
                onlandVisualTrashAssessment.DraftAreaName = AssessmentAreaName;
            }

            var onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToUpdate = PreliminarySourceIdentifications.Where(x => x.Has).Select(x =>
                new OnlandVisualTrashAssessmentPreliminarySourceIdentificationType(
                    OnlandVisualTrashAssessmentID.GetValueOrDefault(),
                    x.PreliminarySourceIdentificationTypeID.GetValueOrDefault())
                {
                    ExplanationIfTypeIsOther = x.ExplanationIfTypeIsOther
                }).ToList();

            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Merge(onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToUpdate,
                allOnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes,
                (z,w)=>z.OnlandVisualTrashAssessmentID == w.OnlandVisualTrashAssessmentID && z.PreliminarySourceIdentificationTypeID == w.PreliminarySourceIdentificationTypeID,
                (z,w) => z.ExplanationIfTypeIsOther = w.ExplanationIfTypeIsOther
                );
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var assessmentAreaID = AssessmentAreaID.GetValueOrDefault();

            if (HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Where(x=>x.OnlandVisualTrashAssessmentAreaID != assessmentAreaID).Any(x => x.OnlandVisualTrashAssessmentAreaName == AssessmentAreaName && x.StormwaterJurisdictionID == StormwaterJurisdictionID))
            {
                yield return new SitkaValidationResult<FinalizeOVTAViewModel, string>(
                    "There is already an Assessment Area with this name in the selected jurisdiction. Please choose another name",
                    m => m.AssessmentAreaName);
            }
        }
    }

    public class PreliminarySourceIdentificationSimple
    {
        /// <summary>
        /// needed by Model Binder
        /// </summary>
        public PreliminarySourceIdentificationSimple()
        {

        }
        // create a simple from an answer provided on an OVTA
        public PreliminarySourceIdentificationSimple(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
        {
            Has = true;
            PreliminarySourceIdentificationTypeID =
                onlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID;
            ExplanationIfTypeIsOther =
                onlandVisualTrashAssessmentPreliminarySourceIdentificationType.ExplanationIfTypeIsOther;
        }

        // create a simple from the platonic form
        public PreliminarySourceIdentificationSimple(PreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
        {
            Has = false;
            PreliminarySourceIdentificationTypeID =
                onlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID;
        }

        public bool Has { get; set; }

        [Required]
        public int? PreliminarySourceIdentificationTypeID { get; set; }

        [StringLength(Models.OnlandVisualTrashAssessmentPreliminarySourceIdentificationType.FieldLengths.ExplanationIfTypeIsOther)]
        public string ExplanationIfTypeIsOther { get; set; }
    }
}