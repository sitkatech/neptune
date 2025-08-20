using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Views.HRUCharacteristic;
using Neptune.WebMvc.Views.LoadGeneratingUnit;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;

namespace Neptune.WebMvc.Views.RegionalSubbasin
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.RegionalSubbasin RegionalSubbasin { get; }
        public HRUCharacteristicsViewData? HRUCharacteristicsViewData { get; }
        public StormwaterMapInitJson MapInitJson { get; }
        public bool HasAnyHRUCharacteristics { get; }
        public string? OCSurveyDownstreamCatchmentDetailUrl { get; }
        public GridSpec<vHRUCharacteristic> HRUCharacteristicsGridSpec { get; }
        public string HRUCharacteristicsGridName { get; }
        public string HRUCharacteristicsGridDataUrl { get; }
        public LoadGeneratingUnitGridSpec LoadGeneratingUnitsGridSpec { get; }
        public string LoadGeneratingUnitsGridName { get; }
        public string LoadGeneratingUnitsGridDataUrl { get; }
        public string GeoServerUrl { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration,
            Person currentPerson, EFModels.Entities.RegionalSubbasin regionalSubbasin,
            HRUCharacteristicsViewData hruCharacteristicsViewData, StormwaterMapInitJson mapInitJson,
            bool hasAnyHRUCharacteristics, EFModels.Entities.RegionalSubbasin? ocSurveyDownstreamCatchment,
            string geoServerUrl) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            HasAnyHRUCharacteristics = hasAnyHRUCharacteristics;
            EntityName = "Regional Subbasin";
            EntityUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            PageTitle = $"{regionalSubbasin.Watershed} {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}";

            RegionalSubbasin = regionalSubbasin;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;

            OCSurveyDownstreamCatchmentDetailUrl = ocSurveyDownstreamCatchment != null
                ? SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.Detail(ocSurveyDownstreamCatchment.RegionalSubbasinID)) : "";

            HRUCharacteristicsGridSpec = new HRUCharacteristicGridSpec(LinkGenerator);
            HRUCharacteristicsGridName = "HRUCharacteristics";
            HRUCharacteristicsGridDataUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.HRUCharacteristicGridJsonData(regionalSubbasin));

            LoadGeneratingUnitsGridSpec = new LoadGeneratingUnitGridSpec(LinkGenerator);
            LoadGeneratingUnitsGridName = "LoadGeneratingUnits";
            LoadGeneratingUnitsGridDataUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.LoadGeneratingUnitGridJsonData(regionalSubbasin));
        }
    }
}
