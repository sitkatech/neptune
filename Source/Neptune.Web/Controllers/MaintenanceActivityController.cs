using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using MaintenanceActivityGridSpec = Neptune.Web.Views.MaintenanceActivity.MaintenanceActivityGridSpec;

namespace Neptune.Web.Controllers
{
    public class MaintenanceActivityController : NeptuneBaseController
    {
        
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<MaintenanceActivity> MaintenanceActivitysGridJsonData()
        {
            var gridSpec = new MaintenanceActivityGridSpec(CurrentPerson);
            var bmpMaintenanceActivitys = HttpRequestStorage.DatabaseEntities.MaintenanceActivitys.ToList().Where(x => x.IsPublicRegularMaintenanceActivity()).OrderByDescending(x => x.MaintenanceActivityDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<MaintenanceActivity>(bmpMaintenanceActivitys, gridSpec);
            return gridJsonNetJObjectResult;
        }
        
    }
}