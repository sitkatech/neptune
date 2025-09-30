using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class MaintenanceRecordExtensionMethods
    {
        public static MaintenanceRecordDto AsDto(this MaintenanceRecord entity)
        {
            return new MaintenanceRecordDto
            {
                MaintenanceRecordID = entity.MaintenanceRecordID,
                Name = null
            };
        }
    }
}
