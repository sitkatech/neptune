using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment.MapInitJson;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public abstract class TrashAssessmentSummaryMap : TypedWebPartialViewPage<TrashAssessmentSummaryMapViewData>
    {
        public static void RenderPartialView(IHtmlHelper html, TrashAssessmentSummaryMapViewData viewData)
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
