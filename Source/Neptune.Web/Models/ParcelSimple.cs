namespace Neptune.Web.Models
{
    public class ParcelSimple
    {
        public ParcelSimple(Parcel parcel)
        {
            ParcelID = parcel.ParcelID;
            ParcelNumber = parcel.ParcelNumber;
            OwnerName = parcel.OwnerName;
            LandUse = parcel.LandUse;
            ParcelAddress = parcel.ParcelAddress;
            ParcelStreetNumber = parcel.ParcelStreetNumber;
            ParcelZipCode = parcel.ParcelZipCode;
        }

        public int ParcelID { get; set; }
        public string ParcelNumber { get; set; }
        public string OwnerName { get; set; }
        public string LandUse { get; set; }
        public string ParcelAddress { set; get; }
        public string ParcelStreetNumber { get; set; }
        public string ParcelZipCode { get; set; }
    }
}
