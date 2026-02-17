---
name: migrate-grid
description: Migrate data grids from legacy MVC to Angular using neptune-grid and ag-Grid. Ensures complete column parity with legacy.
allowed-tools: [Read, Glob, Grep, Edit, Write, Bash(dotnet build:*), Bash(npm run gen-model:*)]
argument-hint: <EntityName> <GridName>
---

# Migrate Grid Skill

When the user invokes `/migrate-grid <EntityName> <GridName>`:

## Overview

This skill guides the migration of data grids from legacy MVC views to Angular using the `neptune-grid` component, ensuring complete column parity with the legacy implementation.

---

## 1. Analyze Legacy Grid Implementation

First, thoroughly examine the legacy MVC grid:

### Find Legacy Grid Views

- **Index views**: `Neptune.WebMvc/Views/{Entity}/Index.cshtml`
- **Detail views**: `Neptune.WebMvc/Views/{Entity}/Detail.cshtml`
- **Partial views**: `Neptune.WebMvc/Views/{Entity}/*Grid*.cshtml`
- **Shared partials**: `Neptune.WebMvc/Views/Shared/*Grid*.cshtml`

Note: Neptune's legacy grids use DhtmlxGrid. Look for grid initialization patterns in the views.

### Document Every Column

Create a column inventory table:

| # | Header | Field/Expression | Type | Link? | Filter? | Notes |
|---|--------|------------------|------|-------|---------|-------|
| 1 | Name | TreatmentBMPName | Text | Yes, to detail | Yes | - |
| 2 | Status | StatusName | Text | No | Dropdown | - |
| 3 | Date | CreatedDate | Date | No | Date range | Format: M/d/yyyy |
| 4 | Area (ac) | DelineationArea | Decimal | No | Number | 2 decimals |
| 5 | Actions | - | Actions | Yes | No | Edit, Delete |

### Identify Data Source

- Look for grid data controller actions
- Check for any complex joins or calculations
- Note any spatial data transformations

---

## 2. Design Grid Row DTO

### Naming Convention

- Grid DTOs: `{Entity}GridDto`
- Context-specific grid DTOs: `{Entity}{Context}GridDto`
- Examples:
  - `TreatmentBMPGridDto` — main BMP grid
  - `ProjectTreatmentBMPGridDto` — BMPs on project detail page

### DTO Structure

```csharp
// In Neptune.Models/DataTransferObjects/{Entity}GridDto.cs
namespace Neptune.Models.DataTransferObjects;

public class EntityGridDto
{
    // Primary key for row identification
    public int EntityID { get; set; }

    // Simple text fields
    public string Name { get; set; }
    public string Description { get; set; }

    // Related entity names (denormalized for grid display)
    public string? JurisdictionName { get; set; }
    public string? StatusName { get; set; }

    // Formatted/calculated fields
    public decimal? TotalArea { get; set; }
    public DateTime? CreatedDate { get; set; }

    // Boolean fields
    public bool IsActive { get; set; }
}
```

---

## 3. Create Extension Method

The `AsGridDto()` extension method is defined in `Neptune.EFModels/Entities/Generated/ExtensionMethods/`:

```csharp
public static EntityGridDto AsGridDto(this Entity entity)
{
    return new EntityGridDto
    {
        EntityID = entity.EntityID,
        Name = entity.Name,
        Description = entity.Description,
        JurisdictionName = entity.StormwaterJurisdiction?.Organization?.OrganizationName,
        StatusName = entity.Status?.StatusDisplayName,
        TotalArea = entity.DelineationArea,
        CreatedDate = entity.CreateDate,
        IsActive = entity.IsActive ?? false
    };
}
```

---

## 4. Create Static Helper Method

```csharp
// In Neptune.EFModels/Entities/{PluralEntity}.cs
public static class Entities
{
    public static List<EntityGridDto> ListAsGridDto(NeptuneDbContext dbContext)
    {
        return dbContext.Entities
            .AsNoTracking()
            .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(j => j.Organization)
            .Include(x => x.Status)
            .Select(x => x.AsGridDto())
            .ToList();
    }

    // For filtered grids (e.g., entities for a specific parent)
    public static List<EntityGridDto> ListByParentIDAsGridDto(
        NeptuneDbContext dbContext, int parentID)
    {
        return dbContext.Entities
            .AsNoTracking()
            .Where(x => x.ParentID == parentID)
            .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(j => j.Organization)
            .Select(x => x.AsGridDto())
            .ToList();
    }
}
```

---

## 5. Add API Endpoint

```csharp
// In Neptune.API/Controllers/{Entity}Controller.cs
[HttpGet]
[UserViewFeature]
public ActionResult<List<EntityGridDto>> List()
{
    var entities = Entities.ListAsGridDto(DbContext);
    return Ok(entities);
}

// For child grids on parent detail pages
[HttpGet("{parentID}/entities")]
[UserViewFeature]
public ActionResult<List<EntityGridDto>> ListByParent([FromRoute] int parentID)
{
    var entities = Entities.ListByParentIDAsGridDto(DbContext, parentID);
    return Ok(entities);
}
```

---

## 6. Create Angular Column Definitions

### Import ag-Grid Helper

```typescript
import { AgGridHelper } from "src/app/shared/helpers/ag-grid-helper";
import { ColDef } from "ag-grid-community";
import { EntityGridDto } from "src/app/shared/generated/model/entity-grid-dto";

public columnDefs: ColDef<EntityGridDto>[] = [];

ngOnInit(): void {
    this.columnDefs = this.createColumnDefs();
}
```

### Column Definition Methods Reference

| Method | Use Case | Example |
|--------|----------|---------|
| `createBasicColumnDef` | Text, nested object fields | Name, Description |
| `createLinkColumnDef` | Single link to another entity | BMP Name -> BMP Detail |
| `createMultiLinkColumnDef` | Array of links | Tags, Programs |
| `createDateColumnDef` | Dates with format | Created Date |
| `createDecimalColumnDef` | Numbers with decimals | Area (2 decimal places) |
| `createCurrencyColumnDef` | Money values | Amount ($X,XXX) |
| `createBooleanColumnDef` | Yes/No values | Is Active |
| `createActionsColumnDef` | Edit/Delete buttons | Row actions |

### Column Definition Examples

```typescript
private createColumnDefs(): ColDef<EntityGridDto>[] {
    return [
        // Link column
        AgGridHelper.createLinkColumnDef("Name", "Name", "EntityID", {
            InRouterLink: "/entities/"
        }),

        // Basic text column
        AgGridHelper.createBasicColumnDef("Jurisdiction", "JurisdictionName", {
            CustomDropdownFilterField: "JurisdictionName"
        }),

        // Date column
        AgGridHelper.createDateColumnDef("Start Date", "StartDate", "M/d/yyyy"),

        // Decimal column
        AgGridHelper.createDecimalColumnDef("Area (ac)", "TotalArea", {
            MaxDecimalPlacesToDisplay: 2
        }),

        // Boolean column with dropdown filter
        AgGridHelper.createBooleanColumnDef("Active", "IsActive", {
            CustomDropdownFilterField: "IsActive",
        }),

        // Actions column
        AgGridHelper.createActionsColumnDef((params) => ({
            items: [
                {
                    name: "Edit",
                    icon: "Edit",
                    callback: () => this.openEditModal(params.data)
                },
                {
                    name: "Delete",
                    icon: "Delete",
                    callback: () => this.confirmDelete(params.data)
                }
            ]
        }))
    ];
}
```

### Dropdown Filters

**Always use `CustomDropdownFilterField`** for columns with a fixed set of values:

| Column Type | Use Dropdown Filter? |
|-------------|---------------------|
| Boolean (Yes/No) | Yes |
| Status/Stage | Yes |
| Type/Category | Yes |
| Linked entity name | Yes |
| Free-form text | No |
| Numbers | No |
| Dates | No |

### Column Alignment

**Right-aligned columns** (handled automatically by helper methods):
- Numbers (createDecimalColumnDef)
- Dates (createDateColumnDef)
- Currency (createCurrencyColumnDef)

**Left-aligned** (default): text columns

---

## 7. Angular Template Pattern

### Basic Grid

```html
<div class="card">
    <div class="card-header"><span class="card-title">Entities</span></div>
    <div class="card-body">
        <neptune-grid
            [rowData]="entities$ | async"
            [columnDefs]="columnDefs"
            [downloadFileName]="'entities'">
        </neptune-grid>
    </div>
</div>
```

### Grid with Height and Custom Options

```html
<neptune-grid
    [rowData]="entities$ | async"
    [columnDefs]="columnDefs"
    [height]="'400px'"
    [downloadFileName]="'entities'">
</neptune-grid>
```

---

## 8. Grid Component Input Reference

| Input | Type | Default | Description |
|-------|------|---------|-------------|
| `rowData` | `any[]` | - | Grid data array |
| `columnDefs` | `ColDef[]` | - | Column definitions |
| `height` | string | `'500px'` | Grid height |
| `downloadFileName` | string | `'grid-data'` | CSV export filename |
| `hideDownloadButton` | boolean | `false` | Hide CSV download button |
| `hideGlobalFilter` | boolean | `false` | Hide search box |
| `sizeColumnsToFitGrid` | boolean | `false` | Fit columns to grid width |
| `pagination` | boolean | `false` | Enable pagination |
| `paginationPageSize` | number | `100` | Rows per page |

---

## 9. Common Patterns

### Multiple Grids on One Page

```typescript
public bmpColumnDefs: ColDef<TreatmentBMPGridDto>[] = [];
public projectColumnDefs: ColDef<ProjectGridDto>[] = [];

ngOnInit(): void {
    this.bmpColumnDefs = this.createBMPColumnDefs();
    this.projectColumnDefs = this.createProjectColumnDefs();
}
```

### Refreshing Grid Data

```typescript
private refreshData$ = new Subject<void>();

this.entities$ = combineLatest([this.entityID$, this.refreshData$.pipe(startWith(undefined))]).pipe(
    switchMap(([id]) => this.entityService.listByParent(id)),
    shareReplay({ bufferSize: 1, refCount: true })
);

refreshGrid(): void {
    this.refreshData$.next();
}
```

---

## 10. Column Parity Checklist

Before considering migration complete, verify:

- [ ] All legacy columns are present in Angular grid
- [ ] Column headers match (or are intentionally improved)
- [ ] Data formats match:
  - [ ] Dates display in same format (M/d/yyyy)
  - [ ] Currency displays with proper formatting ($X,XXX)
  - [ ] Decimals show correct precision
  - [ ] Numbers have comma separators (1,234,567)
  - [ ] Booleans show Yes/No
- [ ] Column alignment:
  - [ ] Numbers are right-aligned
  - [ ] Dates are right-aligned
  - [ ] Currency is right-aligned
  - [ ] Text is left-aligned (default)
- [ ] Links navigate to correct routes
- [ ] Dropdown filters configured for lookup columns
- [ ] Column sort works correctly
- [ ] Actions column has all required actions

---

## 11. Migration Checklist

- [ ] Documented all legacy grid columns
- [ ] Created GridDto with all required fields
- [ ] Created/updated extension method (`AsGridDto`)
- [ ] Created static helper method(s)
- [ ] Added API endpoint(s)
- [ ] Ran `dotnet build Neptune.API` to generate swagger.json
- [ ] Ran `npm run gen-model` to generate TypeScript models
- [ ] Created column definitions using ag-Grid helper
- [ ] Added neptune-grid component to template
- [ ] Verified column parity with legacy grid
- [ ] Verified filtering works
- [ ] Verified sorting works
- [ ] Verified links navigate correctly
- [ ] Verified CSV export works
