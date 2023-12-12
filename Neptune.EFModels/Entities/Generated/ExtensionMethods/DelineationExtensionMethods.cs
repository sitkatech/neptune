//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DelineationExtensionMethods
    {

        public static DelineationSimpleDto AsSimpleDto(this Delineation delineation)
        {
            var delineationSimpleDto = new DelineationSimpleDto()
            {
                DelineationID = delineation.DelineationID,
                DelineationTypeID = delineation.DelineationTypeID,
                IsVerified = delineation.IsVerified,
                DateLastVerified = delineation.DateLastVerified,
                VerifiedByPersonID = delineation.VerifiedByPersonID,
                TreatmentBMPID = delineation.TreatmentBMPID,
                DateLastModified = delineation.DateLastModified,
                HasDiscrepancies = delineation.HasDiscrepancies
            };
            DoCustomSimpleDtoMappings(delineation, delineationSimpleDto);
            return delineationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(Delineation delineation, DelineationSimpleDto delineationSimpleDto);
    }
}