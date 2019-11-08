using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.NetworkCatchment
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.NetworkCatchment NetworkCatchment { get; }
        public string HRURefreshUrl { get; }

        public DetailViewData(Person currentPerson, Models.NetworkCatchment networkCatchment) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Network Catchment";
            EntityUrl = SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = $"{networkCatchment.Watershed} {networkCatchment.DrainID}: {networkCatchment.NetworkCatchmentID}";

            NetworkCatchment = networkCatchment;
            HRURefreshUrl =
                SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x =>
                    x.RefreshHRUCharacteristics(NetworkCatchment));
        }
    }
}