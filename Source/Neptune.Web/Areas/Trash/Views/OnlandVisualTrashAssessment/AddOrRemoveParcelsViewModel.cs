using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsViewModel: OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [DisplayName("Parcels")]
        public List<int> ParcelIDs { get; set; }

        /// <summary>
        /// needed by ModelBinder
        /// </summary>
        public AddOrRemoveParcelsViewModel()
        {
        }

        public AddOrRemoveParcelsViewModel(List<int> parcelIDs)
        {
            ParcelIDs = parcelIDs;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ParcelIDs == null || ParcelIDs.Count == 0)
            {
                yield return new ValidationResult("You must select at least one parcel");
            }
            yield break;
        }
    }
}