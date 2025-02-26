namespace Neptune.EFModels.Entities
{
    public partial class Parcel
    {
        public bool HasValidAddress()
        {
            return !string.IsNullOrWhiteSpace(ParcelAddress);
        }

        public string GetParcelAddress()
        {
            return $"{ParcelAddress}{(!string.IsNullOrWhiteSpace(ParcelCityState) ? $", {ParcelCityState}" : "")}{(!string.IsNullOrWhiteSpace(ParcelZipCode) ? $", {ParcelZipCode}" : "")}";
        }
    }
}
