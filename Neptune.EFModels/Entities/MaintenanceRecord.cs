using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class MaintenanceRecord
    {
        public DateTime GetMaintenanceRecordDate()
        {
            return FieldVisit.VisitDate;
        }

        public Person GetMaintenanceRecordPerson()
        {
            return FieldVisit.PerformedByPerson;
        }

        public Organization GetMaintenanceRecordOrganization()
        {
            return FieldVisit.PerformedByPerson.Organization;
        }

        public bool IsMissingRequiredAttributes()
        {
            return MaintenanceRecordObservations.Any(x => x.CustomAttributeType.IsRequired && !x.IsObservationComplete());
        }

        public string MaintenanceRecordStatus()
        {
            var completedObservationCount =
                MaintenanceRecordObservations.Count(x => x.IsObservationComplete());
            var totalObservationCount =
                MaintenanceRecordObservations.Count;

            return $"{completedObservationCount} of {totalObservationCount} observations provided";
        }

        public string GetObservationValueForAttributeType(CustomAttributeType customAttributeType)
        {
            if (customAttributeType.TreatmentBMPTypeCustomAttributeTypes.All(x => x.TreatmentBMPTypeID != TreatmentBMPTypeID))
            {
                return "n/a";
            }

            var maintenanceRecordObservation = MaintenanceRecordObservations.SingleOrDefault(y =>
                y.CustomAttributeTypeID == customAttributeType.CustomAttributeTypeID &&
                y.MaintenanceRecordObservationValues.Any(z =>
                    !string.IsNullOrWhiteSpace(z.ObservationValue)));
            return maintenanceRecordObservation?.GetObservationValueWithoutUnits() ?? "not provided";
        }

        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            await dbContext.MaintenanceRecordObservationValues
                .Include(x => x.MaintenanceRecordObservation)
                .Where(x => x.MaintenanceRecordObservation.MaintenanceRecordID == MaintenanceRecordID)
                .ExecuteDeleteAsync();
            await dbContext.MaintenanceRecordObservations.Where(x => x.MaintenanceRecordID == MaintenanceRecordID)
                .ExecuteDeleteAsync();
            await dbContext.MaintenanceRecords.Where(x => x.MaintenanceRecordID == MaintenanceRecordID)
                .ExecuteDeleteAsync();
        }
    }
}