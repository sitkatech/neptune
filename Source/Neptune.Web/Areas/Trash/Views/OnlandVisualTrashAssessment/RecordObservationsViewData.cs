using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewData : OVTASectionViewData
    {
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public OVTAObservationsMapInitJson MapInitJson { get; }

        public RecordObservationsViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta, OVTAObservationsMapInitJson mapInitJson)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.RecordObservations, ovta)
        {
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(OVTAObservationsMapInitJson mapInitJson)
            {
                MapInitJson = mapInitJson;
            }

            public OVTAObservationsMapInitJson MapInitJson { get; }
        }
    }
}