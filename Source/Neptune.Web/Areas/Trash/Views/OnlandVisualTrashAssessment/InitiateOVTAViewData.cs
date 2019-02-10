using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewData : OVTASectionViewData
    {
        public List<StormwaterJurisdiction> Jurisdictions { get; }
        public SelectOVTAAreaMapInitJson MapInitJson { get; }
        public StormwaterJurisdiction DefaultJurisdiction { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }

        public InitiateOVTAViewData(Person currentPerson,
            Models.OnlandVisualTrashAssessment ovta, List<StormwaterJurisdiction> jurisdictions,
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
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, onlandVisualTrashAssessmentAreas, useDefaultJurisdiction, selectedOVTAArea, jurisdictions);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(SelectOVTAAreaMapInitJson mapInitJson,
                IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
                bool useDefaultJurisdiction, OnlandVisualTrashAssessmentAreaSimple selectedOVTAArea,
                List<StormwaterJurisdiction> jurisdictions)
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
                StormwaterJurisdictions = jurisdictions.Select(x=> new StormwaterJurisdictionSimple(x)).ToList();
            }

            public SelectOVTAAreaMapInitJson MapInitJson { get; }
            public bool UseDefaultJurisdiction { get; }
            public IEnumerable<OnlandVisualTrashAssessmentAreaSimple> OnlandVisualTrashAssessmentAreas { get; }
            public OnlandVisualTrashAssessmentAreaSimple SelectedOnlandVisualTrashAssessmentArea { get; }
            public List<StormwaterJurisdictionSimple> StormwaterJurisdictions { get; }
        }
    }
}
