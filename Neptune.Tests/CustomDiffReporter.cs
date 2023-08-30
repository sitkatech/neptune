using ApprovalTests.Reporters;
using ApprovalTests.Reporters.TestFrameworks;

namespace Neptune.Tests;

public class CustomDiffReporter :
    FirstWorkingReporter
{
    public CustomDiffReporter()
        : base(
            //TODO: re-order or remove as required
            TortoiseDiffReporter.INSTANCE,
            VisualStudioReporter.INSTANCE,
            BeyondCompareReporter.INSTANCE,
            AraxisMergeReporter.INSTANCE,
            P4MergeReporter.INSTANCE,
            WinMergeReporter.INSTANCE,
            KDiff3Reporter.INSTANCE,
            RiderReporter.INSTANCE,
            FrameworkAssertReporter.INSTANCE,
            QuietReporter.INSTANCE
        )
    {
    }
}