using Neptune.Common;

namespace Neptune.QGISAPI
{
    public class QgisRunner
    {
        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, 
            List<string> additionalArguments, string databaseServerName, string databaseName, string pyqgisUsername, string pyqgisPassword, string pathToPyqgisProjData, string pathToPyqgisLauncher, ILogger logger)
        {
            var commandLineArguments = new List<string>
            {
                pathToPyqgisScript,
                databaseServerName,
                databaseName,
                pyqgisUsername,
                pyqgisPassword
            };
            commandLineArguments.AddRange(additionalArguments);

            return ExecutePyqgisScriptImpl(commandLineArguments, pathToPyqgisProjData, pathToPyqgisLauncher, logger);
        }

        private static ProcessUtilityResult ExecutePyqgisScriptImpl(List<string> commandLineArguments, string pathToPyqgisProjData, string pathToPyqgisLauncher, ILogger logger)
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
