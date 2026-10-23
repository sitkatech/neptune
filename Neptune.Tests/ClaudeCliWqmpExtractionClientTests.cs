using System;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.API.Services;
using Neptune.API.Services.AI;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Writer;

namespace Neptune.Tests
{
    /// <summary>
    /// NPT-1133: the Claude CLI extraction path. Pins the pieces that don't need a live CLI:
    /// schema compaction, PDF splitting, reading the CLI's event stream, error wording, and the
    /// Development-only guard.
    /// </summary>
    [TestClass]
    public class ClaudeCliWqmpExtractionClientTests
    {
        private const string ExtractedValue = """
            {"type":"object","description":"DESC","properties":{"Value":{"type":"string"},"ExtractionEvidence":{"type":"string"},"DocumentSource":{"type":"string"},
             "BoundingBox":{"type":"object","properties":{"PageNumber":{"type":"integer"}},"required":["PageNumber"],"additionalProperties":false}},
             "required":["Value","ExtractionEvidence","DocumentSource","BoundingBox"],"additionalProperties":false}
            """;

        [TestMethod]
        public void CompactSchema_ReplacesInlinedExtractedValuesWithOneDefinition()
        {
            var schema = """{"type":"object","properties":{"ApprovalDate":""" + ExtractedValue.Replace("DESC", "Approval date.") +
                         ""","City":""" + ExtractedValue.Replace("DESC", "City.") +
                         """},"required":["ApprovalDate","City"],"additionalProperties":false}""";

            var compact = JsonNode.Parse(ClaudeCliWqmpExtractionClient.CompactSchema(schema))!;

            Assert.AreEqual("#/$defs/ExtractedValue", (string)compact["properties"]!["ApprovalDate"]!["$ref"]!);
            Assert.AreEqual("Approval date.", (string)compact["properties"]!["ApprovalDate"]!["description"]!);
            Assert.AreEqual("City.", (string)compact["properties"]!["City"]!["description"]!);
            Assert.IsNotNull(compact["$defs"]!["ExtractedValue"]!["properties"]!["BoundingBox"]);
            CollectionAssert.AreEqual(new[] { "ApprovalDate", "City" }, compact["required"]!.AsArray().Select(x => (string)x!).ToArray());
        }

        [TestMethod]
        public void CompactSchema_LeavesAlreadyReferencedArraySchemasAlone()
        {
            var schema = """{"type":"object","$defs":{"ExtractedValue":""" + ExtractedValue +
                         """},"properties":{"items":{"type":"array","items":{"type":"object","properties":{"ParcelNumber":{"$ref":"#/$defs/ExtractedValue","description":"APN"}},""" +
                         """ "required":["ParcelNumber"],"additionalProperties":false}}},"required":["items"],"additionalProperties":false}""";

            var compact = JsonNode.Parse(ClaudeCliWqmpExtractionClient.CompactSchema(schema))!;

            Assert.AreEqual("#/$defs/ExtractedValue", (string)compact["properties"]!["items"]!["items"]!["properties"]!["ParcelNumber"]!["$ref"]!);
            Assert.AreEqual(1, compact["$defs"]!.AsObject().Count);
        }

        [TestMethod]
        public void PlanPageRanges_SplitsEvenlyAndCoversEveryPage()
        {
            var ranges = ClaudeCliWqmpExtractionClient.PlanPageRanges(pageCount: 200, totalBytes: 47L * 1024 * 1024, maxPartBytes: 18L * 1024 * 1024);

            CollectionAssert.AreEqual(new[] { (1, 67), (68, 134), (135, 200) }, ranges.ToArray());
        }

        [TestMethod]
        public void PlanPageRanges_SmallDocumentIsOneRange()
        {
            var ranges = ClaudeCliWqmpExtractionClient.PlanPageRanges(pageCount: 12, totalBytes: 1024, maxPartBytes: 18L * 1024 * 1024);

            CollectionAssert.AreEqual(new[] { (1, 12) }, ranges.ToArray());
        }

        [TestMethod]
        public void PlanPageRanges_RefusesAPdfWithNoPages()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
                ClaudeCliWqmpExtractionClient.PlanPageRanges(pageCount: 0, totalBytes: 40L * 1024 * 1024, maxPartBytes: 18L * 1024 * 1024));
        }

        [TestMethod]
        public void WritePdfParts_SmallPdfIsMovedWhole()
        {
            var folder = NewTempFolder();
            try
            {
                var source = WriteTestPdf(folder, pages: 3);

                var (parts, pageCount) = ClaudeCliWqmpExtractionClient.WritePdfParts(source, folder, maxPartBytes: 10L * 1024 * 1024);

                Assert.AreEqual((int?)3, pageCount);
                Assert.AreEqual(1, parts.Count);
                Assert.AreEqual(new ClaudeCliWqmpExtractionClient.PdfPart("document.pdf", 1, 3), parts[0]);
                Assert.IsTrue(File.Exists(Path.Combine(folder, "document.pdf")));
                Assert.IsFalse(File.Exists(source));
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [TestMethod]
        public void WritePdfParts_SmallPdfThatCantBeParsedIsStillUsed()
        {
            // Claude may read a PDF that PDFsharp can't (the API path never parses it locally), so
            // an unsplit PDF only loses its page count.
            var folder = NewTempFolder();
            try
            {
                var source = Path.Combine(folder, "source.pdf");
                File.WriteAllText(source, "%PDF-1.4 not really a pdf");

                var (parts, pageCount) = ClaudeCliWqmpExtractionClient.WritePdfParts(source, folder, maxPartBytes: 10L * 1024 * 1024);

                Assert.IsNull(pageCount);
                Assert.AreEqual("document.pdf", parts.Single().FileName);
                Assert.IsTrue(File.Exists(Path.Combine(folder, "document.pdf")));
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [TestMethod]
        public void WritePdfParts_LargePdfThatCantBeParsedFailsClearly()
        {
            var folder = NewTempFolder();
            try
            {
                var source = Path.Combine(folder, "source.pdf");
                File.WriteAllText(source, "%PDF-1.4 " + new string('x', 4096));

                var ex = Assert.ThrowsExactly<ClaudeCliException>(() => ClaudeCliWqmpExtractionClient.WritePdfParts(source, folder, maxPartBytes: 1024));
                StringAssert.Contains(ex.Message, "couldn't be split");
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [TestMethod]
        public void WritePdfParts_LargePdfSplitsIntoConsecutivePartsUnderTheLimit()
        {
            var folder = NewTempFolder();
            try
            {
                var source = WriteTestPdf(folder, pages: 12);
                var maxPartBytes = new FileInfo(source).Length / 2;

                var (parts, pageCount) = ClaudeCliWqmpExtractionClient.WritePdfParts(source, folder, maxPartBytes);

                Assert.AreEqual((int?)12, pageCount);
                Assert.IsTrue(parts.Count >= 2);
                Assert.AreEqual(1, parts.First().FirstPage);
                Assert.AreEqual(12, parts.Last().LastPage);
                for (var i = 1; i < parts.Count; i++)
                {
                    Assert.AreEqual(parts[i - 1].LastPage + 1, parts[i].FirstPage);
                }
                foreach (var part in parts)
                {
                    var path = Path.Combine(folder, part.FileName);
                    Assert.IsTrue(new FileInfo(path).Length <= maxPartBytes, $"{part.FileName} is over the limit");
                    using var pdf = PdfDocument.Open(path);
                    Assert.AreEqual(part.LastPage - part.FirstPage + 1, pdf.NumberOfPages);
                }
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [TestMethod]
        public void DocumentInstructions_NameEveryPartWithItsOriginalPages()
        {
            var instructions = ClaudeCliWqmpExtractionClient.BuildDocumentInstructions(
            [
                new ClaudeCliWqmpExtractionClient.PdfPart("document-pages-1-67.pdf", 1, 67),
                new ClaudeCliWqmpExtractionClient.PdfPart("document-pages-68-134.pdf", 68, 134),
            ]);

            StringAssert.Contains(instructions, "`document-pages-1-67.pdf`: original pages 1-67");
            StringAssert.Contains(instructions, "`document-pages-68-134.pdf`: original pages 68-134");
            StringAssert.Contains(instructions, "WITHOUT a `pages` parameter");
            StringAssert.Contains(instructions, "ORIGINAL page number");
        }

        [TestMethod]
        public void CliRun_RecordsDocumentsReadAndTheResult()
        {
            var run = new ClaudeCliRun();
            run.Feed("""{"type":"system","subtype":"init","apiKeySource":"none"}""");
            run.Feed("""{"type":"assistant","message":{"content":[{"type":"tool_use","id":"t1","name":"Read","input":{"file_path":"C:\\temp\\x\\document.pdf"}}]}}""");
            run.Feed("""{"type":"user","message":{"content":[{"type":"tool_result","tool_use_id":"t1","content":[{"type":"text","text":"PDF"},{"type":"document","source":{"type":"base64"}}]}]}}""");
            run.Feed("""{"type":"result","is_error":false,"stop_reason":"tool_use","num_turns":3,"structured_output":{"items":[]},"usage":{"input_tokens":4,"cache_creation_input_tokens":114433,"cache_read_input_tokens":10,"output_tokens":553}}""");

            Assert.IsTrue(run.Started);
            Assert.AreEqual(ClaudeCliRun.LoginApiKeySource, run.ApiKeySource);
            CollectionAssert.AreEquivalent(new[] { "document.pdf" }, run.DocumentsRead.ToArray());
            Assert.AreEqual(0, run.ReadProblems.Count);
            Assert.IsFalse(run.Result.IsError);
            Assert.AreEqual("""{"items":[]}""", run.Result.StructuredOutput);
            Assert.AreEqual(114433, run.Result.CacheCreationInputTokens);
            Assert.AreEqual(553, run.Result.OutputTokens);
            Assert.AreEqual(3, run.Result.NumTurns);
        }

        [TestMethod]
        public void CliRun_RecordsAReadThatDidNotReturnTheDocument()
        {
            // The case that motivated the check: the PDF was over the Read tool's 20 MB limit, and
            // the CLI still reported success with an empty answer.
            var run = new ClaudeCliRun();
            run.Feed("""{"type":"assistant","message":{"content":[{"type":"tool_use","id":"t1","name":"Read","input":{"file_path":"/tmp/x/document.pdf"}}]}}""");
            run.Feed("""{"type":"user","message":{"content":[{"type":"tool_result","tool_use_id":"t1","content":"PDF file exceeds maximum allowed size of 20MB."}]}}""");
            run.Feed("""{"type":"result","is_error":false,"structured_output":{"items":[]},"usage":{}}""");

            Assert.AreEqual(0, run.DocumentsRead.Count);
            StringAssert.Contains(run.ReadProblems.Single(), "exceeds maximum allowed size");
        }

        [TestMethod]
        public void CliRun_ReportsWhichCredentialTheCliUses()
        {
            // RunCliAsync stops the process as soon as this is anything but "none".
            var withKey = new ClaudeCliRun();
            withKey.Feed("""{"type":"system","subtype":"init","apiKeySource":"ANTHROPIC_API_KEY"}""");
            Assert.AreEqual("ANTHROPIC_API_KEY", withKey.ApiKeySource);

            StringAssert.Contains(withKey.CredentialProblem, "apiKeySource=ANTHROPIC_API_KEY");

            var unreported = new ClaudeCliRun();
            unreported.Feed("""{"type":"system","subtype":"init"}""");
            Assert.AreNotEqual(ClaudeCliRun.LoginApiKeySource, unreported.ApiKeySource);
            Assert.IsNotNull(unreported.CredentialProblem);

            var login = new ClaudeCliRun();
            login.Feed("""{"type":"system","subtype":"init","apiKeySource":"none"}""");
            login.Feed("""{"type":"assistant","message":{"content":[]}}""");
            Assert.IsNull(login.CredentialProblem);
        }

        [TestMethod]
        public void CliRun_FailsClosedWhenTheCredentialIsNeverReported()
        {
            // No init event at all: stop as soon as the model answers.
            var answered = new ClaudeCliRun();
            answered.Feed("""{"type":"assistant","message":{"content":[]}}""");
            Assert.IsNotNull(answered.CredentialProblem);

            // An error-only run still gets its own message ("not logged in").
            var errorOnly = new ClaudeCliRun();
            errorOnly.Feed("""{"type":"result","is_error":true,"result":"Not logged in · Please run /login","usage":{}}""");
            Assert.IsNull(errorOnly.CredentialProblem);
        }

        [TestMethod]
        public void CliRun_CountsAPagedReadAsRead()
        {
            var run = new ClaudeCliRun();
            run.Feed("""{"type":"assistant","message":{"content":[{"type":"tool_use","id":"t1","name":"Read","input":{"file_path":"/tmp/x/document.pdf","pages":"1-20"}}]}}""");
            run.Feed("""{"type":"user","message":{"content":[{"type":"tool_result","tool_use_id":"t1","content":[{"type":"image","source":{"type":"base64"}}]}]}}""");

            CollectionAssert.AreEquivalent(new[] { "document.pdf" }, run.DocumentsRead.ToArray());
        }

        [TestMethod]
        public void CliRun_IgnoresLinesThatAreNotJson()
        {
            var run = new ClaudeCliRun();
            run.Feed("not json");
            run.Feed("");

            Assert.IsNull(run.Result);
            Assert.IsFalse(run.Started);
        }

        [TestMethod]
        public void DescribeCliError_TellsTheDeveloperWhatToDo()
        {
            StringAssert.Contains(ClaudeCliWqmpExtractionClient.DescribeCliError("Not logged in · Please run /login", "WQMP"), "isn't logged in");
            StringAssert.Contains(ClaudeCliWqmpExtractionClient.DescribeCliError("Claude AI usage limit reached|1760000000", "WQMP"), "usage limit");
            StringAssert.Contains(ClaudeCliWqmpExtractionClient.DescribeCliError("You've hit your limit · resets 5pm", "WQMP"), "usage limit");
            StringAssert.Contains(ClaudeCliWqmpExtractionClient.DescribeCliError("something else broke", "Parcels"), "failed on Parcels: something else broke");
            // A rate limit isn't the plan's usage limit.
            StringAssert.Contains(ClaudeCliWqmpExtractionClient.DescribeCliError("Rate limit reached, retry later", "Parcels"), "failed on Parcels");
        }

        [TestMethod]
        public void Registration_DefaultsToTheCliOnlyWhereAllowed()
        {
            Assert.IsInstanceOfType<ClaudeCliWqmpExtractionClient>(RegisteredClientType(new NeptuneConfiguration(), cliAllowed: true));
            Assert.IsInstanceOfType<AnthropicApiWqmpExtractionClient>(RegisteredClientType(new NeptuneConfiguration(), cliAllowed: false));
            Assert.IsInstanceOfType<AnthropicApiWqmpExtractionClient>(RegisteredClientType(new NeptuneConfiguration { ClaudeCliEnabled = false }, cliAllowed: true));
        }

        [TestMethod]
        public void Registration_RefusesTheCliOutsideDevelopment()
        {
            var ex = Assert.ThrowsExactly<InvalidOperationException>(() =>
                WqmpExtractionClientRegistration.Add(new ServiceCollection(), new NeptuneConfiguration { ClaudeCliEnabled = true }, cliAllowed: false, "Production"));
            StringAssert.Contains(ex.Message, "'Production'");
        }

        // Which implementation the registration picked, without resolving its dependencies.
        private static object RegisteredClientType(NeptuneConfiguration configuration, bool cliAllowed)
        {
            var services = new ServiceCollection();
            WqmpExtractionClientRegistration.Add(services, configuration, cliAllowed, "Test");
            var implementation = services.Single(d => d.ServiceType == typeof(IWqmpExtractionClient)).ImplementationType!;
            return System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(implementation);
        }

        private static string NewTempFolder()
        {
            var folder = Path.Combine(Path.GetTempPath(), "npt1133-tests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(folder);
            return folder;
        }

        // A PDF whose pages carry enough text to make each one a few KB, so splits are measurable.
        private static string WriteTestPdf(string folder, int pages)
        {
            var builder = new PdfDocumentBuilder();
            var font = builder.AddStandard14Font(UglyToad.PdfPig.Fonts.Standard14Fonts.Standard14Font.Helvetica);
            for (var i = 1; i <= pages; i++)
            {
                var page = builder.AddPage(PageSize.Letter);
                for (var line = 0; line < 40; line++)
                {
                    page.AddText($"Page {i} line {line} {Guid.NewGuid():N}{Guid.NewGuid():N}", 8, new UglyToad.PdfPig.Core.PdfPoint(20, 760 - line * 18), font);
                }
            }
            var path = Path.Combine(folder, "source.pdf");
            File.WriteAllBytes(path, builder.Build());
            return path;
        }
    }
}
