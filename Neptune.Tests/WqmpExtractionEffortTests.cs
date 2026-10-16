using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.API.Services.AI;

namespace Neptune.Tests
{
    /// <summary>
    /// NPT-1132: ClaudeEffort reaches the API path as output_config.effort and the CLI path as
    /// --effort. Unset leaves the model default; anything else is a configuration error.
    /// </summary>
    [TestClass]
    public class WqmpExtractionEffortTests
    {
        [TestMethod]
        public void UnsetEffort_SendsNothing()
        {
            Assert.IsNull(AnthropicApiWqmpExtractionClient.BuildOutputConfig(null));
            Assert.IsNull(AnthropicApiWqmpExtractionClient.BuildOutputConfig("  "));
            Assert.IsNull(AnthropicApiWqmpExtractionClient.NormalizeEffort(""));
        }

        [TestMethod]
        public void EveryLevel_IsAccepted_CaseInsensitively()
        {
            foreach (var level in new[] { "low", "Medium", " HIGH ", "xhigh", "max" })
            {
                Assert.IsNotNull(AnthropicApiWqmpExtractionClient.BuildOutputConfig(level), level);
                Assert.AreEqual(level.Trim().ToLowerInvariant(), AnthropicApiWqmpExtractionClient.NormalizeEffort(level));
            }
        }

        [TestMethod]
        public void UnknownLevel_Throws()
        {
            var ex = Assert.ThrowsExactly<InvalidOperationException>(() => AnthropicApiWqmpExtractionClient.BuildOutputConfig("extreme"));
            StringAssert.Contains(ex.Message, "extreme");
        }
    }
}
