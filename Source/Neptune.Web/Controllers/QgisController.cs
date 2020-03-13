using System;
using System.IO;
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
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisWorkingDirectory}TestPyqgisProcessing.py", @"C:\Windows\System32\", $"{Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())}.shp");

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
        public ContentResult TestNoProcessing()
        {
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisWorkingDirectory}TestPyqgisMssql.py", @"C:\Windows\System32\", $"{Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())}.shp");

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
        public ContentResult TestComputeTrashGeneratingUnits()
        {
            var processUtilityResult = QgisRunner.ExecutePyqgisScript($"{NeptuneWebConfiguration.PyqgisWorkingDirectory}ComputeTrashGeneratingUnits.py", NeptuneWebConfiguration.PyqgisWorkingDirectory, $"{Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())}.shp");

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
