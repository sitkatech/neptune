//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionGeometry]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionGeometryExtensionMethods
    {

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