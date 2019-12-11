using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Neptune.Web.Views.TreatmentBMP;

namespace Neptune.Web.Views.NetworkCatchment
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.NetworkCatchment NetworkCatchment { get; }
        public string HRURefreshUrl { get; }
        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }
        public StormwaterMapInitJson MapInitJson { get; }

        public DetailViewData(Person currentPerson, Models.NetworkCatchment networkCatchment, HRUCharacteristicsViewData hruCharacteristicsViewData, StormwaterMapInitJson mapInitJson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Network Catchment";
            EntityUrl = SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = $"{networkCatchment.Watershed} {networkCatchment.DrainID}: {networkCatchment.NetworkCatchmentID}";

            NetworkCatchment = networkCatchment;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;
            MapInitJson = mapInitJson;
            HRURefreshUrl =
                SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x =>
                    x.RefreshHRUCharacteristics(NetworkCatchment));
        }
    }
}
