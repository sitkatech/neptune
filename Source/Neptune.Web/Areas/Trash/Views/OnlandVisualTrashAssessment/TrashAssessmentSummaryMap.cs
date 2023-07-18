using System.Collections.Generic;
using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public abstract class TrashAssessmentSummaryMap : TypedWebPartialViewPage<TrashAssessmentSummaryMapViewData>
    {
        public static void RenderPartialView(HtmlHelper html, TrashAssessmentSummaryMapViewData viewData)
        {
            html.RenderRazorSitkaPartial<TrashAssessmentSummaryMap, TrashAssessmentSummaryMapViewData>(viewData);
        }
    }

    public class TrashAssessmentSummaryMapViewData
    {
        public TrashAssessmentSummaryMapViewData(OVTASummaryMapInitJson ovtaSummaryMapInitJson, IEnumerable<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations, string geoServerUrl)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            OnlandVisualTrashAssessmentObservations = onlandVisualTrashAssessmentObservations;
            GeoServerUrl = geoServerUrl;
        }

        public OVTASummaryMapInitJson OVTASummaryMapInitJson { get; }
        public IEnumerable<OnlandVisualTrashAssessmentObservation> OnlandVisualTrashAssessmentObservations
        { get; }
        public string GeoServerUrl { get; }
    }
}
