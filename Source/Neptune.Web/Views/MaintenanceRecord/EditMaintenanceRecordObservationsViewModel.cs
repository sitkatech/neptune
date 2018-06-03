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
            // todo: kill?
        }

        public void UpdateModel(Models.MaintenanceRecord maintenanceRecord)
        {
            // todo: kill?
        }
    }
}