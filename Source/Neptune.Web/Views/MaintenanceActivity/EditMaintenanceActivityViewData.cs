using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.MaintenanceActivity
{
    public class EditMaintenanceActivityViewData : NeptuneUserControlViewData
    {
        public EditMaintenanceActivityViewData(List<Person> persons)
        {
            AllPersons = persons.ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                x => x.FullNameLastFirst,"");

            AllMaintenanceActivityTypes = MaintenanceActivityType.All.ToSelectListWithEmptyFirstRow(x=>x.MaintenanceActivityTypeID.ToString(CultureInfo.InvariantCulture), x=>x.MaintenanceActivityTypeDisplayName,"");
        }

        public IEnumerable<SelectListItem> AllMaintenanceActivityTypes { get; }

        public IEnumerable<SelectListItem> AllPersons { get; }
    }
}