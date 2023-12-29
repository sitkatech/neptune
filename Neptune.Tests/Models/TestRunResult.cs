using System;
using System.Collections;
using System.Text;
using Humanizer.Bytes;
using Neptune.Tests.Interfaces;
using Newtonsoft.Json;

namespace Neptune.Tests.Models
{
    public class TestRunResult : IAmATestResult
    {
        public int Lap { get; set; }
        public string SerializedResult { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long RunTimeTicks { get; set; }
        public int ObjectCount { get; set; }
        public bool Success { get; set; }
        public long ResultSize { get; set; }
        public ByteSize ResultByteSize
        {
            get
            {
                if (string.IsNullOrEmpty(SerializedResult))
                {
                    return ByteSize.FromBytes(0);
                }

                var result = new ByteSize(Encoding.ASCII.GetByteCount(SerializedResult));
                return result;
            }
        }

        public string ResultSizeAsString => ResultSize.ToString("#.#");
        public TimeSpan RunTime => TimeSpan.FromTicks(RunTimeTicks);

        public TestRunResult(object result, int lap, DateTime start, DateTime end, bool serializeResult = true)
        {
            if (serializeResult)
            {
                SerializedResult = JsonConvert.SerializeObject(result, Formatting.Indented);
            }

            ResultSize = ResultByteSize.Bits;
            Lap = lap;
            Start = start;
            End = end;

            if (result is IList objectAsList)
            {
                ObjectCount = objectAsList.Count;
            }
            else
            {
                ObjectCount = result != null ? 1 : 0;
            }

            var runTime = end - start;
            RunTimeTicks = runTime.Ticks;
        }

        public bool IsEnumerable(object o)
        {
            if (o == null) return false;
            return o is IEnumerable;
        }
    }
}
