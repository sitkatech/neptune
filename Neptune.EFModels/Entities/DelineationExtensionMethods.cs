using Neptune.EFModels.Util;
using Neptune.Models.DataTransferObjects;
using System;
using Neptune.Common.GeoSpatial;

namespace Neptune.EFModels.Entities
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
                Geometry = delineation.GetGeometry4326GeoJson(),
                HasDiscrepancies = delineation.HasDiscrepancies,
                TreatmentBMPID = delineation.TreatmentBMPID
            };

            return delineationUpsertDto;
        }

        static partial void DoCustomSimpleDtoMappings(Delineation delineation, DelineationSimpleDto delineationSimpleDto)
        {
            delineationSimpleDto.Geometry = delineation.GetGeometry4326GeoJson();
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
        public static GeometryGeoJSONAndAreaDto AsGeometryGeoJSONAndAreaDto(
            this vRegionalSubbasinUpstreamCatchmentGeometry4326 regionalSubbasinUpstreamCatchmentGeometry4326,
            int treatmentBMPID, int? delineationID)
        {
            return new GeometryGeoJSONAndAreaDto()
            {
                GeometryGeoJSON = regionalSubbasinUpstreamCatchmentGeometry4326.GetUpstreamCatchGeometry4326GeoJson(treatmentBMPID, delineationID),
                Area = Math.Round(regionalSubbasinUpstreamCatchmentGeometry4326.UpstreamCatchmentGeometry4326.ProjectTo2771().Area * DbSpatialHelper.SquareMetersToAcres, 2)
            };
        }
    }
}