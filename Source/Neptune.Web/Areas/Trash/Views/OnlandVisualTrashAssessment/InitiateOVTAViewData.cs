using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using log4net.Util;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewData : OVTASectionViewData
    {
        public IEnumerable<SelectListItem> Jurisdictions { get; }
        public SelectOVTAAreaMapInitJson MapInitJson { get; }
        public StormwaterJurisdiction DefaultJurisdiction { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }

        public InitiateOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity,
            Models.OnlandVisualTrashAssessment ovta, IEnumerable<SelectListItem> jurisdictions,
            SelectOVTAAreaMapInitJson mapInitJson,
            IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
            StormwaterJurisdiction defaultJurisdiction)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.InitiateOVTA, ovta)
        {
            Jurisdictions = jurisdictions;
            MapInitJson = mapInitJson;
            DefaultJurisdiction = defaultJurisdiction;
            var useDefaultJurisdiction = defaultJurisdiction != null;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, onlandVisualTrashAssessmentAreas, useDefaultJurisdiction);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(SelectOVTAAreaMapInitJson mapInitJson, IEnumerable<OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas, bool useDefaultJurisdiction)
            {
                MapInitJson = mapInitJson;
                UseDefaultJurisdiction = useDefaultJurisdiction;
                OnlandVisualTrashAssessmentAreas = onlandVisualTrashAssessmentAreas.Select(x =>
                    new OnlandVisualTrashAssessmentAreaSimple
                    {
                        OnlandVisualTrashAssessmentAreaName = x.OnlandVisualTrashAssessmentAreaName,
                        OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID
                    });
            }

            public SelectOVTAAreaMapInitJson MapInitJson { get; }
            public bool UseDefaultJurisdiction { get; }
            public IEnumerable<OnlandVisualTrashAssessmentAreaSimple> OnlandVisualTrashAssessmentAreas { get; }
        }
    }

    public class OnlandVisualTrashAssessmentAreaSimple
    {
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
    }
}
