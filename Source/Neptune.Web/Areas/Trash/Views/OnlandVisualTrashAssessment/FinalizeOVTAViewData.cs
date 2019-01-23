using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewData : OVTASectionViewData
    {
        public OVTASummaryMapInitJson OVTASummaryMapInitJson { get; }

        public FinalizeOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity,
            Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.FinalizeOVTA, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
        }
    }
}