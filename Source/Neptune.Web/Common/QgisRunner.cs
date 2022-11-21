using System;
using LtInfo.Common;
using System.Collections.Generic;
using System.IO;

namespace Neptune.Web.Common
{
    public class QgisRunner
    {
        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, string workingDirectory,
            string outputFolder, string outputFilename)
        {
            var commandLineArguments = new List<string>
            {
                "/q",
                "/c",
                NeptuneWebConfiguration.PathToPyqgisLauncher,
                pathToPyqgisScript,       
                NeptuneWebConfiguration.DatabaseConnectionString,
                outputFolder,
                outputFilename
            };

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(workingDirectory,
                "cmd.exe", commandLineArguments, true, null);

            return processUtilityResult;
        }

        public static ProcessUtilityResult ExecutePyqgisScript(string pathToPyqgisScript, string workingDirectory, 
            List<string> additionalArguments)
        {
            var commandLineArguments = new List<string>
            {
                "/q",
                "/c",
                NeptuneWebConfiguration.PathToPyqgisLauncher,
                pathToPyqgisScript,       
                NeptuneWebConfiguration.DatabaseConnectionString
            };

            commandLineArguments.AddRange(additionalArguments);

            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(workingDirectory,
                "cmd.exe", commandLineArguments, true, null);

            return processUtilityResult;
        }


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
