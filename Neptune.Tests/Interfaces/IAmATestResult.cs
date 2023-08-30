using System;
using Humanizer.Bytes;

namespace Neptune.Tests.Interfaces
{
    public interface IAmATestResult
    {
        int Lap { get; set; }
        string SerializedResult { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        long RunTimeTicks { get; set; }
        int ObjectCount { get; set; }
        bool Success { get; set; }
        ByteSize ResultByteSize { get; }
        public string ResultSizeAsString { get; }
        TimeSpan RunTime { get; }
    }
}
