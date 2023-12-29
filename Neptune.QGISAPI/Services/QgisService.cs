using Neptune.Common;

namespace Neptune.QGISAPI.Services
{
    public class QgisService
    {
        private readonly ILogger<QgisService> _logger;

        public QgisService(ILogger<QgisService> logger)
        {
            _logger = logger;
        }

        public ProcessUtilityResult Run(List<string> arguments)
        {
            const string exeFileName = "python3";
            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(null, exeFileName, arguments, true, 250000000, new Dictionary<string, string>(), _logger);
            if (processUtilityResult.ReturnCode != 0)
            {
                var argumentsAsString = ProcessUtility.ConjoinCommandLineArguments(arguments);
                var fullProcessAndArguments =
                    $"{ProcessUtility.EncodeArgumentForCommandLine(exeFileName)} {argumentsAsString}";
                var errorMessage =
                    $"Process \"{exeFileName}\" returned with exit code {processUtilityResult.ReturnCode}, expected exit code 0.\r\n\r\nStdErr and StdOut:\r\n{processUtilityResult.StdOutAndStdErr}\r\n\r\nProcess Command Line:\r\n{fullProcessAndArguments}";
                throw new ApplicationException(errorMessage);
            }
            return processUtilityResult;
        }

        private void LogOutput(string output, bool isError)
        {
            if (string.IsNullOrWhiteSpace(output)) return;

            if (isError)
            {
                _logger.LogError(output);
            }
            else
            {
                _logger.LogInformation(output);
            }
        }
    }
}
