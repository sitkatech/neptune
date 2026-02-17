---
name: check-migration-status
description: Audit migration progress from legacy MVC to Angular SPA. Reports which entities are migrated, partial, or not started.
allowed-tools: [Read, Glob, Grep, Bash(ls:*)]
---

# Check Migration Status Skill

When the user invokes `/check-migration-status`:

## Overview

This skill audits the migration progress from legacy MVC (Neptune.WebMvc) to Angular SPA (Neptune.Web) + ASP.NET Core API (Neptune.API). It identifies which entities have been migrated, which are in progress, and which still need migration.

## Steps to Execute

### 1. Inventory Legacy Controllers

List all controllers in the legacy MVC project:
```
Neptune.WebMvc/Controllers/
```

For each controller, gather:
- Controller name (e.g., `TreatmentBMPController.cs`)
- Number of action methods (complexity indicator)
- Number of views in `Neptune.WebMvc/Views/{Entity}/`

**Exclude infrastructure controllers** (not domain entities):
- `AccountController`
- `HomeController`
- `NeptuneBaseController`
- `ErrorController`
- Any controller that doesn't represent a domain entity

### 2. Inventory New API Controllers

List all controllers in the new API project:
```
Neptune.API/Controllers/
```

Note which entity each controller handles.

### 3. Inventory Angular Pages

List all page components in:
```
Neptune.Web/src/app/pages/
```

Check `Neptune.Web/src/app/app.routes.ts` for registered routes.

### 4. Compare and Analyze

Create a migration status for each entity:

| Status | Meaning |
|--------|---------|
| **Migrated** | Has API controller + Angular page + routes |
| **Partial** | Has API controller but missing Angular page (or vice versa) |
| **Not Started** | Only exists in legacy MVC |
| **New Only** | Only exists in new stack (no legacy equivalent) |

### 5. Generate Report

Output a markdown table with the following columns:

| Entity | Legacy Controller | Legacy Views | API Controller | Angular Page | Status | Priority |
|--------|-------------------|--------------|----------------|--------------|--------|----------|

### 6. Priority Assessment

For unmigrated entities, suggest priority based on:
- **High**: Core business entities (TreatmentBMP, Project, OnlandVisualTrashAssessment, WaterQualityManagementPlan)
- **Medium**: Supporting entities with moderate complexity (LandUseBlock, Delineation, TrashGeneratingUnit)
- **Low**: Simple lookup/reference entities

Also consider:
- Dependencies on other entities (migrate dependencies first)
- User impact (frequently used pages = higher priority)
- Complexity (simpler entities = quick wins)

### 7. Recommendations

After the status table, provide:
1. **Quick wins**: Simple entities that can be migrated quickly
2. **Dependencies**: Entities that should be migrated in a specific order
3. **Complex migrations**: Entities requiring significant effort (workflows, maps, complex grids)

## Output Format

```markdown
# Migration Status Report

Generated: {current date}

## Summary
- Total Legacy Controllers: X
- Migrated: Y
- Partial: Z
- Not Started: W

## Detailed Status

| Entity | Legacy | Views | API | Angular | Status | Priority |
|--------|--------|-------|-----|---------|--------|----------|
| TreatmentBMP | Yes | 12 | Yes | Yes | Migrated | - |
| Project | Yes | 8 | Yes | Partial | Partial | High |
| OVTA | Yes | 5 | No | No | Not Started | High |
| ... | ... | ... | ... | ... | ... | ... |

## Recommendations

### Quick Wins (Low Complexity, High Value)
1. {EntityName} - {reason}

### Dependencies to Address First
1. {EntityName} should be migrated before {OtherEntity}

### Complex Migrations (Plan Carefully)
1. {EntityName} - {complexity factors: workflow, maps, large grid, etc.}
```

## Notes

- Ignore controllers that are infrastructure-related (Home, Account, Error, etc.)
- Focus on domain entity controllers
- Check for `[Obsolete]` markers on legacy controllers (already migrated)
- Consider shared components that may need to be created first
