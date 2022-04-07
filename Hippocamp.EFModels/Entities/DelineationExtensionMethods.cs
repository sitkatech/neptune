using Hippocamp.EFModels.Util;
using Hippocamp.Models.DataTransferObjects;
using System;

namespace Hippocamp.EFModels.Entities
{
    public static partial class DelineationExtensionMethods
    {
        public static DelineationUpsertDto AsUpsertDto(this Delineation delineation)
        {
            var delineationUpsertDto = new DelineationUpsertDto()
            {
                DelineationID = delineation.DelineationID,
                DelineationTypeID = delineation.DelineationTypeID,
                DelineationArea = delineation.GetDelineationArea(),
                Geometry = delineation.Geometry4326GeoJson,
                HasDiscrepancies = delineation.HasDiscrepancies,
                TreatmentBMPID = delineation.TreatmentBMPID
            };

            return delineationUpsertDto;
        }

        static partial void DoCustomSimpleDtoMappings(Delineation delineation, DelineationSimpleDto delineationSimpleDto)
        {
            delineationSimpleDto.Geometry = delineation.Geometry4326GeoJson;
            delineationSimpleDto.DelineationArea = delineation.GetDelineationArea();
            delineationSimpleDto.DelineationTypeName = delineation.DelineationType.DelineationTypeName;
        }

        public static double? GetDelineationArea(this Delineation delineation)
        {
            return delineation?.DelineationGeometry.Area != null
                ? Math.Round(delineation.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres, 2)
                : null;
        }
    }

    public static partial class vRegionalSubbasinUpstreamCatchmentGeometry4326ExtensionMethods
    {
        public static GeometryGeoJSONAndAreaDto AsGeometryGeoJSONAndAreaDto(this vRegionalSubbasinUpstreamCatchmentGeometry4326 regionalSubbasinUpstreamCatchmentGeometry4326)
        {
            return new GeometryGeoJSONAndAreaDto()
            {
                GeometryGeoJSON = regionalSubbasinUpstreamCatchmentGeometry4326.UpstreamCatchGeometry4326GeoJson,
                Area = Math.Round(regionalSubbasinUpstreamCatchmentGeometry4326.UpstreamCatchmentGeometry4326.ProjectTo2771().Area * DbSpatialHelper.SquareMetersToAcres, 2)
            };
        }
    }
}