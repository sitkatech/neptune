namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDto
{
    //// create a simple from an answer provided on an OVTA
    //public OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDto(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
    //{
    //    Has = true;
    //    PreliminarySourceIdentificationTypeID =
    //        onlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID;
    //    ExplanationIfTypeIsOther =
    //        onlandVisualTrashAssessmentPreliminarySourceIdentificationType.ExplanationIfTypeIsOther;
    //}

    //// create a simple from the platonic form
    //public OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDto(PreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
    //{
    //    Has = false;
    //    PreliminarySourceIdentificationTypeID =
    //        onlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID;
    //}

    public bool Selected { get; set; }
    public int PreliminarySourceIdentificationTypeID { get; set; }
    public string ExplanationIfTypeIsOther { get; set; }

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (Has && string.IsNullOrWhiteSpace(ExplanationIfTypeIsOther))
    //    {
    //        if (PreliminarySourceIdentificationType.AllLookupDictionary[PreliminarySourceIdentificationTypeID.GetValueOrDefault()].IsOther())
    //        {
    //            yield return new ValidationResult(
    //                "You must provide an explanation if choosing \"Other\" as a Preliminary Source Identification.");
    //        }
    //    }
    //}
}