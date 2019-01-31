using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewData : OVTASectionViewData
    {
        public OVTASummaryMapInitJson OVTASummaryMapInitJson { get; }

        public FinalizeOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson)
            : base(currentPerson, Models.OVTASection.FinalizeOVTA, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
        }
    }
}