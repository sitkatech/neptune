# Angular Patterns

This document contains detailed patterns for the Angular frontend.

---

## Standalone Components

All new components must be standalone with explicit imports:

```typescript
@Component({
  selector: 'app-entity-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    NeptuneGridComponent,
    IconComponent,
    // explicit imports for all dependencies
  ],
  templateUrl: './entity-detail.component.html',
  styleUrl: './entity-detail.component.scss'
})
export class EntityDetailComponent { }
```

---

## Standard Page Structure

All list pages and detail pages should follow this consistent structure:

```html
<!-- 1. Breadcrumb - Navigation context -->
<breadcrumb [items]="[
    { label: 'Parent', url: '/parent' },
    { label: 'Current Page' }
]"></breadcrumb>

<!-- 2. Page Header - Title + page-wide actions -->
<page-header pageTitle="Page Title" [templateRight]="headerActions"></page-header>
<ng-template #headerActions>
    <button class="btn btn-primary">+ Add Entity</button>
</ng-template>

<!-- 3. Content - Cards, grids, forms -->
<div class="page-body">
    <!-- page content here -->
</div>
```

### Structure Elements

| Element | Purpose | Required |
|---------|---------|----------|
| `<breadcrumb>` | Navigation context, shows page hierarchy | Yes |
| `<page-header>` | Page title, uses h1 internally | Yes |
| `templateRight` | Page-wide action buttons (Add, Export, etc.) | If actions exist |
| `<div class="page-body">` | Content wrapper | Recommended |

### Page Header Component

The `page-header` component provides several template slots:

| Input | Purpose |
|-------|---------|
| `pageTitle` | The page title (rendered as h1) |
| `templateRight` | Content inline with title, flushed right (for action buttons) |
| `templateAbove` | Content above the title |
| `templateBottomRight` | Content below title area, flushed right |
| `templateTitleAppend` | Content appended directly after title text |
| `customRichTextTypeID` | ID for custom rich text description below title |

### List Page Example

```html
<breadcrumb [items]="[{ label: 'Stormwater' }, { label: 'Treatment BMPs' }]"></breadcrumb>

<page-header pageTitle="Treatment BMPs" [templateRight]="headerActions"></page-header>
<ng-template #headerActions>
    <a class="btn btn-primary" [routerLink]="['/treatment-bmps/new']">+ Add Treatment BMP</a>
</ng-template>

<div class="page-body">
    <neptune-grid [rowData]="items$ | async" [columnDefs]="columnDefs"></neptune-grid>
</div>
```

### Detail Page Example

```html
<breadcrumb [items]="[
    { label: 'Treatment BMPs', url: '/treatment-bmps' },
    { label: treatmentBMP.TreatmentBMPName }
]"></breadcrumb>

<page-header [pageTitle]="treatmentBMP.TreatmentBMPName" [templateRight]="headerActions"></page-header>
<ng-template #headerActions>
    <a class="btn btn-secondary" [routerLink]="['/treatment-bmps/edit', treatmentBMP.TreatmentBMPID]">
        <icon icon="Edit"></icon> Edit
    </a>
    <button class="btn btn-danger" (click)="delete()">
        <icon icon="Delete"></icon> Delete
    </button>
</ng-template>

<div class="page-body">
    <div class="grid-12">
        <!-- detail cards -->
    </div>
</div>
```

### Multi-Section Detail Page with Card-Level Edit Icons

For detail pages with multiple editable sections, use card-level edit icons:

```html
<div class="card">
    <div class="card-header flex">
        <span class="card-title">BMP Overview</span>
        @if (canEdit) {
            <a [routerLink]="['/treatment-bmps/edit', id, 'overview']" title="Edit Overview">
                <icon icon="Edit"></icon>
            </a>
        }
    </div>
    <div class="card-body">
        <!-- card content -->
    </div>
</div>
```

**Key points:**
- Use `flex` class on `card-header` (NOT `float-end` or other Bootstrap utilities)
- Icon only, no button class or text label
- Icon inherits white color from card header via `color: inherit` in `_card.scss`
- Include `title` attribute for accessibility

| Scenario | Edit Button Placement |
|----------|-----------------------|
| Single-edit detail page | Page header `templateRight` with `btn` class |
| Multi-section detail page | Card header with `flex` class, icon only |

---

## Route Params with withComponentInputBinding()

Use `@Input()` decorators matching route param names with `BehaviorSubject` for reactive state:

```typescript
@Component({...})
export class EntityDetailComponent {
  // Route param bound via withComponentInputBinding()
  @Input() set entityID(value: string) {
    this._entityID$.next(Number(value));
  }

  private _entityID$ = new BehaviorSubject<number | null>(null);

  entity$ = this._entityID$.pipe(
    filter((id): id is number => id != null),
    switchMap(id => this.entityService.getByID(id)),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  constructor(private entityService: EntityService) {}
}
```

---

## Template Pattern

Use `@if` with async pipe for observable data:

```html
@if (entity$ | async; as entity) {
  <div class="card">
    <div class="card-header">
      <span class="card-title">{{ entity.name }}</span>
    </div>
    <div class="card-body">
      <!-- content -->
    </div>
  </div>
} @else {
  <app-loading-spinner></app-loading-spinner>
}
```

---

## Reactive Patterns (Preferred)

**Always prefer observables with async pipe over imperative subscriptions.** This ensures automatic subscription management and avoids memory leaks.

### Why Reactive?

| Approach | Pros | Cons |
|----------|------|------|
| `async` pipe | Auto-unsubscribe, no timing issues, reactive updates | Slightly more verbose |
| Manual subscribe | Simple for one-off calls | Memory leaks if not unsubscribed, timing issues |

### Permission Checks

Use observables for permission state to avoid timing issues with async user loading:

```typescript
@Component({...})
export class EntityDetailComponent implements OnInit {
    public currentUser$: Observable<PersonDto>;
    public isAdmin$: Observable<boolean>;

    constructor(private authenticationService: AuthenticationService) {}

    ngOnInit(): void {
        this.currentUser$ = this.authenticationService.getCurrentUser().pipe(
            shareReplay({ bufferSize: 1, refCount: true })
        );
        this.isAdmin$ = this.currentUser$.pipe(
            map(user => this.authenticationService.isCurrentUserAnAdministrator())
        );
    }
}
```

Neptune's `AuthenticationService` provides these permission methods:
- `isCurrentUserAnAdministrator()` — Admin or SitkaAdmin
- `doesCurrentUserHaveJurisdictionEditPermission()` — JurisdictionManager or JurisdictionEditor
- `isCurrentUserNullOrUndefined()` — Not logged in
- `getCurrentUser()` — Returns observable of current user

**Template:**
```html
@if (isAdmin$ | async) {
    <button class="btn btn-primary" (click)="edit()">
        <icon icon="Edit"></icon> Edit
    </button>
}

<!-- Combining with other conditions -->
@if ((isAdmin$ | async) && entity.HasDelineation) {
    <!-- admin-only content for entities with delineations -->
}
```

### Derived Observables

Create derived observables for computed state:

```typescript
// Base observable
public entity$ = this._entityID$.pipe(
    filter((id): id is number => id != null),
    switchMap(id => this.service.getByID(id)),
    shareReplay({ bufferSize: 1, refCount: true })
);

// Derived observables
public canEdit$ = combineLatest([this.entity$, this.isAdmin$]).pipe(
    map(([entity, isAdmin]) => isAdmin && entity.ProjectStatusID === ProjectStatusEnum.Draft)
);

public relatedItems$ = this.entity$.pipe(
    switchMap(entity => this.service.getRelatedItems(entity.EntityID))
);
```

### Anti-Patterns to Avoid

**Bad - Manual subscription with timing issues:**
```typescript
// DON'T DO THIS
ngOnInit(): void {
    this.isAdmin = this.authService.isCurrentUserAnAdministrator(); // May be false if user not loaded yet
}
```

**Bad - Manual subscription without cleanup:**
```typescript
// DON'T DO THIS
ngOnInit(): void {
    this.authService.getCurrentUser().subscribe(user => {
        this.isAdmin = this.authService.isCurrentUserAnAdministrator();
    });
    // Memory leak - subscription never unsubscribed
}
```

**Good - Reactive with async pipe:**
```typescript
// DO THIS
public isAdmin$ = this.authService.getCurrentUser().pipe(
    map(() => this.authService.isCurrentUserAnAdministrator())
);
// Template: @if (isAdmin$ | async) { ... }
```

---

## Grid Components

Neptune uses `NeptuneGridComponent` at `src/app/shared/components/neptune-grid/` and `NeptuneGridHeaderComponent` at `src/app/shared/components/neptune-grid-header/`.

### Grid Column Definitions

Use the ag-Grid helper at `src/app/shared/helpers/ag-grid-helper.ts` for column definitions:

```typescript
import { AgGridHelper } from "src/app/shared/helpers/ag-grid-helper";

this.columnDefs = [
    AgGridHelper.createLinkColumnDef("Name", "TreatmentBMPName", "TreatmentBMPID", {
        InRouterLink: "/treatment-bmps/"
    }),
    AgGridHelper.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName"),
    AgGridHelper.createDateColumnDef("Created", "CreateDate"),
    AgGridHelper.createDecimalColumnDef("Area (ac)", "DelineationArea", { MaxDecimalPlacesToDisplay: 2 }),
];
```

### Grid Template

```html
<div class="card">
    <div class="card-header"><span class="card-title">Treatment BMPs</span></div>
    <div class="card-body">
        <neptune-grid
            [rowData]="items$ | async"
            [columnDefs]="columnDefs"
            [downloadFileName]="'treatment-bmps'">
        </neptune-grid>
    </div>
</div>
```

---

## Map Component

Neptune uses `neptune-map` at `src/app/shared/components/leaflet/neptune-map/`.

### Available Layer Components

Located in `src/app/shared/components/leaflet/layers/`:

| Component | Purpose |
|-----------|---------|
| `delineations-layer` | BMP delineation polygons |
| `jurisdictions-layer` | Stormwater jurisdiction boundaries |
| `land-use-block-layer` | Land use block polygons |
| `inventoried-bmps-layer` | Inventoried BMP locations |
| `ovta-area-layer` | Single OVTA area |
| `ovta-areas-layer` | Multiple OVTA areas |
| `parcel-layer` | Parcels |
| `permit-type-layer` | Permit type boundaries |
| `trash-generating-unit-layer` | Trash generating units |
| `stormwater-network-layer` | Stormwater network |
| `wqmps-layer` | Water quality management plans |
| `regional-subbasins-layer` | Regional subbasins |
| `load-generating-units-layer` | Load generating units |
| `generic-wms-wfs-layer` | Generic WMS/WFS layers with `OverlayMode` enum |

### Map Template Pattern

```html
@if (entity.HasDelineation) {
<div class="card">
    <div class="card-header"><span class="card-title">Map</span></div>
    <div class="card-body">
        <neptune-map
            [mapHeight]="'400px'"
            [boundingBox]="entity.BoundingBox"
            (onMapLoad)="handleMapReady($event)">

            @if (mapIsReady) {
            <delineations-layer
                [map]="map"
                [layerControl]="layerControl"
                [treatmentBMPID]="entity.TreatmentBMPID">
            </delineations-layer>
            }

        </neptune-map>
    </div>
</div>
}
```

---

## Modal Template Structure

### Opening a Modal

```typescript
import { DialogService } from "@ngneat/dialog";
import { EntityModalComponent, EntityModalData } from "./entity-modal/entity-modal.component";

openCreateModal(): void {
    const dialogRef = this.dialogService.open(EntityModalComponent, {
        data: { mode: "create" } as EntityModalData,
        size: "md"
    });

    dialogRef.afterClosed$.subscribe(result => {
        if (result) {
            this.refreshData();
        }
    });
}
```

### Modal Sizes

| Size | Description |
|------|-------------|
| `"sm"` | Small modal (~300px) |
| `"md"` | Medium modal (~500px) |
| `"lg"` | Large modal (~800px) |
| `"fullScreen"` | Full screen modal |

---

## Loading States

Use `@if/@else` with a loading spinner for async data:

```html
@if (entity$ | async; as entity) {
    <!-- Loaded content -->
} @else {
    <app-loading-spinner></app-loading-spinner>
}
```

For submit buttons, use a loading flag:

```html
<button class="btn btn-primary" (click)="save()" [disabled]="isSubmitting">
    {{ isSubmitting ? 'Saving...' : 'Save' }}
</button>
```

---

## Enum Dropdown Pattern

For dropdowns backed by enums or lookup tables, use generated enum values:

```typescript
import { ProjectStatusEnum } from "src/app/shared/generated/enum/project-status-enum";

// In component
public statusOptions = Object.entries(ProjectStatusEnum)
    .filter(([key]) => isNaN(Number(key)))
    .map(([label, value]) => ({ label, value }));
```

---

## Workflow Components

Neptune has workflow components at `src/app/shared/components/`:

| Component | Purpose |
|-----------|---------|
| `workflow-body` | Main content area for workflow steps |
| `workflow-nav` | Navigation sidebar for workflow steps |
| `workflow-nav-item` | Individual step in workflow nav (shows completion state) |
| `workflow-help` | Help/info panel for workflow steps |

### Workflow Template Pattern

```html
<workflow-nav>
    <workflow-nav-item
        [navRouterLink]="['step-1']"
        [complete]="progress?.Steps?.Step1?.Completed"
        [disabled]="progress?.Steps?.Step1?.Disabled">
        Step 1 Title
    </workflow-nav-item>
    <workflow-nav-item
        [navRouterLink]="['step-2']"
        [complete]="progress?.Steps?.Step2?.Completed"
        [disabled]="progress?.Steps?.Step2?.Disabled">
        Step 2 Title
    </workflow-nav-item>
</workflow-nav>
```

---

## Route Guards

Neptune uses these route guards at `src/app/shared/guards/`:

| Guard | Purpose |
|-------|---------|
| `UnsavedChangesGuard` | Prevents navigation away from unsaved changes |

Additional auth guards are applied via `AuthenticationService` methods in component logic.

---

## Icon Usage

Use the `<icon>` component for consistent iconography:

### Edit Icons

| Icon | Usage | Example |
|------|-------|---------|
| `Edit` | General edit actions (forms, detail pages, grids) | `<icon icon="Edit"></icon>` |
| `GeomanEdit` | Map editing with Geoman library only | Used on map pages with polygon/geometry editing |

**Rule:** Use `Edit` for all standard edit buttons. Reserve `GeomanEdit` exclusively for Geoman map editing pages.

### Delete Icons

| Icon | Usage | Example |
|------|-------|---------|
| `Delete` | Delete actions (remove records, items) | `<icon icon="Delete"></icon>` |
| `CircleX` | Modal close buttons only | Used in dialog/modal headers |

**Rule:** Use `Delete` for all delete buttons. Reserve `CircleX` exclusively for modal close buttons.

### Common Icons

| Icon | Purpose |
|------|---------|
| `Edit` | Edit action (forms, records) |
| `Delete` | Delete action (trash icon) |
| `CircleX` | Modal/dialog close button (red X) |
| `File` | File/document indicator |
| `Download` | Download action |
| `Upload` | Upload action |
| `Info` | Information tooltip |
| `Warning` | Warning indicator |
| `StepComplete` | Workflow step completed |
| `StepIncomplete` | Workflow step incomplete |

---

## Add Button Placement

### Page-Level Add Button

Place in page header using `templateRight`:

```html
<page-header pageTitle="Treatment BMPs" [templateRight]="headerActions"></page-header>
<ng-template #headerActions>
    <a class="btn btn-primary" [routerLink]="['/treatment-bmps/new']">+ Add Treatment BMP</a>
</ng-template>
```

### Section-Level Add Buttons

When a detail page has multiple child entity sections, place each Add button in the card header:

```html
<div class="card">
    <div class="card-header flex">
        <span class="card-title">Assessments</span>
        <button class="btn btn-primary btn-sm" (click)="addAssessment()">
            + Add Assessment
        </button>
    </div>
    <div class="card-body">
        <!-- assessments grid -->
    </div>
</div>
```

### Summary

| Scenario | Placement |
|----------|-----------|
| List page (single entity type) | Page header, top right |
| Detail page (single Add action) | Page header, top right |
| Detail page (multiple child entity sections) | Each card header, top right |

---

## Form Field Component

Neptune uses the reusable form field component at `src/app/shared/components/forms/form-field/`:

```html
<form [formGroup]="form">
    <form-field
        [formControl]="form.controls.EntityName"
        fieldLabel="Name"
        [type]="FormFieldType.Text"
        [required]="true"
        placeholder="Enter name">
    </form-field>

    <form-field
        [formControl]="form.controls.CategoryID"
        fieldLabel="Category"
        [type]="FormFieldType.Select"
        [required]="true"
        [selectOptions]="categories"
        selectLabelField="CategoryName"
        selectValueField="CategoryID">
    </form-field>
</form>
```

---

## SCSS Conventions

- Import: `@use "/src/scss/abstracts" as *;`
- Reuse existing variables and mixins from the abstracts
- Prettier: 4-space indent, 180 printWidth, double quotes, trailing commas (es5), semicolons
