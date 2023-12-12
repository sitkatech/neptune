//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SizingBasisType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SizingBasisTypeExtensionMethods
    {

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