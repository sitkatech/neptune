//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SizingBasisType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class SizingBasisTypeExtensionMethods
    {
        public static SizingBasisTypeDto AsDto(this SizingBasisType sizingBasisType)
        {
            var sizingBasisTypeDto = new SizingBasisTypeDto()
            {
                SizingBasisTypeID = sizingBasisType.SizingBasisTypeID,
                SizingBasisTypeName = sizingBasisType.SizingBasisTypeName,
                SizingBasisTypeDisplayName = sizingBasisType.SizingBasisTypeDisplayName
            };
            DoCustomMappings(sizingBasisType, sizingBasisTypeDto);
            return sizingBasisTypeDto;
        }

        static partial void DoCustomMappings(SizingBasisType sizingBasisType, SizingBasisTypeDto sizingBasisTypeDto);

        public static SizingBasisTypeSimpleDto AsSimpleDto(this SizingBasisType sizingBasisType)
        {
            var sizingBasisTypeSimpleDto = new SizingBasisTypeSimpleDto()
            {
                SizingBasisTypeID = sizingBasisType.SizingBasisTypeID,
                SizingBasisTypeName = sizingBasisType.SizingBasisTypeName,
                SizingBasisTypeDisplayName = sizingBasisType.SizingBasisTypeDisplayName
            };
            DoCustomSimpleDtoMappings(sizingBasisType, sizingBasisTypeSimpleDto);
            return sizingBasisTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(SizingBasisType sizingBasisType, SizingBasisTypeSimpleDto sizingBasisTypeSimpleDto);
    }
}