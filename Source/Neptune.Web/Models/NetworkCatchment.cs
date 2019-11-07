using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Neptune.Web.Models
{
    public partial class NetworkCatchment: IHaveHRUCharacteristics, IAuditableEntity
    {
        public DbGeometry GetCatchmentGeometry()
        {
            return CatchmentGeometry;
        }

        public string GetAuditDescriptionString()
        {
            return $"{Watershed} - {DrainID}";
        }
    }
}