//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DelineationStagingExtensionMethods
    {

        public static DelineationStagingSimpleDto AsSimpleDto(this DelineationStaging delineationStaging)
        {
            var delineationStagingSimpleDto = new DelineationStagingSimpleDto()
            {
                DelineationStagingID = delineationStaging.DelineationStagingID,
                UploadedByPersonID = delineationStaging.UploadedByPersonID,
                TreatmentBMPName = delineationStaging.TreatmentBMPName,
                StormwaterJurisdictionID = delineationStaging.StormwaterJurisdictionID
            };
            DoCustomSimpleDtoMappings(delineationStaging, delineationStagingSimpleDto);
            return delineationStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(DelineationStaging delineationStaging, DelineationStagingSimpleDto delineationStagingSimpleDto);
    }
}