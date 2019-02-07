using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.Shared;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsViewData: OVTASectionViewData
    {
        public AddOrRemoveParcelsViewData(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson) : base(currentPerson, ovtaSection, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(ovtaSummaryMapInitJson);
            RefreshUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(
                    x => x.RefreshParcels(ovta));
            OfferRefresh = ovta.OnlandVisualTrashAssessmentArea != null; // todo: staging?
        }

        public OVTASummaryMapInitJson OVTASummaryMapInitJson { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; set; }
        public string RefreshUrl { get; set; }
        public bool OfferRefresh { get; set; }

        public class ViewDataForAngularClass: TrashModuleMapViewDataForAngularBaseClass
        {
            public ViewDataForAngularClass(OVTASummaryMapInitJson mapInitJson)
            {
                MapInitJson = mapInitJson;
            }

            public OVTASummaryMapInitJson MapInitJson { get; }
        }
    }
}
