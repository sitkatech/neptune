using System;
using System.Collections;
using System.Collections.Generic;
using Neptune.Tests.Models;
using Newtonsoft.Json.Linq;

namespace Neptune.Tests.Helpers
{
    public static class MethodHelper
    {
        public static void ProfileMethod(Delegate method, int lap, List<TestRunResult> runResults, params object[] args)
        {
            var start = DateTime.UtcNow;
            var result = method.DynamicInvoke(args);
            var end = DateTime.UtcNow;

            var runResult = new TestRunResult(result, lap, start, end);
            if (result is IEnumerable resultAsList)
            {
                var resultAsJArray = JArray.FromObject(resultAsList);
                runResult.ObjectCount = resultAsJArray.Count;
            }

            runResults.Add(runResult);
        }

        public static T ProfileMethod<T>(Delegate method, int lap, List<TestRunResult> runResults, bool serializeResult, params object[] args)
        {
            var start = DateTime.UtcNow;
            var result = method.DynamicInvoke(args);
            var end = DateTime.UtcNow;

            var runResult = new TestRunResult(result, lap, start, end, serializeResult);
            if (serializeResult && result is IEnumerable resultAsList)
            {
                var resultAsJArray = JArray.FromObject(resultAsList);
                runResult.ObjectCount = resultAsJArray.Count;
            }

            runResults.Add(runResult);
            return (T)result;
        }
    }
}
