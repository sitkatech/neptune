using LtInfo.Common.Models;

namespace Neptune.Web.Views.FieldVisit
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