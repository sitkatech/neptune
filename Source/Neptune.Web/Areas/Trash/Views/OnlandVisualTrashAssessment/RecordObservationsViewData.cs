using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewData : OVTASectionViewData
    {


        public RecordObservationsViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.RecordObservations)
        {

        }
    }
}