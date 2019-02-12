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
        public IEnumerable<SelectListItem> Scores { get; set; }
        public Person ConductedByPerson { get; set; }
        public StormwaterJurisdiction Jurisdiction { get; set; }

        public FinalizeOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson, IEnumerable<SelectListItem> scores)
            : base(currentPerson, Models.OVTASection.FinalizeOVTA, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            Scores = scores;
            ConductedByPerson = ovta.CreatedByPerson;
            Jurisdiction = ovta.StormwaterJurisdiction;
        }
    }
}