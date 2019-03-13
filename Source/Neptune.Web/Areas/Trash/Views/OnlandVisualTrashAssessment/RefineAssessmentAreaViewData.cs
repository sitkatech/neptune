using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewData : OVTASectionViewData
    {
        public string MapFormID { get;  }
        public RefineAssessmentAreaMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get;  }

        public RefineAssessmentAreaViewData(Person currentPerson, Models.OVTASection ovtaSection, Models.OnlandVisualTrashAssessment ovta, RefineAssessmentAreaMapInitJson mapInitJson, string geoServerUrl) : base(currentPerson, ovtaSection, ovta)
        {
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            MapFormID = "refineAssessmentAreaForm";
        }
    }
}