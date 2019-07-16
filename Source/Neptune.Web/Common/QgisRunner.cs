using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;

namespace Neptune.Web.Common
{
    public class QgisRunner
    {
        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, string workingDirectory)
        {
            var commandLineArguments = new List<string>
            {
                "/q",
                "/c",
                NeptuneWebConfiguration.PathToPyqgisLauncher,
                pathToPyqgisScript,       
                NeptuneWebConfiguration.DatabaseConnectionString
            };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(workingDirectory,
                "cmd.exe", commandLineArguments, true, Convert.ToInt32(5000));

            return processUtilityResult;
        }
    }
}
