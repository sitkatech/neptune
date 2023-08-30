using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanizer.Bytes;
using Neptune.Tests.Models;

namespace Neptune.Tests.Helpers
{
    public static class ConsoleHelper
    {
        public static void OutputTestResultsToConsole(List<TestRunResult> lapResults)
        {
            if (lapResults.Count == 1)
            {
                OutputTestResultsToConsole(lapResults.First());
            }
            else
            {
                OutputTestRunResultsToConsole(lapResults);
            }
        }

        private static void OutputTestResultsToConsole(TestRunResult testRunResult)
        {
            if (testRunResult == null)
            {
                Console.WriteLine("No test result provided for this test.");
                return;
            }

            var serializedResultSizeAsBytes = !string.IsNullOrEmpty(testRunResult.SerializedResult)
                ? Encoding.ASCII.GetByteCount(testRunResult.SerializedResult)
                : 0;

            var resultByteSize = ByteSize.FromBytes(serializedResultSizeAsBytes);

            Console.WriteLine($"Result took {testRunResult.RunTime.TotalMilliseconds:N2} milliseconds.");

            if (testRunResult.SerializedResult == "null")
            {
                Console.WriteLine("Result was null.");
                return;
            }

            if (testRunResult.ObjectCount > 0)
            {
                Console.WriteLine(testRunResult.ObjectCount > 1 ? $"Result contained {testRunResult.ObjectCount} objects." : $"Result contained an object.");
            }

            var resultSize = resultByteSize.ToString("#.#");
            Console.WriteLine($"Result was {resultSize}.");

            if (resultByteSize < ByteSize.FromMegabytes(1))
            {
                Console.WriteLine("Result:");
                Console.WriteLine(testRunResult.SerializedResult);
            }
            else
            {
                Console.WriteLine("Result is too large to display in console.");
            }
        }

        private static void OutputTestRunResultsToConsole(List<TestRunResult> lapResults)
        {
            if (!lapResults.Any())
            {
                Console.WriteLine("No test results provided for this test.");
                return;
            }

            var lapTimes = lapResults.Select(x => x.RunTime);
            var lapsAsMilliseconds = lapTimes.Select(x => x.TotalMilliseconds).ToList();
            var averageLapTime = lapsAsMilliseconds.Average().ToString("N2");
            var shortestLapTime = lapsAsMilliseconds.Min().ToString("N2");
            var longestLapTime = lapsAsMilliseconds.Max().ToString("N2");

            Console.WriteLine($"Took an average of {averageLapTime} milliseconds over {lapResults.Count} laps.");
            Console.WriteLine($"Shortest lap was {shortestLapTime} milliseconds.");
            Console.WriteLine($"Longest lap was {longestLapTime} milliseconds.");
        }
    }
}
