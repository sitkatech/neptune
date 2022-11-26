using LtInfo.Common;
using System.Collections.Generic;

namespace Neptune.Web.Common
{
    public class QgisRunner
    {
        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, string workingDirectory,
            string outputFolder, string outputFilename)
        {
            var commandLineArguments = new List<string>
            {
                pathToPyqgisScript,
                NeptuneWebConfiguration.DatabaseServerName,
                NeptuneWebConfiguration.DatabaseName,
                NeptuneWebConfiguration.PyqgisUsername,
                NeptuneWebConfiguration.PyqgisPassword,
                outputFolder,
                outputFilename
            };
            return ExecutePyqgisScriptImpl(workingDirectory, commandLineArguments);
        }

        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, string workingDirectory, 
            List<string> additionalArguments)
        {
            var commandLineArguments = new List<string>
            {
                pathToPyqgisScript,       
                NeptuneWebConfiguration.DatabaseServerName,
                NeptuneWebConfiguration.DatabaseName,
                NeptuneWebConfiguration.PyqgisUsername,
                NeptuneWebConfiguration.PyqgisPassword
            };

            commandLineArguments.AddRange(additionalArguments);

            return ExecutePyqgisScriptImpl(workingDirectory, commandLineArguments);
        }

        private static ProcessUtilityResult ExecutePyqgisScriptImpl(string workingDirectory, List<string> commandLineArguments)
        {
            var environmentVariables = new Dictionary<string, string>{
                {"PROJ_DATA", $"{NeptuneWebConfiguration.PathToPyqgisProjData}"},
            };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(workingDirectory,
                NeptuneWebConfiguration.PathToPyqgisLauncher, commandLineArguments, true, 3600000, environmentVariables);

            return processUtilityResult;
        }
    }
}
