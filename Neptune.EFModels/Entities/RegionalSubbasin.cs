using Neptune.Web.Models;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class RegionalSubbasin: IHaveHRUCharacteristics, IAuditableEntity
    {
        public Geometry GetCatchmentGeometry()
        {
            return CatchmentGeometry;
        }

        public string GetAuditDescriptionString()
        {
            return $"{Watershed} {DrainID}: {RegionalSubbasinID}";
        }
    }
}