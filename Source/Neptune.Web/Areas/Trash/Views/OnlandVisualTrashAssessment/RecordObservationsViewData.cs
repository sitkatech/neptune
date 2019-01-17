using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewData : OVTASectionViewData
    {
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public MapInitJson MapInitJson { get; }

        public RecordObservationsViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta, MapInitJson mapInitJson)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.RecordObservations, ovta)
        {
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(MapInitJson mapInitJson)
            {
                MapInitJson = mapInitJson;
            }

            public MapInitJson MapInitJson { get; }
        }
    }
}