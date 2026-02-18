---
name: precommit-review
description: Review uncommitted changes like a PR reviewer — catch bugs, missed patterns, and common errors before committing.
allowed-tools: [Bash(git diff:*), Bash(git status:*), Bash(git log:*), Bash(dotnet test:*), Read, Glob, Grep, mcp__atlassian__getJiraIssue, mcp__atlassian__getAccessibleAtlassianResources]
---

Review all staged and unstaged changes as if performing a thorough PR review, catching issues before they are committed.

## Arguments

- `$ARGUMENTS` — Optional. Supports two flags that can be combined:
  - **File paths or globs** to limit the review scope. If omitted, all uncommitted changes are reviewed.
  - **`--card <JIRA-KEY>`** to also check the implementation against a Jira card's acceptance criteria.

### Examples

```
/precommit-review
/precommit-review --card NPT-1224
/precommit-review Neptune.EFModels/Entities/TreatmentBMPs.cs
/precommit-review Neptune.Web/src/app/pages/treatment-bmps/ --card NPT-1225
```

## Steps

1. **Gather the diff.** Run `git diff HEAD` (or scoped to the provided paths) to see all uncommitted changes. Also run `git status` to identify untracked files. For untracked files, read their full contents.

2. **Read context.** For each changed file, read enough surrounding context to understand the change (the full file if small, or the relevant class/method if large). Also read closely related files when needed (e.g. if a controller changed, read the corresponding business logic class in `Neptune.EFModels/Entities/`; if a DTO changed, read the controller that uses it; if a component TS changed, read its template).

3. **Review against this checklist.** Evaluate every change against each category below. Only report **actual issues found** — do not list categories that pass.

   ### Correctness
   - Logic errors, off-by-one, null/undefined access, wrong variable used
   - Missing `await` on async calls
   - API contract mismatches (DTO field names vs. what the controller/frontend expects)
   - Missing or incorrect validation (e.g. FK constraints that could be violated)

   ### Project Patterns (from CLAUDE.md)
   - **Backend:** Business logic in static helper classes in `Neptune.EFModels/Entities/` (e.g. `TreatmentBMPs.cs`, `Projects.cs`), not in controllers. Controllers extend `SitkaController<T>(dbContext, logger, neptuneConfiguration)` with 3 params. Authorization uses `BaseAuthorizationAttribute` derivatives: `[AdminFeature]`, `[SitkaAdminFeature]`, `[JurisdictionEditFeature]`, `[UserViewFeature]`, `[LoggedInUnclassifiedFeature]`, `[TreatmentBMPViewFeature]`.
   - **Frontend:** Standalone components only (no NgModules). Modern `@if`/`@for` flow control in templates (not `*ngIf`/`*ngFor`). Observables + async pipe for data display. Full import paths from `src/app/`. SCSS imports: `@use "/src/scss/abstracts" as *;`. Reactive forms with `src/app/shared/components/forms/form-field/`.
   - **Generated files:** Files under `*/Generated/` directories and `Neptune.Web/src/app/shared/generated/` should not be hand-edited.

   ### Security
   - SQL injection, XSS, command injection
   - Secrets or credentials in code
   - Missing authorization attributes on new endpoints

   ### Consistency
   - Naming conventions (PascalCase C#, camelCase TS properties, PascalCase TS DTOs)
   - New code matches the style of adjacent existing code
   - Duplicated code that should be shared (e.g. identical SCSS in sibling components)

   ### Completeness
   - New API endpoints missing tests
   - New backend validation missing corresponding error handling on the frontend
   - Missing imports or declarations
   - Database changes without corresponding EF model updates (or vice versa)
   - Swagger/codegen not re-run after API changes

   ### Performance
   - N+1 queries (loading collections in loops without `Include`)
   - Missing `AsNoTracking()` on read-only queries
   - Large unnecessary data loading

4. **Check against Jira card** (only if `--card <KEY>` was provided). Fetch the Jira issue using `getAccessibleAtlassianResources` then `getJiraIssue` (with cloudId from the first call). Compare the card's scope/acceptance criteria against the uncommitted changes:
   - Build a table mapping each card requirement to its implementation status (Done / Partial / Missing).
   - Any **Missing** items go into Critical.
   - Any **Partial** items go into Good Idea.
   - Append a **Jira Card Coverage** section after Looks Good with the full requirements table.

5. **Run tests.** Execute the full test suite:
   ```
   dotnet test Neptune.Tests/Neptune.Tests.csproj --no-restore
   ```
   - If any tests fail, include each failure in the Critical findings with the test name and error message.
   - If all tests pass, note the count in the Looks Good section.

6. **Present findings** organized by severity:

   ### Format

   ```
   ## 🔴 Critical (Must Fix)
   1. **[file:line]** Description of the issue and suggested fix
   2. **[file:line]** Description of the issue and suggested fix

   ## 🟠 Good Idea (Probably Should Fix)
   1. **[file:line]** Description of the issue and suggested fix
   2. **[file:line]** Description of the issue and suggested fix

   ## 🟡 Nits (Dealer's Choice)
   1. **[file:line]** Description of the suggestion
   2. **[file:line]** Description of the suggestion

   ## 🟢 Looks Good
   Brief summary of what was reviewed and found to be correct.
   ```

   Number each finding sequentially within its category (starting at 1 per category) so findings can be referenced easily (e.g. "fix Good Idea #2").

   **IMPORTANT:** Use the literal Unicode emoji characters shown above (🔴, 🟠, 🟡, 🟢), NOT markdown shortcodes like `:red_circle:` — shortcodes do not render in Windows Terminal.

   If there are no issues at any severity level, say so and summarize what was reviewed.

7. **Do NOT make any changes.** This skill is read-only — it only reports findings. The user decides what to act on.
