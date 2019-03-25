using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewData : OVTASectionViewData
    {
        public IEnumerable<SelectListItem> Scores { get; }
        public Person ConductedByPerson { get; }
        public StormwaterJurisdiction Jurisdiction { get; }
        public string CreatedDate { get; }
        public string ScoreDescriptionsUrl { get; }

        public TrashAssessmentSummaryMapViewData TrashAssessmentSummaryMapViewData { get; }
        public IEnumerable<PreliminarySourceIdentificationType> PreliminarySourceIdentificationTypeOthers { get; }

        public FinalizeOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson, IEnumerable<SelectListItem> scores, string geoServerUrl)
            : base(currentPerson, Models.OVTASection.FinalizeOVTA, ovta)
        {
            Scores = scores;
            ConductedByPerson = ovta.CreatedByPerson;
            Jurisdiction = ovta.StormwaterJurisdiction;
            CreatedDate = ovta.CreatedDate.ToShortDateString();
            ScoreDescriptionsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.ScoreDescriptions());

            PreliminarySourceIdentificationTypeOthers = PreliminarySourceIdentificationType.All.Where(x=>x.IsOther());

            TrashAssessmentSummaryMapViewData = new TrashAssessmentSummaryMapViewData(ovtaSummaryMapInitJson, ovta.OnlandVisualTrashAssessmentObservations, geoServerUrl);
        }
    }
}