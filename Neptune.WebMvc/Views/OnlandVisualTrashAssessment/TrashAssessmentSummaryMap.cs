using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
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
        public IEnumerable<OnlandVisualTrashAssessmentObservation> OnlandVisualTrashAssessmentObservations { get; }
        public string GeoServerUrl { get; }
    }
}
