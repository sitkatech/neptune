//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionGeometry]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionGeometryExtensionMethods
    {
        public static StormwaterJurisdictionGeometryDto AsDto(this StormwaterJurisdictionGeometry stormwaterJurisdictionGeometry)
        {
            var stormwaterJurisdictionGeometryDto = new StormwaterJurisdictionGeometryDto()
            {
                StormwaterJurisdictionGeometryID = stormwaterJurisdictionGeometry.StormwaterJurisdictionGeometryID,
                StormwaterJurisdiction = stormwaterJurisdictionGeometry.StormwaterJurisdiction.AsDto()
            };
            DoCustomMappings(stormwaterJurisdictionGeometry, stormwaterJurisdictionGeometryDto);
            return stormwaterJurisdictionGeometryDto;
        }

        static partial void DoCustomMappings(StormwaterJurisdictionGeometry stormwaterJurisdictionGeometry, StormwaterJurisdictionGeometryDto stormwaterJurisdictionGeometryDto);

        public static StormwaterJurisdictionGeometrySimpleDto AsSimpleDto(this StormwaterJurisdictionGeometry stormwaterJurisdictionGeometry)
        {
            var stormwaterJurisdictionGeometrySimpleDto = new StormwaterJurisdictionGeometrySimpleDto()
            {
                StormwaterJurisdictionGeometryID = stormwaterJurisdictionGeometry.StormwaterJurisdictionGeometryID,
                StormwaterJurisdictionID = stormwaterJurisdictionGeometry.StormwaterJurisdictionID
            };
            DoCustomSimpleDtoMappings(stormwaterJurisdictionGeometry, stormwaterJurisdictionGeometrySimpleDto);
            return stormwaterJurisdictionGeometrySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(StormwaterJurisdictionGeometry stormwaterJurisdictionGeometry, StormwaterJurisdictionGeometrySimpleDto stormwaterJurisdictionGeometrySimpleDto);
    }
}