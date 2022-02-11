using Hippocamp.Models.DataTransferObjects;


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
                Geometry = delineation.Geometry4326GeoJson,
                TreatmentBMPID = delineation.TreatmentBMPID
            };

            return delineationUpsertDto;
        }
    }
}