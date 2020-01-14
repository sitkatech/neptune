﻿using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Neptune.Web.Views.TreatmentBMP;

namespace Neptune.Web.Views.RegionalSubbasin
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.RegionalSubbasin RegionalSubbasin { get; }
        public string HRURefreshUrl { get; }
        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }
        public StormwaterMapInitJson MapInitJson { get; }

        public DetailViewData(Person currentPerson, Models.RegionalSubbasin regionalSubbasin, HRUCharacteristicsViewData hruCharacteristicsViewData, StormwaterMapInitJson mapInitJson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Regional Subbasin";
            EntityUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = $"{regionalSubbasin.Watershed} {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}";

            RegionalSubbasin = regionalSubbasin;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;
            MapInitJson = mapInitJson;
            HRURefreshUrl =
                SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(x =>
                    x.RefreshHRUCharacteristics(RegionalSubbasin));
        }
    }
}
