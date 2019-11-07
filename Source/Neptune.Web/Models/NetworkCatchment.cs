using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Neptune.Web.Models
{
    public partial class NetworkCatchment: IHaveHRUCharacteristics
    {
        public DbGeometry GetCatchmentGeometry()
        {
            return CatchmentGeometry;
        }
    }
}