using LtInfo.Common.DhtmlWrappers;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class ParcelGridSpec : GridSpec<Models.Parcel>
    {
        public ParcelGridSpec()
        {
            Add("Parcel Number", x => x.ParcelNumber, 100);
            Add("Address", x => x.ParcelAddress, 300);
        }
    }
}
