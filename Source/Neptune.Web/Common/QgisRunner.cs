using LtInfo.Common;
using System.Collections.Generic;

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
                "cmd.exe", commandLineArguments, true, null);

            return processUtilityResult;
        }
    }
}
