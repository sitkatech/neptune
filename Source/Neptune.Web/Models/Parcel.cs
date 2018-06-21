namespace Neptune.Web.Models
{
    public partial class Parcel : IAuditableEntity
    {
        public bool HasValidAddress()
        {
            return !(string.IsNullOrWhiteSpace(ParcelAddress) || string.IsNullOrWhiteSpace(ParcelZipCode));
        }

        public string GetParcelAddress() => $"{ParcelAddress}, {ParcelZipCode}";

        public string AuditDescriptionString => ParcelNumber;
    }
}