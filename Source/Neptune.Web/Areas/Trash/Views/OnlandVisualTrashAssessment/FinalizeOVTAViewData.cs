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
        public OVTASummaryMapInitJson OVTASummaryMapInitJson { get; }
        public IEnumerable<SelectListItem> Scores { get; }
        public Person ConductedByPerson { get; }
        public StormwaterJurisdiction Jurisdiction { get; }
        public string CreatedDate { get; }
        public string GeoServerUrl { get; }
        public string ScoreDescriptionsUrl { get; }
        public IEnumerable<PreliminarySourceIdentificationType> PreliminarySourceIdentificationTypeOthers { get; }

        public FinalizeOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson, IEnumerable<SelectListItem> scores, string geoServerUrl)
            : base(currentPerson, Models.OVTASection.FinalizeOVTA, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            Scores = scores;
            GeoServerUrl = geoServerUrl;
            ConductedByPerson = ovta.CreatedByPerson;
            Jurisdiction = ovta.StormwaterJurisdiction;
            CreatedDate = ovta.CreatedDate.ToShortDateString();
            ScoreDescriptionsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.ScoreDescriptions());

            PreliminarySourceIdentificationTypeOthers = PreliminarySourceIdentificationType.All.Where(x=>x.IsOther());
        }
    }
}