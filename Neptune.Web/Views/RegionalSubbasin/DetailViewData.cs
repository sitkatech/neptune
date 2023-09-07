using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared.HRUCharacteristics;

namespace Neptune.Web.Views.RegionalSubbasin
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.RegionalSubbasin RegionalSubbasin { get; }
        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }
        public StormwaterMapInitJson MapInitJson { get; }
        public bool HasAnyHRUCharacteristics { get; }
        public string OCSurveyDownstreamCatchmentDetailUrl { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.RegionalSubbasin regionalSubbasin, HRUCharacteristicsViewData hruCharacteristicsViewData, StormwaterMapInitJson mapInitJson, bool hasAnyHRUCharacteristics) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            HasAnyHRUCharacteristics = hasAnyHRUCharacteristics;
            EntityName = "Regional Subbasin";
            EntityUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            PageTitle = $"{regionalSubbasin.Watershed} {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}";

            RegionalSubbasin = regionalSubbasin;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;
            MapInitJson = mapInitJson;
            OCSurveyDownstreamCatchmentDetailUrl = regionalSubbasin.OCSurveyDownstreamCatchmentID != null
                ? SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.Detail(regionalSubbasin.OCSurveyDownstreamCatchment.RegionalSubbasinID)) : "";
        }
    }
}
