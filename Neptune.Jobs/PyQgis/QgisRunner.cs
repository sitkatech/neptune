using Microsoft.Extensions.Logging;
using Neptune.Common;

namespace Neptune.Jobs.PyQgis
{
    public class QgisRunner
    {
        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, 
            List<string> additionalArguments, string databaseServerName, string databaseName, string pyqgisUsername, string pyqgisPassword, string pathToPyqgisProjData, string pathToPyqgisLauncher, ILogger logger)
        {
            var commandLineArguments = new Dictionary<string, bool>
            {
                {pathToPyqgisScript, false},
                {databaseServerName, false},
                {databaseName, false},
                {pyqgisUsername, false},
                {pyqgisPassword, true}
            };

            foreach (var additionalArgument in additionalArguments)
            {
                commandLineArguments.Add(additionalArgument, false);
            }

            return ExecutePyqgisScriptImpl(commandLineArguments, pathToPyqgisProjData, pathToPyqgisLauncher, logger);
        }

        private static ProcessUtilityResult ExecutePyqgisScriptImpl(Dictionary<string, bool> commandLineArguments, string pathToPyqgisProjData, string pathToPyqgisLauncher, ILogger logger)
        {
            var environmentVariables = new Dictionary<string, string>{
                {"PROJ_DATA", $"{pathToPyqgisProjData}"},
            };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl("PyQgis",
                pathToPyqgisLauncher, commandLineArguments, true, 3600000, environmentVariables, logger);

            return processUtilityResult;
        }
    }
}
