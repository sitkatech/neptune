using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class Parcel : IAuditableEntity
    {
        public bool HasValidAddress()
        {
            return !(string.IsNullOrWhiteSpace(ParcelAddress) || string.IsNullOrWhiteSpace(ParcelZipCode));
        }

        public string GetParcelAddress()
        {
            return $"{ParcelAddress}, {ParcelZipCode}";
        }

        public string GetAuditDescriptionString()
        {
            return ParcelNumber;
        }
    }
}
