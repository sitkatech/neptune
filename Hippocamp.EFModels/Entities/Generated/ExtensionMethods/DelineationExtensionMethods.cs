//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class DelineationExtensionMethods
    {
        public static DelineationDto AsDto(this Delineation delineation)
        {
            var delineationDto = new DelineationDto()
            {
                DelineationID = delineation.DelineationID,
                DelineationType = delineation.DelineationType.AsDto(),
                IsVerified = delineation.IsVerified,
                DateLastVerified = delineation.DateLastVerified,
                VerifiedByPerson = delineation.VerifiedByPerson?.AsDto(),
                TreatmentBMP = delineation.TreatmentBMP.AsDto(),
                DateLastModified = delineation.DateLastModified,
                HasDiscrepancies = delineation.HasDiscrepancies
            };
            DoCustomMappings(delineation, delineationDto);
            return delineationDto;
        }

        static partial void DoCustomMappings(Delineation delineation, DelineationDto delineationDto);

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