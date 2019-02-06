using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsViewData: OVTASectionViewData
    {
        public AddOrRemoveParcelsViewData(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson) : base(currentPerson, ovtaSection, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(ovtaSummaryMapInitJson);
        }

        public OVTASummaryMapInitJson OVTASummaryMapInitJson { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; set; }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(OVTASummaryMapInitJson mapInitJson)
            {
                MapInitJson = mapInitJson;
            }

            public OVTASummaryMapInitJson MapInitJson { get; }
        }
    }
}
