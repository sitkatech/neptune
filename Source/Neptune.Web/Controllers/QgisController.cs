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

            if (processUtilityResult.ReturnCode == 0)
            {
                return Content($"Pyqgis execution succeeded. Output of QgisRunner test:\n {processUtilityResult.StdOut}");
            }
            else
            {
                return Content($"Pyqgis execution failed. Output of QgisRunner test:\n {processUtilityResult.StdOutAndStdErr}");
            }
        }
    }
}
