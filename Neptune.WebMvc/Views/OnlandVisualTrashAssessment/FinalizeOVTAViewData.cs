using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewData : OVTASectionViewData
    {
        public IEnumerable<SelectListItem> Scores { get; }
        public Person ConductedByPerson { get; }
        public StormwaterJurisdiction Jurisdiction { get; }
        public string CreatedDate { get; }
        public string ScoreDescriptionsUrl { get; }
        public string AssessmentAreaDescription { get; set; }

        public bool IsProgressAssessment { get; set; }

        public TrashAssessmentSummaryMapViewData TrashAssessmentSummaryMapViewData { get; }
        public IEnumerable<PreliminarySourceIdentificationType> PreliminarySourceIdentificationTypeOthers { get; }

        public FinalizeOVTAViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson, IEnumerable<SelectListItem> scores, string geoServerUrl, ICollection<OnlandVisualTrashAssessmentObservation> onlandVisualTrashAssessmentObservations)
            : base(httpContext, linkGenerator, currentPerson, webConfiguration, EFModels.Entities.OVTASection.FinalizeOVTA, ovta)
        {
            Scores = scores;
            ConductedByPerson = ovta.CreatedByPerson;
            Jurisdiction = ovta.StormwaterJurisdiction;
            CreatedDate = ovta.CreatedDate.ToShortDateString();
            IsProgressAssessment = ovta.IsProgressAssessment;
            AssessmentAreaDescription = ovta.OnlandVisualTrashAssessmentArea?.AssessmentAreaDescription ??
                                        ovta.DraftAreaDescription;
            ScoreDescriptionsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.ScoreDescriptions());

            PreliminarySourceIdentificationTypeOthers = PreliminarySourceIdentificationType.All.Where(x => x.IsOther());

            TrashAssessmentSummaryMapViewData = new TrashAssessmentSummaryMapViewData(ovtaSummaryMapInitJson, onlandVisualTrashAssessmentObservations, geoServerUrl);
        }
    }
}