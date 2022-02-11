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
                TreatmentBMPID = delineation.TreatmentBMPID
            };

            return delineationUpsertDto;
        }

        public static double? GetDelineationArea(this Delineation delineation)
        {
            return delineation?.DelineationGeometry.Area != null
                ? Math.Round(delineation.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres, 2)
                : null;
        }
    }
}