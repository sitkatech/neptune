//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasin]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class LSPCBasinExtensionMethods
    {
        public static LSPCBasinDto AsDto(this LSPCBasin lSPCBasin)
        {
            var lSPCBasinDto = new LSPCBasinDto()
            {
                LSPCBasinID = lSPCBasin.LSPCBasinID,
                LSPCBasinKey = lSPCBasin.LSPCBasinKey,
                LSPCBasinName = lSPCBasin.LSPCBasinName,
                LastUpdate = lSPCBasin.LastUpdate
            };
            DoCustomMappings(lSPCBasin, lSPCBasinDto);
            return lSPCBasinDto;
        }

        static partial void DoCustomMappings(LSPCBasin lSPCBasin, LSPCBasinDto lSPCBasinDto);

        public static LSPCBasinSimpleDto AsSimpleDto(this LSPCBasin lSPCBasin)
        {
            var lSPCBasinSimpleDto = new LSPCBasinSimpleDto()
            {
                LSPCBasinID = lSPCBasin.LSPCBasinID,
                LSPCBasinKey = lSPCBasin.LSPCBasinKey,
                LSPCBasinName = lSPCBasin.LSPCBasinName,
                LastUpdate = lSPCBasin.LastUpdate
            };
            DoCustomSimpleDtoMappings(lSPCBasin, lSPCBasinSimpleDto);
            return lSPCBasinSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(LSPCBasin lSPCBasin, LSPCBasinSimpleDto lSPCBasinSimpleDto);
    }
}