using System.Collections.Generic;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class FinalizeOVTAViewData : OVTASectionViewData
    {
        public OVTASummaryMapInitJson OVTASummaryMapInitJson { get; }
        public IEnumerable<SelectListItem> Scores { get; set; }

        public FinalizeOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, OVTASummaryMapInitJson ovtaSummaryMapInitJson)
            : base(currentPerson, Models.OVTASection.FinalizeOVTA, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            Scores = new List<string> {"A", "B", "C", "D"}.ToSelectListWithDisabledEmptyFirstRow(x => x, x => x,
                "Choose a score");
        }
    }
}