using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewData : OVTASectionViewData
    {


        public RecordObservationsViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.RecordObservations, ovta)
        {

        }
    }
}