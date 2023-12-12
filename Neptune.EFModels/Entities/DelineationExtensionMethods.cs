using Neptune.EFModels.Util;
using Neptune.Models.DataTransferObjects;

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

        public static DelineationDto AsDto(this Delineation delineation)
        {
            var dto = new DelineationDto
            {
                DelineationID = delineation.DelineationID,
                DelineationTypeID = delineation.DelineationTypeID,
                IsVerified = delineation.IsVerified,
                DateLastVerified = delineation.DateLastVerified,
                VerifiedByPersonID = delineation.VerifiedByPersonID,
                TreatmentBMPID = delineation.TreatmentBMPID,
                DateLastModified = delineation.DateLastModified,
                HasDiscrepancies = delineation.HasDiscrepancies,
                Geometry = delineation.GetGeometry4326GeoJson(),
                DelineationArea = delineation.GetDelineationArea(),
                DelineationTypeName = delineation.DelineationType.DelineationTypeName
            };

            return dto;
        }

        public static double? GetDelineationArea(this Delineation delineation)
        {
            return delineation?.DelineationGeometry.Area != null
                ? Math.Round(delineation.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres, 2)
                : null;
        }
    }
}