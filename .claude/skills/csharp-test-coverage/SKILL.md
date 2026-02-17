---
name: csharp-test-coverage
description: Run C# tests with code coverage and report per-class/method line and branch coverage with uncovered line numbers. Accepts an optional test filter and optional class filter.
allowed-tools: [Bash(dotnet test:*), Bash(reportgenerator:*), Bash(node:*), Bash(rm:*), Read]
---

Run C# tests with coverlet code coverage collection and produce a per-class coverage summary **including uncovered line numbers** for the classes you care about.

## Arguments

- `$ARGUMENTS` — An optional string with two parts (both optional):
  - **Test filter**: a `dotnet test --filter` expression (e.g. `FullyQualifiedName~TreatmentBMPControllerTests`)
  - **Class filter**: prefixed with `--classes` followed by comma-separated partial class names to include in the report (e.g. `--classes TreatmentBMPs,TreatmentBMPController`)

  If no test filter is provided, all tests are run.
  If no class filter is provided, all classes with >0% coverage are shown.

### Examples

```
/csharp-test-coverage FullyQualifiedName~TreatmentBMPControllerTests --classes TreatmentBMPs,TreatmentBMPController
/csharp-test-coverage --classes Projects,ProjectController
/csharp-test-coverage FullyQualifiedName~ExtensionMethodsTest
/csharp-test-coverage
```

## Steps

1. **Parse arguments** from `$ARGUMENTS`:
   - Split on `--classes` to extract the test filter (before) and class name filters (after, comma-separated).
   - If no `--classes` flag, treat the entire argument as the test filter.

2. **Run tests with coverlet** to collect coverage in cobertura format:
   ```
   dotnet test Neptune.Tests/Neptune.Tests.csproj [--filter "<test-filter>"] -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura -p:CoverletOutput=./coverage-results/ --no-restore
   ```
   - If a test filter was provided, include `--filter "<test-filter>"`.
   - If tests fail, report the failure and stop.

3. **Extract per-class coverage with uncovered lines** using the helper script:
   ```
   node .claude/skills/csharp-test-coverage/parse-coverage.js Neptune.Tests/coverage-results/coverage.cobertura.xml [classFilter1,classFilter2,...]
   ```
   - If class name filters were provided, pass them as a comma-separated second argument.
   - The script outputs one JSON object per line with: `class`, `file`, `lineCoverage`, `branchCoverage`, `uncoveredLines`.
   - The `uncoveredLines` field contains line ranges (e.g. `"18-20, 34-36"`) referring to the source file.

4. **Present results** as a markdown table with columns: Class, File, Line%, Branch%, Uncovered Lines.
   - For any class with uncovered lines, use the Read tool to read those specific lines from the source file so you can describe what code paths are not covered.
   - Summarize what specific test scenarios are missing to cover those lines.

5. **Clean up** temporary coverage files:
   ```
   rm -rf Neptune.Tests/coverage-results/
   ```

## Notes

- The test project uses `coverlet.msbuild` (already a dependency in `Neptune.Tests.csproj`).
- `reportgenerator` is installed as a global dotnet tool.
- Coverage files are written to `Neptune.Tests/coverage-results/` and cleaned up after reporting.
- The `--no-restore` flag speeds up the test run by skipping NuGet restore.
- The helper script `parse-coverage.js` lives alongside this SKILL.md and parses cobertura XML using Node.js.
