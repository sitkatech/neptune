using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class RegionalSubbasin: IHaveHRUCharacteristics, IAuditableEntity
    {
        public DbGeometry GetCatchmentGeometry()
        {
            return CatchmentGeometry;
        }

        public string GetAuditDescriptionString()
        {
            return $"{Watershed} {DrainID}: {RegionalSubbasinID}";
            
        }

        public RegionalSubbasin GetOCSurveyDownstreamCatchment()
        {
            return HttpRequestStorage.DatabaseEntities.RegionalSubbasins.Single(x =>
                x.OCSurveyCatchmentID == OCSurveyDownstreamCatchmentID);
        }
    }
}