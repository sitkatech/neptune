using LtInfo.Common;
using System.Collections.Generic;

namespace Neptune.Web.Common
{
    public class QgisRunner
    {
        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, string workingDirectory, 
            List<string> additionalArguments)
        {
            var commandLineArguments = new Dictionary<string, bool>
            {
                {pathToPyqgisScript, false},
                {NeptuneWebConfiguration.DatabaseServerName, false},
                {NeptuneWebConfiguration.DatabaseName, false},
                {NeptuneWebConfiguration.PyqgisUsername, false},
                {NeptuneWebConfiguration.PyqgisPassword, true}
            };

            foreach (var additionalArgument in additionalArguments)
            {
                commandLineArguments.Add(additionalArgument, false);
            }

            return ExecutePyqgisScriptImpl(workingDirectory, commandLineArguments);
        }

        private static ProcessUtilityResult ExecutePyqgisScriptImpl(string workingDirectory, Dictionary<string, bool> commandLineArguments)
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
