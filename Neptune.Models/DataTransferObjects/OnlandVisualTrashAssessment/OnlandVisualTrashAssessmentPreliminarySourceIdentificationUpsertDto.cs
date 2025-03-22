namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentPreliminarySourceIdentificationUpsertDto
{
    public bool Selected { get; set; }
    public int PreliminarySourceIdentificationTypeID { get; set; }
    public string PreliminarySourceIdentificationTypeName { get; set; }
    public int PreliminarySourceIdentificationCategoryID { get; set; }
    public bool IsOther { get; set; }
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