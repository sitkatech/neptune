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
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisTestWorkingDirectory}TestPyqgisProcessing.py", @"C:\Windows\System32\");

            if (processUtilityResult.ReturnCode == 0)
            {
                return Content($"Pyqgis execution succeeded. Output of QgisRunner test:\n {processUtilityResult.StdOut}");
            }
            else
            {
                return Content($"Pyqgis execution failed. Output of QgisRunner test:\n {processUtilityResult.StdOutAndStdErr}");
            }
        }

        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult TestFlattenDelineations()
        {
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisTestWorkingDirectory}FlattenDelineations.py", NeptuneWebConfiguration.PyqgisTestWorkingDirectory);

            if (processUtilityResult.ReturnCode == 0)
            {
                return Content($"Pyqgis execution succeeded. Output of QgisRunner test:\r\n {processUtilityResult.StdOut}");
            }
            else
            {
                return Content($"Pyqgis execution failed. Output of QgisRunner test:\r\n {processUtilityResult.StdOutAndStdErr}");
            }
        }
    }
}
