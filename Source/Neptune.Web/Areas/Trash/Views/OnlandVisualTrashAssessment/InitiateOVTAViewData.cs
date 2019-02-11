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
        public StormwaterJurisdiction DefaultJurisdiction { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }

        public InitiateOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, IEnumerable<SelectListItem> jurisdictions,
            SelectOVTAAreaMapInitJson mapInitJson,
            IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
            StormwaterJurisdiction defaultJurisdiction)
            : base(currentPerson, Models.OVTASection.InitiateOVTA, ovta)
        {
            Jurisdictions = jurisdictions;
            MapInitJson = mapInitJson;
            DefaultJurisdiction = defaultJurisdiction;
            var selectedOVTAArea = ovta.OnlandVisualTrashAssessmentArea != null
                ? new OnlandVisualTrashAssessmentAreaSimple
                {
                    OnlandVisualTrashAssessmentAreaName =
                        ovta.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName,
                    OnlandVisualTrashAssessmentAreaID =
                        ovta.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
                    StormwaterJurisdictionID = ovta.OnlandVisualTrashAssessmentArea.StormwaterJurisdictionID
                }
                : null;
            var useDefaultJurisdiction = defaultJurisdiction != null;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, onlandVisualTrashAssessmentAreas, useDefaultJurisdiction, selectedOVTAArea);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(SelectOVTAAreaMapInitJson mapInitJson,
                IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
                bool useDefaultJurisdiction, OnlandVisualTrashAssessmentAreaSimple selectedOVTAArea)
            {
                MapInitJson = mapInitJson;
                UseDefaultJurisdiction = useDefaultJurisdiction;
                OnlandVisualTrashAssessmentAreas = onlandVisualTrashAssessmentAreas.Select(x =>
                    new OnlandVisualTrashAssessmentAreaSimple
                    {
                        OnlandVisualTrashAssessmentAreaName = x.OnlandVisualTrashAssessmentAreaName,
                        OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID,
                        StormwaterJurisdictionID = x.StormwaterJurisdictionID
                    });
                SelectedOnlandVisualTrashAssessmentArea = selectedOVTAArea;
            }

            public SelectOVTAAreaMapInitJson MapInitJson { get; }
            public bool UseDefaultJurisdiction { get; }
            public IEnumerable<OnlandVisualTrashAssessmentAreaSimple> OnlandVisualTrashAssessmentAreas { get; }
            public OnlandVisualTrashAssessmentAreaSimple SelectedOnlandVisualTrashAssessmentArea { get; }
        }
    }
}
