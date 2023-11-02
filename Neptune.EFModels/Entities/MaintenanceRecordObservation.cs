using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class MaintenanceRecordObservation
    {
        public string GetObservationValueWithoutUnits()
        {
            return string.Join(", ",
                MaintenanceRecordObservationValues.OrderBy(x => x.ObservationValue).Select(x => x.ObservationValue));
        }

        public bool IsObservationComplete()
        {
            return !MaintenanceRecordObservationValues.All(y =>
                string.IsNullOrWhiteSpace(y.ObservationValue));
        }

        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
           await dbContext.MaintenanceRecordObservationValues.Where(x =>
                x.MaintenanceRecordObservationID == MaintenanceRecordObservationID).ExecuteDeleteAsync();
           await dbContext.MaintenanceRecordObservations.Where(x => x.MaintenanceRecordObservationID == MaintenanceRecordObservationID)
                .ExecuteDeleteAsync();
        }
    }
}