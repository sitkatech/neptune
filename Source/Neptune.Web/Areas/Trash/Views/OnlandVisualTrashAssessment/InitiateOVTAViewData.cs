using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewData : OVTASectionViewData
    {
        public IEnumerable<SelectListItem> Jurisdictions { get; }
        public SelectOVTAAreaMapInitJson MapInitJson { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }

        public InitiateOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta, IEnumerable<SelectListItem> jurisdictions, SelectOVTAAreaMapInitJson mapInitJson)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.InitiateOVTA, ovta)
        {
            Jurisdictions = jurisdictions;
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(SelectOVTAAreaMapInitJson mapInitJson)
            {
                MapInitJson = mapInitJson;
            }

            public SelectOVTAAreaMapInitJson MapInitJson { get; }
        }
    }
}
