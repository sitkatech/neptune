using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class RegionalSubbasin: IHaveHRUCharacteristics
    {
        public Geometry GetCatchmentGeometry()
        {
            return CatchmentGeometry;
        }
    }
}