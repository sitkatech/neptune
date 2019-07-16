using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Security;

namespace Neptune.Web.Controllers
{
    public class QgisController : NeptuneBaseController
    {
        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult Test()
        {
            var processUtilityResult = QgisRunner.ExecutePyqgisScript(NeptuneWebConfiguration.PathToPyqgisTestScript, NeptuneWebConfiguration.PyqgisTestWorkingDirectory);

            if (processUtilityResult.ReturnCode == 0 && processUtilityResult.StdOut.Contains("Success") &&
                processUtilityResult.StdOut.Contains("CatchIDN"))
            {
                return Content("Pyqgis execution succeeded.");
            }
            else
            {
                return Content("Pyqgis execution failed.");
            }
        }
    }
}
