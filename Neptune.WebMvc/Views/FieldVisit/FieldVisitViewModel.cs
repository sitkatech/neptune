using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class FieldVisitViewModel : FormViewModel
    {
        public StepToAdvanceToEnum? StepToAdvanceTo { get; set; }
        public bool? FinalizeVisit { get; set; }
    }

    public enum StepToAdvanceToEnum
    {
        StayOnPage,
        NextPage,
        WrapUpPage
    }
}