using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class EditMaintenanceRecordObservationsViewModel : EditAttributesViewModel
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditMaintenanceRecordObservationsViewModel()
        {

        }

        public EditMaintenanceRecordObservationsViewModel(Models.MaintenanceRecord maintenanceRecord)
        {
            CustomAttributes =
                maintenanceRecord.MaintenanceRecordObservations.Select(x => new CustomAttributeSimple(x)).ToList();
        }

        public void UpdateModel(Models.MaintenanceRecord maintenanceRecord)
        {

            var treatmentBMPTypeCustomAttributeTypes = maintenanceRecord.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();
            var customAttributeSimplesWithValues = CustomAttributes.Where(x => x.CustomAttributeValues != null && x.CustomAttributeValues.Count > 0);
            var customAttributesToUpdate = new List<MaintenanceRecordObservation>();
            var customAttributeValuesToUpdate = new List<MaintenanceRecordObservationValue>();
            foreach (var x in customAttributeSimplesWithValues)
            {

                var customAttribute = new MaintenanceRecordObservation(maintenanceRecord.MaintenanceRecordID,
                    treatmentBMPTypeCustomAttributeTypes.Single(y => y.CustomAttributeTypeID == x.CustomAttributeTypeID)
                        .TreatmentBMPTypeCustomAttributeTypeID, maintenanceRecord.TreatmentBMP.TreatmentBMPTypeID,
                    x.CustomAttributeTypeID);
                customAttributesToUpdate.Add(customAttribute);

                foreach (var value in x.CustomAttributeValues)
                {
                    var customAttributeValue = new MaintenanceRecordObservationValue(customAttribute,value);
                    customAttributeValuesToUpdate.Add(customAttributeValue);
                }
            }

            var maintenanceRecordObservationsInDatabase = HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservations.Local;
            var maintenanceRecordObservationValuesInDatabase = HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservationValues.Local;

            var existingMaintenanceRecordObservations = maintenanceRecord.MaintenanceRecordObservations.Where(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).ToList();

            var existingMaintenanceRecordObservationValues = existingMaintenanceRecordObservations.SelectMany(x => x.MaintenanceRecordObservationValues).ToList();

            existingMaintenanceRecordObservations.Merge(customAttributesToUpdate, maintenanceRecordObservationsInDatabase,
                (x, y) => x.MaintenanceRecordID == y.MaintenanceRecordID
                          && x.TreatmentBMPTypeID == y.TreatmentBMPTypeID
                          && x.CustomAttributeTypeID == y.CustomAttributeTypeID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { });

            existingMaintenanceRecordObservationValues.Merge(customAttributeValuesToUpdate, maintenanceRecordObservationValuesInDatabase,
                (x, y) => x.MaintenanceRecordObservationValueID == y.MaintenanceRecordObservationValueID
                          && x.MaintenanceRecordObservationID == y.MaintenanceRecordObservationID,
                (x, y) => { x.ObservationValue = y.ObservationValue; });
        }
    }
}