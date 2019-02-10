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
        public AddOrRemoveParcelsMapIntJson OVTASummaryMapInitJson { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; set; }
        public string RefreshUrl { get; set; }
        public bool OfferRefresh { get; set; }

        public AddOrRemoveParcelsViewData(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta, AddOrRemoveParcelsMapIntJson ovtaSummaryMapInitJson) : base(currentPerson, ovtaSection, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(ovtaSummaryMapInitJson);
            RefreshUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(
                    x => x.RefreshParcels(ovta));
            OfferRefresh = ovta.DraftGeometry != null;
        }

        public class ViewDataForAngularClass: TrashModuleMapViewDataForAngularBaseClass
        {
            public ViewDataForAngularClass(AddOrRemoveParcelsMapIntJson mapInitJson)
            {
                MapInitJson = mapInitJson;
            }

            public AddOrRemoveParcelsMapIntJson MapInitJson { get; }
        }
    }
}
