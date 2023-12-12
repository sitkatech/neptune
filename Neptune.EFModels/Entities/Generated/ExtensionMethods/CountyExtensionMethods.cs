//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[County]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class CountyExtensionMethods
    {
        public static CountySimpleDto AsSimpleDto(this County county)
        {
            var dto = new CountySimpleDto()
            {
                CountyID = county.CountyID,
                CountyName = county.CountyName,
                StateProvinceID = county.StateProvinceID
            };
            return dto;
        }
    }
}