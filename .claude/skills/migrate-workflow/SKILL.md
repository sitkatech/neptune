---
name: migrate-workflow
description: Migrate multi-step wizard workflows from legacy MVC to Angular with workflow progress tracking, step components, and state transitions.
allowed-tools: [Read, Glob, Grep, Edit, Write, Bash(dotnet build:*), Bash(npm run gen-model:*), Bash(dotnet test:*), Bash(npm test:*)]
argument-hint: <EntityName>
---

# Migrate Workflow Skill

When the user invokes `/migrate-workflow <EntityName>`:

This skill guides migration of multi-step wizard workflows from legacy MVC to Angular + ASP.NET Core API. Use it for entities with sequential data entry steps, such as projects, assessments, or multi-page forms.

## Prerequisites

- Basic entity CRUD already exists (or will be created as part of step 1)
- Reference implementations: Project workflow (Draft -> WIP -> Submitted -> Approved), OnlandVisualTrashAssessment workflow

---

## 1. Analyze Legacy Workflow

First, thoroughly examine the existing MVC implementation:

- Read the legacy controller: `Neptune.WebMvc/Controllers/{Entity}Controller.cs`
- Read workflow views in: `Neptune.WebMvc/Views/{Entity}/` (look for step-specific views)
- Identify workflow action methods (e.g., `Step1`, `Step2`, `Submit`)

Document for each step:
- Step name and purpose
- Fields/data collected
- Validation rules
- Dependencies (which steps must complete first)
- Step type: form entry, map selection, file upload, grid/list management, or review

### Step Type Reference

| Step Type | Pattern | Examples |
|-----------|---------|----------|
| Form entry | Reactive form, save to entity | Project Information, BMP Details |
| Map selection | Map component + geometry binding | Delineation Area, OVTA Boundary |
| File upload | `[FromForm]` + file processing | Document Attachments |
| Grid/list management | Add/remove items from collection | Add Treatment BMPs |
| Review/submit | Read-only summary + submit action | Review and Finalize |

---

## 2. Backend: Workflow Progress Class

Create `Neptune.EFModels/Workflows/{Entity}WorkflowProgress.cs`:

```csharp
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Workflows;

public static class {Entity}WorkflowProgress
{
    public enum {Entity}WorkflowStep
    {
        Step1Name,
        Step2Name,
        Step3Name,
        // ... all steps
    }

    public static async Task<{Entity}WorkflowProgressDto?> GetProgressAsync(
        NeptuneDbContext dbContext,
        int {entity}ID)
    {
        var ctx = await LoadWorkflowContextAsync(dbContext, {entity}ID);
        if (ctx == null) return null;

        return new {Entity}WorkflowProgressDto
        {
            {Entity}ID = ctx.{Entity}ID,
            Name = ctx.Name,
            Steps = Enum.GetValuesAsUnderlyingType<{Entity}WorkflowStep>()
                .Cast<{Entity}WorkflowStep>()
                .ToDictionary(
                    step => step,
                    step => new WorkflowStepStatus
                    {
                        Completed = IsStepComplete(ctx, step),
                        Disabled = !IsStepActive(ctx, step)
                    })
        };
    }

    public static async Task<bool> CanSubmitAsync(NeptuneDbContext dbContext, int {entity}ID)
    {
        var ctx = await LoadWorkflowContextAsync(dbContext, {entity}ID);
        if (ctx == null) return false;

        return Enum.GetValuesAsUnderlyingType<{Entity}WorkflowStep>()
            .Cast<{Entity}WorkflowStep>()
            .All(step => IsStepComplete(ctx, step));
    }

    private static bool IsStepActive({Entity}WorkflowContext ctx, {Entity}WorkflowStep step)
    {
        return step switch
        {
            {Entity}WorkflowStep.Step1Name => true,
            _ => ctx.{Entity}ID > 0
        };
    }

    private static bool IsStepComplete({Entity}WorkflowContext ctx, {Entity}WorkflowStep step)
    {
        return step switch
        {
            {Entity}WorkflowStep.Step1Name => true,
            {Entity}WorkflowStep.Step2Name => ctx.SomeCount > 0,
            _ => throw new ArgumentOutOfRangeException(nameof(step))
        };
    }

    internal sealed class {Entity}WorkflowContext
    {
        public int {Entity}ID { get; init; }
        public string? Name { get; init; }
        // ... counts and flags for step completion checks
    }

    internal static async Task<{Entity}WorkflowContext?> LoadWorkflowContextAsync(
        NeptuneDbContext dbContext,
        int {entity}ID)
    {
        return await dbContext.{Entities}
            .AsNoTracking()
            .Where(x => x.{Entity}ID == {entity}ID)
            .Select(x => new {Entity}WorkflowContext
            {
                {Entity}ID = x.{Entity}ID,
                Name = x.Name,
            })
            .SingleOrDefaultAsync();
    }
}
```

---

## 3. Backend: Controller Workflow Endpoints

Add workflow endpoints to `Neptune.API/Controllers/{Entity}Controller.cs`:

```csharp
#region Workflow

[HttpGet("{id}/workflow/progress")]
[JurisdictionEditFeature]
public async Task<ActionResult<{Entity}WorkflowProgressDto>> GetWorkflowProgress([FromRoute] int id)
{
    var progress = await {Entity}WorkflowProgress.GetProgressAsync(DbContext, id);
    if (progress == null) return NotFound();
    return Ok(progress);
}

[HttpPut("{id}/workflow/steps/{step-name}")]
[JurisdictionEditFeature]
public async Task<IActionResult> Save{StepName}([FromRoute] int id, [FromBody] StepUpsertDto request)
{
    var entity = Entities.GetByIDWithChangeTracking(DbContext, id);
    Entities.Update{StepName}(DbContext, entity, request);
    return NoContent();
}

[HttpPost("{id}/workflow/submit")]
[JurisdictionEditFeature]
public async Task<IActionResult> SubmitWorkflow([FromRoute] int id)
{
    var canSubmit = await {Entity}WorkflowProgress.CanSubmitAsync(DbContext, id);
    if (!canSubmit) return BadRequest("Not all steps are complete.");

    var entity = Entities.GetByIDWithChangeTracking(DbContext, id);
    Entities.SubmitWorkflow(DbContext, entity);
    return NoContent();
}

#endregion
```

---

## 4. Backend: State Transitions

Workflows typically have a status lifecycle. Neptune's existing workflows use these patterns:

### Status Enums

- `ProjectStatusEnum`: Draft, PendingReview, Submitted, Approved
- `OnlandVisualTrashAssessmentStatusEnum`: InProgress, Complete, Unfinished

### State Transition Methods

Add to `Neptune.EFModels/Entities/{Entities}.cs`:

```csharp
#region State Transitions

public static void Submit(NeptuneDbContext dbContext, int {entity}ID, int callingPersonID)
{
    var entity = GetByIDWithChangeTracking(dbContext, {entity}ID);
    entity.{Entity}StatusID = (int){Entity}StatusEnum.Submitted;
    entity.UpdateDate = DateTime.UtcNow;
    entity.UpdatePersonID = callingPersonID;
    dbContext.SaveChanges();
}

public static void Approve(NeptuneDbContext dbContext, int {entity}ID, int callingPersonID)
{
    var entity = GetByIDWithChangeTracking(dbContext, {entity}ID);
    entity.{Entity}StatusID = (int){Entity}StatusEnum.Approved;
    entity.UpdateDate = DateTime.UtcNow;
    entity.UpdatePersonID = callingPersonID;
    dbContext.SaveChanges();
}

public static void Return(NeptuneDbContext dbContext, int {entity}ID, int callingPersonID)
{
    var entity = GetByIDWithChangeTracking(dbContext, {entity}ID);
    entity.{Entity}StatusID = (int){Entity}StatusEnum.Draft;
    entity.UpdateDate = DateTime.UtcNow;
    entity.UpdatePersonID = callingPersonID;
    dbContext.SaveChanges();
}

#endregion
```

---

## 5. Frontend: Workflow Components

Neptune has shared workflow components at `src/app/shared/components/`:

| Component | Purpose |
|-----------|---------|
| `workflow-body` | Main content area for workflow steps |
| `workflow-nav` | Navigation sidebar for workflow steps |
| `workflow-nav-item` | Individual step in workflow nav (shows completion state) |
| `workflow-help` | Help/info panel for workflow steps |

### Workflow Outlet Component

Create `Neptune.Web/src/app/pages/{entity}/workflow/{entity}-workflow-outlet/`:

```typescript
import { Component, Input, OnInit } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { Observable } from "rxjs";
import { AsyncPipe } from "@angular/common";
import { WorkflowNavComponent } from "src/app/shared/components/workflow-nav/workflow-nav.component";
import { WorkflowNavItemComponent } from "src/app/shared/components/workflow-nav/workflow-nav-item/workflow-nav-item.component";
import { {Entity}Service } from "src/app/shared/generated/api/{entity}.service";

@Component({
    selector: "{entity}-workflow-outlet",
    standalone: true,
    imports: [RouterOutlet, AsyncPipe, WorkflowNavComponent, WorkflowNavItemComponent],
    templateUrl: "./{entity}-workflow-outlet.component.html",
    styleUrls: ["./{entity}-workflow-outlet.component.scss"],
})
export class {Entity}WorkflowOutletComponent implements OnInit {
    @Input() {entity}ID: number | null = null;
    public progress$: Observable<{Entity}WorkflowProgressDto>;

    constructor(private {entity}Service: {Entity}Service) {}

    ngOnInit() {
        if (this.{entity}ID) {
            this.progress$ = this.{entity}Service.getWorkflowProgress(this.{entity}ID);
        }
    }
}
```

### Workflow Outlet Template

```html
<div class="workflow dashboard">
    <aside class="sidebar">
        @if (progress$ | async; as progressDto) {
            <workflow-nav>
                <workflow-nav-item
                    [navRouterLink]="['step-1']"
                    [complete]="progressDto?.Steps?.Step1Name?.Completed"
                    [disabled]="progressDto?.Steps?.Step1Name?.Disabled">
                    Step 1 Title
                </workflow-nav-item>
                <workflow-nav-item
                    [navRouterLink]="['step-2']"
                    [complete]="progressDto?.Steps?.Step2Name?.Completed"
                    [disabled]="progressDto?.Steps?.Step2Name?.Disabled">
                    Step 2 Title
                </workflow-nav-item>
            </workflow-nav>
        }
    </aside>
    <main class="main">
        <div class="outlet-container">
            <router-outlet></router-outlet>
        </div>
    </main>
</div>
```

---

## 6. Frontend: Step Components

Create a component for each step in `Neptune.Web/src/app/pages/{entity}/workflow/{step-name}/`:

### Form Entry Step Template

```typescript
import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/forms/form-field/form-field.component";
import { WorkflowBodyComponent } from "src/app/shared/components/workflow-body/workflow-body.component";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";

@Component({
    selector: "{entity}-{step-name}",
    standalone: true,
    imports: [ReactiveFormsModule, PageHeaderComponent, WorkflowBodyComponent, FormFieldComponent],
    templateUrl: "./{step-name}.component.html",
})
export class {StepName}Component implements OnInit {
    @Input() {entity}ID: number | null = null;
    public isLoadingSubmit = false;
    public FormFieldType = FormFieldType;

    constructor(
        private router: Router,
        private {entity}Service: {Entity}Service
    ) {}

    ngOnInit(): void {
        if (this.{entity}ID) {
            this.{entity}Service.get{Entity}(this.{entity}ID).subscribe((detail) => {
                this.formGroup.patchValue(detail);
            });
        }
    }

    public save(andContinue: boolean = false): void {
        this.isLoadingSubmit = true;
        const request = this.formGroup.value;

        this.{entity}Service.update{Entity}(this.{entity}ID, request).subscribe({
            next: () => {
                this.isLoadingSubmit = false;
                if (andContinue) {
                    this.router.navigate(["/{entities}/edit", this.{entity}ID, "next-step"]);
                }
            },
            error: () => {
                this.isLoadingSubmit = false;
            }
        });
    }
}
```

### Step HTML Template

```html
<page-header pageTitle="{Step Title}"></page-header>
<workflow-body>
    <div class="card">
        <div class="card-header">
            <span class="card-title">{Section Title}</span>
        </div>
        <div class="card-body">
            <form [formGroup]="formGroup">
                <div class="grid-12">
                    <div class="g-col-6">
                        <form-field
                            [formGroup]="formGroup"
                            fieldLabel="Field Label"
                            formControlName="FieldName"
                            [type]="FormFieldType.Text">
                        </form-field>
                    </div>
                </div>
            </form>
        </div>
    </div>
</workflow-body>

<div class="page-footer">
    <button class="btn btn-secondary" (click)="save(false)" [disabled]="isLoadingSubmit || formGroup.invalid">
        Save
    </button>
    <button class="btn btn-primary" (click)="save(true)" [disabled]="isLoadingSubmit || formGroup.invalid">
        Save &amp; Continue
    </button>
</div>
```

---

## 7. Frontend: Route Configuration

Add routes to `Neptune.Web/src/app/app.routes.ts`:

```typescript
// Create flow
{
    path: "{entities}/new",
    title: "New {Entity}",
    loadComponent: () =>
        import("./pages/{entity}/workflow/{entity}-workflow-outlet/{entity}-workflow-outlet.component").then(
            (m) => m.{Entity}WorkflowOutletComponent
        ),
    children: [
        { path: "", redirectTo: "step-1", pathMatch: "full" },
        {
            path: "step-1",
            loadComponent: () =>
                import("./pages/{entity}/workflow/step-1/step-1.component").then(
                    (m) => m.Step1Component
                ),
        },
    ],
},

// Edit flow - all steps available
{
    path: "{entities}/edit/:{entity}ID",
    title: "Edit {Entity}",
    loadComponent: () =>
        import("./pages/{entity}/workflow/{entity}-workflow-outlet/{entity}-workflow-outlet.component").then(
            (m) => m.{Entity}WorkflowOutletComponent
        ),
    children: [
        { path: "", redirectTo: "step-1", pathMatch: "full" },
        {
            path: "step-1",
            loadComponent: () =>
                import("./pages/{entity}/workflow/step-1/step-1.component").then(
                    (m) => m.Step1Component
                ),
        },
        {
            path: "step-2",
            loadComponent: () =>
                import("./pages/{entity}/workflow/step-2/step-2.component").then(
                    (m) => m.Step2Component
                ),
        },
        // ... all remaining steps
    ],
},
```

---

## 8. Checklist Before Completion

### Backend: Workflow Steps

- [ ] Created `{Entity}WorkflowProgress.cs` with step enum and completion logic
- [ ] Created step DTOs
- [ ] Added `GetWorkflowProgress` endpoint
- [ ] Added GET/PUT endpoints for each step
- [ ] Built API and regenerated swagger

### Backend: State Transitions

- [ ] Created state transition methods (Submit, Approve, Return, etc.)
- [ ] Added state transition endpoints to controller
- [ ] Authorization attributes applied (JurisdictionEditFeature, AdminFeature, etc.)

### Frontend: Workflow

- [ ] Created workflow outlet component with sidebar and nav
- [ ] Created step components (one per step)
- [ ] Added create route (`/new`) with first step only
- [ ] Added edit route (`/edit/:id`) with all step children
- [ ] Regenerated TypeScript models (`npm run gen-model`)

### Frontend: State Transitions

- [ ] Added action buttons to detail page based on status
- [ ] Implemented state transition handlers
- [ ] Status-based UI (edit button only when editable, etc.)

### Testing

- [ ] Navigation between steps works
- [ ] Save and continue advances to next step
- [ ] Disabled steps prevent navigation
- [ ] State transitions work correctly
- [ ] Components are standalone with explicit imports
