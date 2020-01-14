using System.Collections.Generic;
using System.Data.Entity.Spatial;
using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public interface IHaveHRUCharacteristics : IHavePrimaryKey
    {
        DbGeometry GetCatchmentGeometry();

        ICollection<HRUCharacteristic> HRUCharacteristics { get; set; }
    }

    public class HRUCharacteristicsSummarySimple
    {
        public string LandUse { get; set; }
        public string Area { get; set; }
        public string ImperviousCover { get; set; }
    }
}
