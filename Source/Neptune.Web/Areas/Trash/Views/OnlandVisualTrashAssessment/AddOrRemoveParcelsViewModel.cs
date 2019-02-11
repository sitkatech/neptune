using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsViewModel: OnlandVisualTrashAssessmentViewModel, IValidatableObject
    {
        [DisplayName("Parcels")]
        public IEnumerable<int> ParcelIDs { get; set; }

        /// <summary>
        /// needed by ModelBinder
        /// </summary>
        public AddOrRemoveParcelsViewModel()
        {
        }

        public AddOrRemoveParcelsViewModel(IEnumerable<int> parcelIDs)
        {
            ParcelIDs = parcelIDs;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}