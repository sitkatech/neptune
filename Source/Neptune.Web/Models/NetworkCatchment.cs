using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using Neptune.Web.Common;

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
            return $"{Watershed} {DrainID}: {NetworkCatchmentID}";
            
        }

        public NetworkCatchment GetOCSurveyDownstreamCatchment()
        {
            return HttpRequestStorage.DatabaseEntities.NetworkCatchments.Single(x =>
                x.OCSurveyCatchmentID == OCSurveyDownstreamCatchmentID);
        }
    }
}