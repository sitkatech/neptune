---
name: crud-modal
description: Create CRUD modals with forms, validation, and permission checks for an entity. Use when building create/edit/delete UI for a database entity.
allowed-tools: [Read, Glob, Grep, Edit, Write, Bash(dotnet build:*), Bash(npm run gen-model:*), Bash(dotnet test:*), Bash(npm test:*)]
argument-hint: <EntityName>
---

# CRUD Modal Skill

When the user invokes `/crud-modal <EntityName>`:

## Overview

This skill guides the creation of CRUD (Create, Read, Update, Delete) modals with forms, validation, and permission checks for the Angular application.

---

## 1. Analyze Legacy Forms

First, examine the legacy MVC implementation:

### Find Legacy Forms

- **Edit views**: `Neptune.WebMvc/Views/{Entity}/Edit.cshtml`
- **New views**: `Neptune.WebMvc/Views/{Entity}/New.cshtml`
- **Modal partials**: `Neptune.WebMvc/Views/{Entity}/*Modal*.cshtml`
- **ViewModels**: `Neptune.WebMvc/Models/{Entity}ViewModel.cs`

### Document Form Fields

Create a field inventory:

| # | Field Label | Field Name | Type | Required | Validation | Notes |
|---|-------------|------------|------|----------|------------|-------|
| 1 | Name | EntityName | Text | Yes | Max 200 chars | - |
| 2 | Description | Description | Textarea | No | Max 4000 chars | - |
| 3 | Start Date | StartDate | Date | Yes | Must be valid date | - |
| 4 | Category | CategoryID | Dropdown | Yes | Must exist | FK to Category |
| 5 | Amount | Amount | Currency | No | Min 0, Max 9999999 | 2 decimals |
| 6 | Is Active | IsActive | Checkbox | No | - | Default: true |

### Identify Validation Rules

- Look for `[Required]`, `[StringLength]`, `[Range]` attributes
- Check for custom validation logic in controller or ViewModel
- Note any conditional validation rules

---

## 2. Create Upsert DTO

### DTO with Validation Attributes

```csharp
// In Neptune.Models/DataTransferObjects/EntityUpsertDto.cs
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class EntityUpsertDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string EntityName { get; set; }

    [StringLength(4000, ErrorMessage = "Description cannot exceed 4000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Start Date is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public int CategoryID { get; set; }

    [Range(0, 9999999.99, ErrorMessage = "Amount must be between 0 and 9,999,999.99")]
    public decimal? Amount { get; set; }

    public bool IsActive { get; set; } = true;

    // For creating related records
    public List<int>? RelatedEntityIDs { get; set; }
}
```

---

## 3. Add API Endpoints

### Controller Endpoints

```csharp
// In Neptune.API/Controllers/EntityController.cs

[HttpPost]
[AdminFeature]
public async Task<ActionResult<EntityDto>> Create([FromBody] EntityUpsertDto dto)
{
    var entity = Entities.CreateNew(DbContext, dto);
    var detail = Entities.GetByIDAsDto(DbContext, entity.EntityID);
    return Ok(detail);
}

[HttpPut("{entityID}")]
[AdminFeature]
public async Task<ActionResult<EntityDto>> Update(
    [FromRoute] int entityID,
    [FromBody] EntityUpsertDto dto)
{
    var entity = Entities.GetByIDWithChangeTracking(DbContext, entityID);
    Entities.Update(DbContext, entity, dto);
    var detail = Entities.GetByIDAsDto(DbContext, entity.EntityID);
    return Ok(detail);
}

[HttpDelete("{entityID}")]
[AdminFeature]
public async Task<IActionResult> Delete([FromRoute] int entityID)
{
    var entity = Entities.GetByIDWithChangeTracking(DbContext, entityID);
    await Entities.DeleteAsync(DbContext, entity);
    return Ok();
}
```

### Permission Attributes

| Attribute | Roles |
|-----------|-------|
| `[AdminFeature]` | Admin, SitkaAdmin |
| `[SitkaAdminFeature]` | SitkaAdmin only |
| `[JurisdictionEditFeature]` | Admin, SitkaAdmin, JurisdictionManager, JurisdictionEditor (+ jurisdiction match) |
| `[UserViewFeature]` | Any authenticated user |
| `[LoggedInUnclassifiedFeature]` | Unassigned users |

---

## 4. Create Static Helper Methods

```csharp
// In Neptune.EFModels/Entities/{PluralEntity}.cs

public static Entity CreateNew(NeptuneDbContext dbContext, EntityUpsertDto dto)
{
    var entity = new Entity
    {
        EntityName = dto.EntityName,
        Description = dto.Description,
        StartDate = dto.StartDate,
        CategoryID = dto.CategoryID,
        Amount = dto.Amount,
        IsActive = dto.IsActive,
        CreateDate = DateTime.UtcNow,
        CreatePersonID = dbContext.GetCurrentPersonID()
    };

    dbContext.Entities.Add(entity);
    dbContext.SaveChanges();

    // Handle related entities if needed
    if (dto.RelatedEntityIDs?.Any() == true)
    {
        foreach (var relatedID in dto.RelatedEntityIDs)
        {
            dbContext.EntityRelations.Add(new EntityRelation
            {
                EntityID = entity.EntityID,
                RelatedEntityID = relatedID
            });
        }
        dbContext.SaveChanges();
    }

    return entity;
}

public static void Update(NeptuneDbContext dbContext, Entity entity, EntityUpsertDto dto)
{
    entity.EntityName = dto.EntityName;
    entity.Description = dto.Description;
    entity.StartDate = dto.StartDate;
    entity.CategoryID = dto.CategoryID;
    entity.Amount = dto.Amount;
    entity.IsActive = dto.IsActive;
    entity.UpdateDate = DateTime.UtcNow;
    entity.UpdatePersonID = dbContext.GetCurrentPersonID();

    dbContext.SaveChanges();
}

public static async Task DeleteAsync(NeptuneDbContext dbContext, Entity entity)
{
    // Remove related records first if needed
    var relations = await dbContext.EntityRelations
        .Where(x => x.EntityID == entity.EntityID)
        .ToListAsync();
    dbContext.EntityRelations.RemoveRange(relations);

    dbContext.Entities.Remove(entity);
    await dbContext.SaveChangesAsync();
}
```

---

## 5. Create Modal Component

### Component Files

Create the following files:
- `Neptune.Web/src/app/pages/{entity}/{entity}-modal/{entity}-modal.component.ts`
- `Neptune.Web/src/app/pages/{entity}/{entity}-modal/{entity}-modal.component.html`
- `Neptune.Web/src/app/pages/{entity}/{entity}-modal/{entity}-modal.component.scss`

### Generated Form Helpers

The OpenAPI code generator produces reactive form helpers for every UpsertDto:
- `{Entity}UpsertDto` - Class with constructor that accepts form values
- `{Entity}UpsertDtoForm` - Interface with typed FormControls
- `{Entity}UpsertDtoFormControls` - Static factory methods for creating FormControls

### TypeScript Component

```typescript
// {entity}-modal.component.ts
import { Component, inject, OnInit } from "@angular/core";
import { DialogRef } from "@ngneat/dialog";
import { FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/forms/form-field/form-field.component";
import { ModalAlertsComponent } from "src/app/shared/components/modal/modal-alerts.component";
import { BaseModal } from "src/app/shared/components/modal/base-modal";
import { AlertService } from "src/app/shared/services/alert.service";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";

import { EntityService } from "src/app/shared/generated/api/entity.service";
import { EntityDto } from "src/app/shared/generated/model/entity-dto";
import { CategorySimpleDto } from "src/app/shared/generated/model/category-simple-dto";

// Import generated form helpers
import {
    EntityUpsertDto,
    EntityUpsertDtoForm,
    EntityUpsertDtoFormControls
} from "src/app/shared/generated/model/entity-upsert-dto";

export interface EntityModalData {
    mode: "create" | "edit";
    entity?: EntityDto;
    categories: CategorySimpleDto[];
}

@Component({
    selector: "entity-modal",
    standalone: true,
    imports: [ReactiveFormsModule, FormFieldComponent, ModalAlertsComponent],
    templateUrl: "./entity-modal.component.html",
    styleUrls: ["./entity-modal.component.scss"]
})
export class EntityModalComponent extends BaseModal implements OnInit {
    public ref: DialogRef<EntityModalData, EntityDto | null> = inject(DialogRef);

    public FormFieldType = FormFieldType;
    public mode: "create" | "edit" = "create";
    public entity?: EntityDto;
    public categories: CategorySimpleDto[] = [];
    public isSubmitting = false;

    // Use generated form interface for type safety
    public form = new FormGroup<EntityUpsertDtoForm>({
        EntityName: EntityUpsertDtoFormControls.EntityName("", {
            validators: [Validators.required, Validators.maxLength(200)]
        }),
        Description: EntityUpsertDtoFormControls.Description(""),
        StartDate: EntityUpsertDtoFormControls.StartDate(null, {
            validators: [Validators.required]
        }),
        CategoryID: EntityUpsertDtoFormControls.CategoryID(null, {
            validators: [Validators.required]
        }),
        Amount: EntityUpsertDtoFormControls.Amount(null, {
            validators: [Validators.min(0), Validators.max(9999999.99)]
        }),
        IsActive: EntityUpsertDtoFormControls.IsActive(true),
    });

    constructor(
        private entityService: EntityService,
        alertService: AlertService
    ) {
        super(alertService);
    }

    ngOnInit(): void {
        const data = this.ref.data;
        this.mode = data?.mode ?? "create";
        this.entity = data?.entity;
        this.categories = data?.categories ?? [];

        if (this.mode === "edit" && this.entity) {
            this.form.patchValue({
                EntityName: this.entity.EntityName,
                Description: this.entity.Description,
                StartDate: this.entity.StartDate ? new Date(this.entity.StartDate) : null,
                CategoryID: this.entity.Category?.CategoryID,
                Amount: this.entity.Amount,
                IsActive: this.entity.IsActive
            });
        }
    }

    get modalTitle(): string {
        return this.mode === "create" ? "New Entity" : "Edit Entity";
    }

    save(): void {
        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        this.isSubmitting = true;
        this.localAlerts = [];

        const dto = new EntityUpsertDto(this.form.value);

        const request$ = this.mode === "create"
            ? this.entityService.createEntity(dto)
            : this.entityService.updateEntity(this.entity!.EntityID, dto);

        request$.subscribe({
            next: (result) => {
                const message = this.mode === "create"
                    ? "Entity created successfully."
                    : "Entity updated successfully.";
                this.pushGlobalSuccess(message);
                this.ref.close(result);
            },
            error: (err) => {
                this.isSubmitting = false;
                const message = err?.error?.message ?? err?.message ?? "An error occurred.";
                this.addLocalAlert(message, AlertContext.Danger, true);
            }
        });
    }

    cancel(): void {
        this.ref.close(null);
    }
}
```

### HTML Template

```html
<!-- {entity}-modal.component.html -->
<div class="modal-header">
    <h3>{{ modalTitle }}</h3>
</div>
<div class="modal-body">
    <!-- Modal-local alerts -->
    <modal-alerts [alerts]="localAlerts" (onClosed)="removeLocalAlert($event)"></modal-alerts>

    <form [formGroup]="form">
        <!-- Text input -->
        <form-field
            [formControl]="form.controls.EntityName"
            fieldLabel="Name"
            [type]="FormFieldType.Text"
            [required]="true"
            placeholder="Enter name">
        </form-field>

        <!-- Textarea -->
        <form-field
            [formControl]="form.controls.Description"
            fieldLabel="Description"
            [type]="FormFieldType.Textarea"
            placeholder="Enter description">
        </form-field>

        <!-- Date input -->
        <form-field
            [formControl]="form.controls.StartDate"
            fieldLabel="Start Date"
            [type]="FormFieldType.Date"
            [required]="true">
        </form-field>

        <!-- Dropdown/Select -->
        <form-field
            [formControl]="form.controls.CategoryID"
            fieldLabel="Category"
            [type]="FormFieldType.Select"
            [required]="true"
            [selectOptions]="categories"
            selectLabelField="CategoryName"
            selectValueField="CategoryID"
            placeholder="Select a category">
        </form-field>

        <!-- Number input -->
        <form-field
            [formControl]="form.controls.Amount"
            fieldLabel="Amount"
            [type]="FormFieldType.Number"
            placeholder="0.00">
        </form-field>

        <!-- Checkbox -->
        <form-field
            [formControl]="form.controls.IsActive"
            fieldLabel="Active"
            [type]="FormFieldType.Checkbox">
        </form-field>
    </form>
</div>
<div class="modal-footer">
    <button
        class="btn btn-primary"
        (click)="save()"
        [disabled]="isSubmitting">
        {{ isSubmitting ? 'Saving...' : 'Save' }}
    </button>
    <button
        class="btn btn-secondary"
        (click)="cancel()"
        [disabled]="isSubmitting">
        Cancel
    </button>
</div>
```

---

## 6. FormFieldType Reference

```typescript
export enum FormFieldType {
    Text = "text",
    Textarea = "textarea",
    Number = "number",
    Date = "date",
    Select = "select",
    Checkbox = "checkbox",
    Email = "email",
    Phone = "phone",
    Password = "password",
    MultiSelect = "multiselect"
}
```

---

## 7. Opening the Modal

### From Parent Component

```typescript
import { DialogService } from "@ngneat/dialog";
import { EntityModalComponent, EntityModalData } from "./entity-modal/entity-modal.component";

constructor(
    private dialogService: DialogService,
    private entityService: EntityService
) {}

openCreateModal(): void {
    this.categoryService.listCategories().subscribe(categories => {
        const dialogRef = this.dialogService.open(EntityModalComponent, {
            data: {
                mode: "create",
                categories: categories
            } as EntityModalData,
            size: "md"
        });

        dialogRef.afterClosed$.subscribe(result => {
            if (result) {
                this.refreshData();
            }
        });
    });
}

openEditModal(entity: EntityDto): void {
    this.categoryService.listCategories().subscribe(categories => {
        const dialogRef = this.dialogService.open(EntityModalComponent, {
            data: {
                mode: "edit",
                entity: entity,
                categories: categories
            } as EntityModalData,
            size: "md"
        });

        dialogRef.afterClosed$.subscribe(result => {
            if (result) {
                this.refreshData();
            }
        });
    });
}
```

---

## 8. Delete Confirmation Pattern

```typescript
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";

constructor(private confirmService: ConfirmService) {}

async confirmDelete(entity: EntityDto): Promise<void> {
    const confirmed = await this.confirmService.confirm({
        title: "Delete Entity",
        message: `Are you sure you want to delete "${entity.EntityName}"? This action cannot be undone.`,
        buttonTextYes: "Delete",
        buttonClassYes: "btn-danger",
        buttonTextNo: "Cancel"
    });

    if (confirmed) {
        this.entityService.deleteEntity(entity.EntityID).subscribe({
            next: () => {
                this.alertService.pushAlert(new Alert(
                    "Entity deleted successfully.",
                    AlertContext.Success,
                    true
                ));
                this.refreshData();
            },
            error: (err) => {
                this.alertService.pushAlert(new Alert(
                    err?.error?.message ?? "Failed to delete entity.",
                    AlertContext.Danger,
                    true
                ));
            }
        });
    }
}
```

---

## 9. Permission Checks (Frontend)

### Using AuthenticationService

```typescript
import { AuthenticationService } from "src/app/shared/services/authentication.service";

constructor(private authService: AuthenticationService) {}

public canEdit = false;
public canDelete = false;

ngOnInit(): void {
    this.authService.getCurrentUser().subscribe(user => {
        this.canEdit = this.authService.isCurrentUserAnAdministrator();
        this.canDelete = this.authService.isCurrentUserAnAdministrator();
    });
}
```

### Common Permission Patterns

```typescript
// Admin check
const isAdmin = this.authService.isCurrentUserAnAdministrator();

// Jurisdiction edit check
const canEditJurisdiction = this.authService.doesCurrentUserHaveJurisdictionEditPermission();

// Combined permissions
const canEdit = isAdmin || canEditJurisdiction;
```

---

## 10. Migration Checklist

### Backend

- [ ] Created `{Entity}UpsertDto` with validation attributes
- [ ] Added `Create` endpoint to controller
- [ ] Added `Update` endpoint to controller
- [ ] Added `Delete` endpoint to controller
- [ ] Created `CreateNew` static helper
- [ ] Created `Update` static helper
- [ ] Created `DeleteAsync` static helper
- [ ] Added appropriate permission attributes
- [ ] Ran `dotnet build Neptune.API` to generate swagger.json

### Frontend

- [ ] Ran `npm run gen-model` to generate TypeScript models
- [ ] Created modal component files
- [ ] Implemented form with all fields
- [ ] Implemented save logic for create/edit modes
- [ ] Implemented cancel logic
- [ ] Added modal-local error handling
- [ ] Added loading/submitting state
- [ ] Added delete confirmation (if applicable)
- [ ] Added Edit/Delete buttons with permission checks
- [ ] Tested create flow
- [ ] Tested edit flow
- [ ] Tested delete flow
- [ ] Tested validation errors display
- [ ] Tested permission checks
