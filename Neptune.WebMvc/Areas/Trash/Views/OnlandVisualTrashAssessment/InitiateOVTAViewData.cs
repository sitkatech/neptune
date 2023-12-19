using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment.MapInitJson;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewData : OVTASectionViewData
    {
        public List<StormwaterJurisdiction> Jurisdictions { get; }
        public SelectOVTAAreaMapInitJson MapInitJson { get; }
        public StormwaterJurisdiction DefaultJurisdiction { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; }

        public InitiateOVTAViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.OnlandVisualTrashAssessment? ovta, List<StormwaterJurisdiction> jurisdictions,
            SelectOVTAAreaMapInitJson mapInitJson,
            IEnumerable<EFModels.Entities.OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
            StormwaterJurisdiction? defaultJurisdiction, string geoServerUrl)
            : base(httpContext, linkGenerator, currentPerson, webConfiguration, EFModels.Entities.OVTASection.InitiateOVTA, ovta)
        {
            Jurisdictions = jurisdictions;
            MapInitJson = mapInitJson;
            DefaultJurisdiction = defaultJurisdiction;
            var useDefaultJurisdiction = defaultJurisdiction != null;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, onlandVisualTrashAssessmentAreas, useDefaultJurisdiction, jurisdictions, geoServerUrl);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(SelectOVTAAreaMapInitJson mapInitJson,
                IEnumerable<EFModels.Entities.OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
                bool useDefaultJurisdiction,
                IEnumerable<StormwaterJurisdiction> jurisdictions, string geoServerUrl)
            {
                MapInitJson = mapInitJson;
                UseDefaultJurisdiction = useDefaultJurisdiction;
                GeoServerUrl = geoServerUrl;
                OnlandVisualTrashAssessmentAreas = onlandVisualTrashAssessmentAreas.Select(x =>
                    new OnlandVisualTrashAssessmentAreaSimple
                    {
                        OnlandVisualTrashAssessmentAreaName = x.OnlandVisualTrashAssessmentAreaName,
                        OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID,
                        StormwaterJurisdictionID = x.StormwaterJurisdictionID
                    });
                StormwaterJurisdictions = jurisdictions.Select(x => x.AsDisplayDto()).ToList();
            }

            public SelectOVTAAreaMapInitJson MapInitJson { get; }
            public bool UseDefaultJurisdiction { get; }
            public IEnumerable<OnlandVisualTrashAssessmentAreaSimple> OnlandVisualTrashAssessmentAreas { get; }
            public List<StormwaterJurisdictionDisplayDto> StormwaterJurisdictions { get; }
            public string GeoServerUrl { get; }
        }
    }
}
