using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewData : OVTASectionViewData
    {
        public IEnumerable<SelectListItem> Jurisdictions { get; }
        public SelectOVTAAreaMapInitJson MapInitJson { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }

        public InitiateOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, IEnumerable<SelectListItem> jurisdictions,
            SelectOVTAAreaMapInitJson mapInitJson,
            IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas)
            : base(currentPerson, Models.OVTASection.InitiateOVTA, ovta)
        {
            Jurisdictions = jurisdictions;
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, onlandVisualTrashAssessmentAreas);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(SelectOVTAAreaMapInitJson mapInitJson, IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas)
            {
                MapInitJson = mapInitJson;
                OnlandVisualTrashAssessmentAreas = onlandVisualTrashAssessmentAreas.Select(x =>
                    new OnlandVisualTrashAssessmentAreaSimple
                    {
                        OnlandVisualTrashAssessmentAreaName = x.OnlandVisualTrashAssessmentAreaName,
                        OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID
                    });
            }

            public SelectOVTAAreaMapInitJson MapInitJson { get; }

            public IEnumerable<OnlandVisualTrashAssessmentAreaSimple> OnlandVisualTrashAssessmentAreas { get; }
        }
    }

    public class OnlandVisualTrashAssessmentAreaSimple
    {
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
    }
}
