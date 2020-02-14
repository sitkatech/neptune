using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class NewViewModel : FormViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}