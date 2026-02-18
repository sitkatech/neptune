# API Patterns

This document contains detailed C# patterns for the ASP.NET Core API layer.

---

## Controller Pattern

Controllers extend `SitkaController<T>` using primary constructor with three parameters:

```csharp
[ApiController]
[Route("api/[controller]")]
public class EntityController(NeptuneDbContext dbContext, ILogger<EntityController> logger, IOptions<NeptuneConfiguration> neptuneConfiguration)
    : SitkaController<EntityController>(dbContext, logger, neptuneConfiguration)
{
    [HttpGet]
    public async Task<ActionResult<List<EntityGridDto>>> List()
    {
        var entities = await Entities.ListAsDtoAsync(DbContext);
        return Ok(entities);
    }

    [HttpGet("{entityID}")]
    public async Task<ActionResult<EntityDto>> GetByID([FromRoute] int entityID)
    {
        var entity = Entities.GetByIDAsDto(DbContext, entityID);
        return Ok(entity);
    }
}
```

---

## DTO Naming Conventions

Use descriptive suffixes indicating the DTO's purpose:

- `{Entity}GridDto` — List/grid views with minimal fields
- `{Entity}Dto` — Full detail views with related data
- `{Entity}SimpleDto` — Simple projections (e.g., for nested objects, dropdowns)
- `{Entity}UpsertDto` — Create/update requests

DTOs live in `Neptune.Models/DataTransferObjects/` (flat directory, not per-entity subdirectories).

---

## Method Naming Conventions

Use verb prefixes that indicate return cardinality:

| Prefix | Returns | Examples |
|--------|---------|----------|
| `Get...` | Single item (or null) | `GetByID()`, `GetByIDAsDto()`, `GetByIDWithChangeTracking()` |
| `List...` | Collection (`List<T>`, `IEnumerable<T>`) | `ListAsDtoAsync()`, `ListAsGridDtoAsync()` |

**Method placement**: Place methods in the **target entity's** static helpers, not the filter entity's. The entity being queried owns its query logic:

```csharp
// GOOD - Each entity owns its queries
var projects = Projects.ListByPersonIDAsGridDto(dbContext, personID);
var bmps = TreatmentBMPs.ListByJurisdictionIDAsGridDto(dbContext, jurisdictionID);

// BAD - Person shouldn't know how to query other entities
var projects = People.ListProjectsForPerson(dbContext, personID);
```

---

## Static Helper Class Pattern

Create `{PluralEntity}.cs` in `Neptune.EFModels/Entities/`:

```csharp
public static class Entities
{
    public static List<EntityGridDto> ListAsGridDto(NeptuneDbContext dbContext)
    {
        return dbContext.Entities
            .AsNoTracking()
            .Include(x => x.RelatedEntity)
            .Select(x => x.AsGridDto())
            .ToList();
    }

    public static EntityDto? GetByIDAsDto(NeptuneDbContext dbContext, int entityID)
    {
        return dbContext.Entities
            .AsNoTracking()
            .Include(x => x.RelatedEntity)
            .Where(x => x.EntityID == entityID)
            .Select(x => x.AsDto())
            .SingleOrDefault();
    }

    public static Entity GetByID(NeptuneDbContext dbContext, int entityID)
    {
        return dbContext.Entities
            .Single(x => x.EntityID == entityID);
    }

    public static Entity GetByIDWithChangeTracking(NeptuneDbContext dbContext, int entityID)
    {
        return dbContext.Entities
            .Single(x => x.EntityID == entityID);
    }

    public static Entity CreateNew(NeptuneDbContext dbContext, EntityUpsertDto dto)
    {
        var entity = new Entity
        {
            // map from dto
            CreateDate = DateTime.UtcNow,
            CreatePersonID = dbContext.GetCurrentPersonID()
        };
        dbContext.Entities.Add(entity);
        dbContext.SaveChanges();
        return entity;
    }

    public static void Update(NeptuneDbContext dbContext, Entity entity, EntityUpsertDto dto)
    {
        // map from dto to entity
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdatePersonID = dbContext.GetCurrentPersonID();
        dbContext.SaveChanges();
    }

    public static async Task DeleteAsync(NeptuneDbContext dbContext, Entity entity)
    {
        dbContext.Entities.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}
```

---

## Extension Method Pattern

Neptune uses extension methods (NOT Projection classes) for entity-to-DTO conversion. Extension methods are defined in `Neptune.EFModels/Entities/Generated/ExtensionMethods/{Entity}ExtensionMethods.cs`:

```csharp
public static class EntityExtensionMethods
{
    public static EntityDto AsDto(this Entity entity)
    {
        return new EntityDto
        {
            EntityID = entity.EntityID,
            EntityName = entity.EntityName,
            Description = entity.Description,
            RelatedEntityName = entity.RelatedEntity?.Name,
            // Include all fields and related data
        };
    }

    public static EntityGridDto AsGridDto(this Entity entity)
    {
        return new EntityGridDto
        {
            EntityID = entity.EntityID,
            EntityName = entity.EntityName,
            // Only include fields needed for grid display
        };
    }

    public static EntitySimpleDto AsSimpleDto(this Entity entity)
    {
        return new EntitySimpleDto
        {
            EntityID = entity.EntityID,
            EntityName = entity.EntityName,
        };
    }
}
```

**Usage in static helpers:**
```csharp
// Extension methods are called on entities AFTER Include() chains load related data
var entities = dbContext.Entities
    .AsNoTracking()
    .Include(x => x.RelatedEntity)
    .Select(x => x.AsGridDto())
    .ToList();
```

**Key difference from Projection classes:** Extension methods operate on materialized entities (after Include), NOT as `Expression<Func<>>` passed to EF Core `.Select()`. This means the Include chains must load all related data the extension method needs.

---

## Query Optimization Rules

- **ALWAYS** use `.AsNoTracking()` for read-only queries
- **ALWAYS** use `.Include()` to load related data needed by extension methods, then `.Select(x => x.AsDto())`
- **NEVER** return EF entities directly from controllers
- **AVOID** N+1 queries by using `.Include()` for related data
- **NEVER** use `.Distinct()` on entities that contain geometry/geography columns — SQL Server cannot compare spatial types. Instead, select distinct IDs first, then query by those IDs:

```csharp
// BAD - Will fail if Entity has geometry columns
var items = dbContext.JoinTable
    .Where(j => j.SomeID == id)
    .Select(j => j.Entity)
    .Distinct()  // ERROR: geometry cannot be compared
    .Select(x => x.AsGridDto())
    .ToList();

// GOOD - Get distinct IDs first, then query
var entityIDs = dbContext.JoinTable
    .Where(j => j.SomeID == id)
    .Select(j => j.EntityID)
    .Distinct()
    .ToList();

var items = dbContext.Entities
    .AsNoTracking()
    .Where(e => entityIDs.Contains(e.EntityID))
    .Select(x => x.AsGridDto())
    .ToList();
```

- **NEVER** use `.Include()` or `.ThenInclude()` with lookup dictionary properties. Some entities have computed properties that access lookup tables via in-memory dictionaries (e.g., `ObservationTypeSpecification`, `MeasurementUnitType`). These are NOT navigation properties and cannot be used with EF Core's `.Include()`:

```csharp
// BAD - ObservationTypeSpecification is a lookup dictionary property, not a navigation
.Include(x => x.TreatmentBMPObservationType)
    .ThenInclude(x => x.ObservationTypeSpecification)  // ERROR: Invalid Include

// GOOD - Just include the entity, access the lookup property in memory after loading
.Include(x => x.TreatmentBMPObservationType)
// Then access: entity.TreatmentBMPObservationType.ObservationTypeSpecification (resolved via dictionary)
```

Lookup properties are defined in `Generated/ExtensionMethods/*.Binding.cs` files with patterns like:
```csharp
public SomeType SomeProperty => SomeType.AllLookupDictionary[SomePropertyID];
```

- **NEVER** use `AllLookupDictionary` inside EF Core projections (`.Select()` before `.ToListAsync()`). EF Core cannot translate dictionary access to SQL and will throw `InvalidOperationException` about constant expression references causing memory leaks:

```csharp
// BAD - Dictionary access inside EF projection (before ToListAsync)
var items = await dbContext.Entities
    .AsNoTracking()
    .Where(x => x.SomeID == id)
    .Select(x => new EntityDto
    {
        EntityID = x.EntityID,
        // ERROR: EF cannot translate this to SQL
        StatusName = Status.AllLookupDictionary[x.StatusID].StatusDisplayName
    })
    .ToListAsync();

// GOOD - Use extension methods which run in memory after materialization
var items = dbContext.Entities
    .AsNoTracking()
    .Where(x => x.SomeID == id)
    .Include(x => x.Status)
    .Select(x => x.AsDto())  // Extension method runs in memory
    .ToList();
```

**Detection pattern**: Search for `AllLookupDictionary[x.` in StaticHelpers files — if it appears inside a `.Select()` that's part of an EF query chain (before `.ToListAsync()`), it needs to be fixed.

---

## Two-Phase Projection Pattern

When extension methods aren't sufficient (e.g., complex aggregations needed in SQL), use a two-phase approach:

```csharp
public static async Task<EntityDto?> GetByIDAsDetailAsync(NeptuneDbContext dbContext, int entityID)
{
    // PHASE 1: Project raw data in SQL (only columns we need)
    var rawData = await dbContext.Entities
        .AsNoTracking()
        .Where(x => x.EntityID == entityID)
        .Select(x => new
        {
            x.EntityID,
            x.EntityName,
            x.StatusID,

            // Aggregate calculations done in SQL
            TotalArea = x.Delineations.Sum(d => (double?)d.DelineationArea) ?? 0,

            // Nested navigation - project raw values
            JurisdictionName = x.StormwaterJurisdiction != null
                ? x.StormwaterJurisdiction.Organization.OrganizationName
                : null,

            // Collections
            BMPs = x.TreatmentBMPs.Select(b => new { b.TreatmentBMPID, b.TreatmentBMPName }).ToList()
        })
        .FirstOrDefaultAsync();

    if (rawData == null) return null;

    // PHASE 2: Transform to DTO in memory (can use dictionaries, string formatting)
    return new EntityDto
    {
        EntityID = rawData.EntityID,
        EntityName = rawData.EntityName,
        StatusDisplayName = ProjectStatus.AllLookupDictionary[rawData.StatusID].ProjectStatusDisplayName,
        TotalArea = rawData.TotalArea,
        JurisdictionName = rawData.JurisdictionName,
        BMPs = rawData.BMPs.Select(b => new TreatmentBMPSimpleDto
        {
            TreatmentBMPID = b.TreatmentBMPID,
            TreatmentBMPName = b.TreatmentBMPName
        }).ToList()
    };
}
```

### Key Principles

| Principle | Explanation |
|-----------|-------------|
| **Project only needed columns** | Don't select entire entities — select only the fields you need |
| **Do aggregations in SQL** | Use `.Sum()`, `.Count()`, `.Any()` in the projection |
| **Project nested values flat** | Instead of `Parent.Grandparent.Name`, project as `GrandparentName` |
| **Use nullable types for optional paths** | `x.Parent != null ? x.Parent.Name : null` |
| **Transform in memory** | Dictionary lookups, string formatting, conditionals after `ToListAsync()` |

### What CAN'T Be Done in SQL Projections

These must be computed in memory (Phase 2):

1. **Lookup dictionary access**: `Status.AllLookupDictionary[x.StatusID]`
2. **String formatting with format specifiers**: `$"{number:0000}"`
3. **Instance methods on entities**: `entity.GetDisplayName()`
4. **Complex conditional logic** that depends on multiple computed values

---

## Authorization Attributes

Neptune uses `BaseAuthorizationAttribute` (implements `IAuthorizationFilter`) with derived attributes:

| Attribute | Granted Roles |
|-----------|---------------|
| `SitkaAdminFeature` | SitkaAdmin |
| `AdminFeature` | Admin, SitkaAdmin |
| `JurisdictionEditFeature` | Admin, SitkaAdmin, JurisdictionManager, JurisdictionEditor (+ jurisdiction match) |
| `JurisdictionManageFeature` | Admin, SitkaAdmin, JurisdictionManager (+ jurisdiction match) |
| `UserViewFeature` | Any authenticated user |
| `LoggedInUnclassifiedFeature` | Unassigned users |
| `TreatmentBMPViewFeature` | Users with view access to the BMP's jurisdiction |

Roles: Admin(1), SitkaAdmin(2), JurisdictionManager(3), JurisdictionEditor(4), Unassigned(5), Viewer(6), ExternalViewer(7).

```csharp
// Public endpoints
[HttpGet]
[AllowAnonymous]
public ActionResult<List<EntityGridDto>> List() { ... }

// Admin-only endpoints
[HttpPost]
[AdminFeature]
public ActionResult<EntityDto> Create([FromBody] EntityUpsertDto dto) { ... }

// Jurisdiction-scoped endpoints
[HttpPut("{entityID}")]
[JurisdictionEditFeature]
public ActionResult<EntityDto> Update([FromRoute] int entityID, [FromBody] EntityUpsertDto dto) { ... }

// Any authenticated user
[HttpGet("{entityID}")]
[UserViewFeature]
public ActionResult<EntityDto> GetByID([FromRoute] int entityID) { ... }
```
