using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
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
        }
    }
}