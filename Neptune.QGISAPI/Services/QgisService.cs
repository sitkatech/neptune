﻿using Neptune.Common;

namespace Neptune.QGISAPI.Services
{
    public class QgisService
    {
        private readonly ILogger<QgisService> _logger;

        public QgisService(ILogger<QgisService> logger)
        {
            _logger = logger;
        }

        public ProcessUtilityResult Run(Dictionary<string, bool> arguments)
        {
            var exeFileName = "python3";
            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(null, exeFileName, arguments, true, 250000000, new Dictionary<string, string>(), _logger);
            if (processUtilityResult.ReturnCode != 0)
            {
                var argumentsAsString = ProcessUtility.ConjoinAndMaskCommandLineArguments(arguments);
                var fullProcessAndArguments =
                    $"{ProcessUtility.EncodeArgumentForCommandLine(exeFileName)} {argumentsAsString}";
                var errorMessage =
                    $"Process \"{exeFileName}\" returned with exit code {processUtilityResult.ReturnCode}, expected exit code 0.\r\n\r\nStdErr and StdOut:\r\n{processUtilityResult.StdOutAndStdErr}\r\n\r\nProcess Command Line:\r\n{fullProcessAndArguments}";
                throw new ApplicationException(errorMessage);
            }
            return processUtilityResult;

            //_logger.LogInformation($"Running ogr2ogr with the following arguments [{arguments}]");
            //var process = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = exeFileName,
            //        Arguments = arguments,
            //        RedirectStandardOutput = true,
            //        RedirectStandardError = true,
            //        UseShellExecute = false,
            //        CreateNoWindow = true
            //    }
            //};
            //process.Start();
            //var stdOutString = await process.StandardOutput.ReadToEndAsync();
            //var errorString = await process.StandardError.ReadToEndAsync();
            //LogOutput(stdOutString, false);
            //LogOutput(errorString, true);
            //await process.WaitForExitAsync();
            
            //return stdOutString;
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
