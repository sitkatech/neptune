using Neptune.WebMvc.Common.DhtmlWrappers;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class ParcelGridSpec : GridSpec<EFModels.Entities.Parcel>
    {
        public ParcelGridSpec()
        {
            Add("Parcel Number", x => x.ParcelNumber, 100);
            Add("Address", x => x.ParcelAddress, 300);
        }
    }
}
