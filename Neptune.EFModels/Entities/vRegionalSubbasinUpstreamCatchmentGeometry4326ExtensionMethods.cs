using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class vRegionalSubbasinUpstreamCatchmentGeometry4326ExtensionMethods
{
    public static GeometryGeoJSONAndAreaDto AsGeometryGeoJSONAndAreaDto(
        this vRegionalSubbasinUpstreamCatchmentGeometry4326 regionalSubbasinUpstreamCatchmentGeometry4326,
        int treatmentBMPID, int? delineationID)
    {
        return new GeometryGeoJSONAndAreaDto()
        {
            GeometryGeoJSON = regionalSubbasinUpstreamCatchmentGeometry4326.GetUpstreamCatchGeometry4326GeoJson(treatmentBMPID, delineationID),
            Area = Math.Round(regionalSubbasinUpstreamCatchmentGeometry4326.UpstreamCatchmentGeometry4326.ProjectTo2771().Area * Constants.SquareMetersToAcres, 2)
        };
    }
}