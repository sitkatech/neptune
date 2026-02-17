# Database Patterns

Load this skill when working with `.sql` files, database schema changes, or migrations in Neptune.Database.

## Cross-References

| After schema changes... | Load |
|-------------------------|------|
| Creating API endpoints | See `api-patterns.md` |
| Regenerating frontend models | Run `npm run gen-model` in Neptune.Web |

---

## Migration Script Template

Use release scripts in `Neptune.Database/Scripts/ReleaseScripts/` or `PreReleaseScripts/`:

```sql
DECLARE @MigrationName VARCHAR(200);
SET @MigrationName = '0001 - Description'

IF NOT EXISTS(SELECT * FROM dbo.DatabaseMigration DM WHERE DM.ReleaseScriptFileName = @MigrationName)
BEGIN
    -- Migration logic here

    INSERT INTO dbo.DatabaseMigration(MigrationAuthorName, ReleaseScriptFileName, MigrationReason)
    SELECT 'AuthorName', @MigrationName, 'Reason'
END
```

**Naming convention**: Scripts are numbered sequentially (001, 002, etc.) with a short description.

---

## Code Generation Pipeline

After making database changes, run this pipeline:

1. **Add/modify tables** in `Neptune.Database/dbo/Tables/`
2. **Run `Build/Scaffold.ps1`** to regenerate:
   - EF entities in `Neptune.EFModels/Entities/Generated/`
   - Extension methods in `Neptune.EFModels/Entities/Generated/ExtensionMethods/`
   - TypeScript enums in `Neptune.Web/src/app/shared/generated/enum/`
3. **Build the API** to regenerate `swagger.json`
4. **Run `npm run gen-model`** in Neptune.Web to regenerate TypeScript API clients

### Commands

```powershell
# From Build/ directory
.\Scaffold.ps1                    # Regenerate EF models
.\DatabaseBuild.ps1               # Build and deploy database project
```

---

## Table Conventions

- Primary keys: `{TableName}ID` (e.g., `ProjectID`, `TreatmentBMPID`)
- Foreign keys: Match the referenced table's primary key name
- Audit columns: `CreateDate`, `CreatePersonID`, `UpdateDate`, `UpdatePersonID` where needed
- Soft delete: Use `IsActive` or similar flag rather than hard deletes where appropriate

---

## Adding a New Table

1. Create table definition in `Neptune.Database/dbo/Tables/{TableName}.sql`
2. Add any foreign key constraints
3. Create release script for initial data if needed
4. Run `Build/DatabaseBuild.ps1` to deploy
5. Run `Build/Scaffold.ps1` to generate EF models
6. Create static helpers and extension methods (see `api-patterns.md`)
