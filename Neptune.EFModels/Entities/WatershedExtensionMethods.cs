using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class WatershedExtensionMethods
    {
        public static WatershedDto AsDto(this Watershed entity)
        {
            return new WatershedDto
            {
                WatershedID = entity.WatershedID,
                WatershedName = entity.WatershedName
            };
        }
    }
}
