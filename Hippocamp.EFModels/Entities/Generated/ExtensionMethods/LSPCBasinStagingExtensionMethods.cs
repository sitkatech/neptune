//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasinStaging]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class LSPCBasinStagingExtensionMethods
    {
        public static LSPCBasinStagingDto AsDto(this LSPCBasinStaging lSPCBasinStaging)
        {
            var lSPCBasinStagingDto = new LSPCBasinStagingDto()
            {
                LSPCBasinStagingID = lSPCBasinStaging.LSPCBasinStagingID,
                LSPCBasinKey = lSPCBasinStaging.LSPCBasinKey,
                LSPCBasinName = lSPCBasinStaging.LSPCBasinName
            };
            DoCustomMappings(lSPCBasinStaging, lSPCBasinStagingDto);
            return lSPCBasinStagingDto;
        }

        static partial void DoCustomMappings(LSPCBasinStaging lSPCBasinStaging, LSPCBasinStagingDto lSPCBasinStagingDto);

        public static LSPCBasinStagingSimpleDto AsSimpleDto(this LSPCBasinStaging lSPCBasinStaging)
        {
            var lSPCBasinStagingSimpleDto = new LSPCBasinStagingSimpleDto()
            {
                LSPCBasinStagingID = lSPCBasinStaging.LSPCBasinStagingID,
                LSPCBasinKey = lSPCBasinStaging.LSPCBasinKey,
                LSPCBasinName = lSPCBasinStaging.LSPCBasinName
            };
            DoCustomSimpleDtoMappings(lSPCBasinStaging, lSPCBasinStagingSimpleDto);
            return lSPCBasinStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(LSPCBasinStaging lSPCBasinStaging, LSPCBasinStagingSimpleDto lSPCBasinStagingSimpleDto);
    }
}