using LtInfo.Common.DhtmlWrappers;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class ParcelGridSpec : GridSpec<Models.Parcel>
    {
        public ParcelGridSpec()
        {
            Add("Parcel Number", x => x.ParcelNumber, 100);
            Add("Owner", x => x.OwnerName, 200);
            Add("Land Use", x => x.LandUse, 100);
            Add("Address", x => x.ParcelAddress, 300);
            Add("Street Number", x => x.ParcelStreetNumber, 50);
            Add("Zip", x => x.ParcelZipCode, 50);
        }
    }
}
