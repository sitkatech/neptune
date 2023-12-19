using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [Required]
        public int? OnlandVisualTrashAssessmentID { get; set; }
        public bool? Finalize { get; set; }

        [Required]
        [StringLength(EFModels.Entities.OnlandVisualTrashAssessmentArea.FieldLengths.OnlandVisualTrashAssessmentAreaName)]
        [DisplayName("Assessment Area Name")]
        public string AssessmentAreaName { get; set; }

        [StringLength(EFModels.Entities.OnlandVisualTrashAssessmentArea.FieldLengths.AssessmentAreaDescription)]
        [DisplayName("Assessment Area Description")]
        public string AssessmentAreaDescription { get; set; }

        [DisplayName("Assessment Date")]
        public DateTime AssessmentDate { get; set; }

        [StringLength(EFModels.Entities.OnlandVisualTrashAssessment.FieldLengths.Notes)]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.OnlandVisualTrashAssessmentNotes)]
        public string Notes { get; set; }

        [Required]
        [DisplayName("Assessment Score")]
        public int? ScoreID { get; set; }

        [Required]
        public int? StormwaterJurisdictionID { get; set; }

        [DisplayName("Assessment Type")]
        public bool IsProgressAssessment { get; set; }

        public List<PreliminarySourceIdentificationSimple> PreliminarySourceIdentifications { get; set; }

        public int? AssessmentAreaID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public FinalizeOVTAViewModel()
        {

        }

        public FinalizeOVTAViewModel(EFModels.Entities.OnlandVisualTrashAssessment ovta)
        {
            AssessmentAreaName = ovta.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName ?? ovta.DraftAreaName;
            AssessmentAreaDescription = ovta.OnlandVisualTrashAssessmentArea?.AssessmentAreaDescription ??
                                        ovta.DraftAreaDescription;
            ScoreID = ovta.OnlandVisualTrashAssessmentScoreID;
            Notes = ovta.Notes;
            StormwaterJurisdictionID = ovta.StormwaterJurisdictionID;
            AssessmentAreaID = ovta.OnlandVisualTrashAssessmentAreaID;
            PreliminarySourceIdentifications = ovta.GetPreliminarySourceIdentificationSimples();
            OnlandVisualTrashAssessmentID = ovta.OnlandVisualTrashAssessmentID;
            AssessmentDate = ovta.CompletedDate ??  DateTime.Now;
            IsProgressAssessment = ovta.IsProgressAssessment;

        }

        public async Task UpdateModel(NeptuneDbContext dbContext,
            EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            DbSet<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> allOnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes)
        {
            if (Finalize.GetValueOrDefault())
            {
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = ScoreID;
                onlandVisualTrashAssessment.Notes = Notes;
                onlandVisualTrashAssessment.CompletedDate = AssessmentDate;
                onlandVisualTrashAssessment.IsProgressAssessment = IsProgressAssessment;

                // create the assessment area
                if (onlandVisualTrashAssessment.AssessingNewArea.GetValueOrDefault())
                {

                    var onlandVisualTrashAssessmentAreaGeometry2771 = onlandVisualTrashAssessment.DraftGeometry;

                    var onlandVisualTrashAssessmentArea = new Neptune.EFModels.Entities.OnlandVisualTrashAssessmentArea
                    {
                        OnlandVisualTrashAssessmentAreaName = AssessmentAreaName,
                        StormwaterJurisdictionID = onlandVisualTrashAssessment.StormwaterJurisdictionID,
                        OnlandVisualTrashAssessmentAreaGeometry = onlandVisualTrashAssessmentAreaGeometry2771,
                        OnlandVisualTrashAssessmentAreaGeometry4326 = onlandVisualTrashAssessmentAreaGeometry2771.ProjectTo4326()
                    };

                    await dbContext.SaveChangesAsync();

                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID =
                        onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID;
                    onlandVisualTrashAssessment.DraftGeometry = null;
                    onlandVisualTrashAssessment.DraftAreaDescription = null;
                }

                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentStatusID =
                    OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID;

                await dbContext.SaveChangesAsync();

                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.AssessmentAreaDescription =
                    AssessmentAreaDescription;

                var onlandVisualTrashAssessments = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(dbContext, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID.Value);
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScoreID =
                    OnlandVisualTrashAssessmentAreaModelExtensions.CalculateScoreFromBackingData(onlandVisualTrashAssessments, false)?
                        .OnlandVisualTrashAssessmentScoreID;

                if (IsProgressAssessment)
                {
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea
                            .OnlandVisualTrashAssessmentProgressScoreID =
                        onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID;
                }

                if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.TransectLine == null && onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.Count >= 2)
                {
                    var transect = onlandVisualTrashAssessment.GetTransect();
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea.TransectLine = transect;
                    onlandVisualTrashAssessment.IsTransectBackingAssessment = true;

                    var transectBackingAssessment = 
                        OnlandVisualTrashAssessments.GetTransectBackingAssessment(dbContext, onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID);
                    if (transectBackingAssessment != null)
                    {
                        transectBackingAssessment.IsTransectBackingAssessment = false;
                    }
                }
            }
            else
            {
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentScoreID = ScoreID;
                onlandVisualTrashAssessment.Notes = Notes;
                onlandVisualTrashAssessment.DraftAreaName = AssessmentAreaName;
                onlandVisualTrashAssessment.DraftAreaDescription = AssessmentAreaDescription;
            }

            var onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToUpdate =
                PreliminarySourceIdentifications.Where(x => x.Has).Select(x =>
                    new OnlandVisualTrashAssessmentPreliminarySourceIdentificationType
                    {
                        OnlandVisualTrashAssessmentID = OnlandVisualTrashAssessmentID.GetValueOrDefault(),
                        PreliminarySourceIdentificationTypeID =
                            x.PreliminarySourceIdentificationTypeID.GetValueOrDefault(),
                        ExplanationIfTypeIsOther = x.ExplanationIfTypeIsOther
                    }).ToList();

            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Merge(onlandVisualTrashAssessmentPreliminarySourceIdentificationTypesToUpdate,
                allOnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes,
                (z,w)=>z.OnlandVisualTrashAssessmentID == w.OnlandVisualTrashAssessmentID && z.PreliminarySourceIdentificationTypeID == w.PreliminarySourceIdentificationTypeID,
                (z,w) => z.ExplanationIfTypeIsOther = w.ExplanationIfTypeIsOther
                );

            // bug?: why are we nulling these unconditionally?
            onlandVisualTrashAssessment.DraftAreaDescription = null;
            onlandVisualTrashAssessment.DraftAreaName = null;
            onlandVisualTrashAssessment.DraftGeometry = null;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var assessmentAreaID = AssessmentAreaID.GetValueOrDefault();
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            if (dbContext.OnlandVisualTrashAssessmentAreas.AsNoTracking().Where(x => x.OnlandVisualTrashAssessmentAreaID != assessmentAreaID).Any(x => x.OnlandVisualTrashAssessmentAreaName == AssessmentAreaName && x.StormwaterJurisdictionID == StormwaterJurisdictionID))
            {
                yield return new SitkaValidationResult<FinalizeOVTAViewModel, string>(
                    "There is already an Assessment Area with this name in the selected jurisdiction. Please choose another name",
                    m => m.AssessmentAreaName);
            }
        }
    }

    public class PreliminarySourceIdentificationSimple: IValidatableObject
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

        [StringLength(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType.FieldLengths.ExplanationIfTypeIsOther)]
        public string ExplanationIfTypeIsOther { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Has && string.IsNullOrWhiteSpace(ExplanationIfTypeIsOther))
            {
                if (PreliminarySourceIdentificationType.AllLookupDictionary[PreliminarySourceIdentificationTypeID.GetValueOrDefault()].IsOther())
                {
                    yield return new ValidationResult(
                        "You must provide an explanation if choosing \"Other\" as a Preliminary Source Identification.");
                }
            }
        }
    }
}