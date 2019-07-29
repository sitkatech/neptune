using System;
using LtInfo.Common;
using System.Collections.Generic;
using System.IO;

namespace Neptune.Web.Common
{
    public class QgisRunner
    {
        public static ProcessUtilityResult ExecuteTrashGeneratingUnitScript(string pathToPyqgisScript, string workingDirectory, string outputPath)
        {
            var commandLineArguments = new List<string>
            {
                "/q",
                "/c",
                NeptuneWebConfiguration.PathToPyqgisLauncher,
                pathToPyqgisScript,       
                NeptuneWebConfiguration.DatabaseConnectionString,
                outputPath
            };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(workingDirectory,
                "cmd.exe", commandLineArguments, true, null);

            return processUtilityResult;
        }
        public static ProcessUtilityResult ExecuteGenericPyqgisScript(string pathToPyqgisScript, string workingDirectory)
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
