---
name: migrate-map
description: Migrate Leaflet maps from legacy MVC to Angular using neptune-map and layer components.
allowed-tools: [Read, Glob, Grep, Edit, Write, Bash(dotnet build:*), Bash(npm run gen-model:*)]
argument-hint: <EntityName>
---

# Migrate Map Skill

When the user invokes `/migrate-map <EntityName>`:

## Overview

This skill guides the migration of Leaflet maps from legacy MVC views to Angular using the `neptune-map` component and associated layer components.

---

## 1. Analyze Legacy Map Implementation

First, examine the legacy MVC implementation:

- **Views**: Look for map containers in `Neptune.WebMvc/Views/{Entity}/`
  - Search for `<div id="*Map*">` or similar map containers
  - Check for `leaflet` or map-related classes
- **JavaScript**: Search `Neptune.WebMvc/Scripts/` for map initialization
  - Look for `L.map()`, `L.geoJSON()`, `L.tileLayer()` calls
  - Identify layer sources (WMS, GeoJSON, markers)
- **Controllers**: Check `Neptune.WebMvc/Controllers/{Entity}Controller.cs`
  - Look for methods returning GeoJSON or map data
  - Identify boundary/location data retrieval patterns

### Questions to Answer:

1. What data is displayed on the map? (boundaries, points, polygons)
2. How is the data retrieved? (inline JSON, API calls, WMS services)
3. Are there multiple layers? What are they?
4. Does the map zoom to fit specific features?
5. Are there popups or tooltips on features?
6. What is the default tile layer? (Terrain, Street, Satellite)

---

## 2. Plan Backend API Endpoints

### GeoJSON Feature Collection Pattern

For entity boundaries or spatial data, create endpoints returning `FeatureCollection`:

```csharp
// In EntityController.cs
[HttpGet("{entityID}/boundary")]
[UserViewFeature]
public ActionResult<FeatureCollection> GetBoundary([FromRoute] int entityID)
{
    var features = Entities.GetBoundaryAsFeatureCollection(DbContext, entityID);
    return Ok(features);
}
```

### Static Helper Pattern

```csharp
// In Entities.cs
public static FeatureCollection GetBoundaryAsFeatureCollection(
    NeptuneDbContext dbContext, int entityID)
{
    var entity = dbContext.Entities
        .AsNoTracking()
        .Where(x => x.EntityID == entityID && x.EntityGeometry != null)
        .Select(x => new { x.EntityID, x.EntityGeometry })
        .SingleOrDefault();

    if (entity?.EntityGeometry == null)
        return new FeatureCollection();

    var feature = new Feature(entity.EntityGeometry, new AttributesTable
    {
        { "EntityID", entity.EntityID }
    });

    return new FeatureCollection(new[] { feature });
}
```

---

## 3. Update Detail DTO for Map Flags

Add boolean flags to the detail DTO to indicate if map data exists:

```csharp
// In EntityDto.cs
public bool HasBoundary { get; set; }
public BoundingBoxDto? BoundingBox { get; set; }
```

---

## 4. Angular Component Integration

### Available Map and Layer Components

| Component | Selector | Purpose |
|-----------|----------|---------|
| `NeptuneMapComponent` | `neptune-map` | Base map container |
| `DelineationsLayerComponent` | `delineations-layer` | BMP delineation polygons |
| `JurisdictionsLayerComponent` | `jurisdictions-layer` | Stormwater jurisdiction boundaries |
| `LandUseBlockLayerComponent` | `land-use-block-layer` | Land use block polygons |
| `InventoriedBmpsLayerComponent` | `inventoried-bmps-layer` | Inventoried BMP locations |
| `OvtaAreaLayerComponent` | `ovta-area-layer` | Single OVTA area |
| `OvtaAreasLayerComponent` | `ovta-areas-layer` | Multiple OVTA areas |
| `ParcelLayerComponent` | `parcel-layer` | Parcels |
| `PermitTypeLayerComponent` | `permit-type-layer` | Permit type boundaries |
| `TrashGeneratingUnitLayerComponent` | `trash-generating-unit-layer` | Trash generating units |
| `StormwaterNetworkLayerComponent` | `stormwater-network-layer` | Stormwater network |
| `WqmpsLayerComponent` | `wqmps-layer` | Water quality management plans |
| `RegionalSubbasinsLayerComponent` | `regional-subbasins-layer` | Regional subbasins |
| `LoadGeneratingUnitsLayerComponent` | `load-generating-units-layer` | Load generating units |
| `GenericWmsWfsLayerComponent` | `generic-wms-wfs-layer` | Generic WMS/WFS with `OverlayMode` |

### OverlayMode Enum

```typescript
import { OverlayMode } from "src/app/shared/components/leaflet/layers/generic-wms-wfs-layer/overlay-mode.enum";
```

### Component TypeScript Pattern

```typescript
import { Component } from "@angular/core";
import { Observable } from "rxjs";
import { Map } from "leaflet";
import { NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";

@Component({
    // ...
    imports: [
        NeptuneMapComponent,
        DelineationsLayerComponent,
        JurisdictionsLayerComponent,
        // other layer components as needed
    ],
})
export class EntityDetailComponent {
    public map: Map;
    public layerControl: L.Control.Layers;
    public mapIsReady: boolean = false;

    handleMapReady(event: MapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }
}
```

### Template Pattern

```html
@if (entity.HasBoundary) {
<div class="card">
    <div class="card-header"><span class="card-title">Map</span></div>
    <div class="card-body">
        <neptune-map
            [mapHeight]="'400px'"
            [boundingBox]="entity.BoundingBox"
            (onMapLoad)="handleMapReady($event)">

            <!-- Delineation layer -->
            @if (mapIsReady) {
            <delineations-layer
                [map]="map"
                [layerControl]="layerControl"
                [treatmentBMPID]="entity.TreatmentBMPID">
            </delineations-layer>
            }

            <!-- Jurisdiction reference layer -->
            @if (mapIsReady) {
            <jurisdictions-layer
                [map]="map"
                [layerControl]="layerControl">
            </jurisdictions-layer>
            }

        </neptune-map>
    </div>
</div>
}
```

---

## 5. GeoServer Views

Neptune uses `vGeoServer*` views for WMS/WFS layers served by GeoServer. Common views:

- `vGeoServerTreatmentBMP` — BMP locations
- `vGeoServerDelineation` — BMP delineation polygons
- `vGeoServerTrashGeneratingUnit` — Trash generating units
- `vGeoServerLandUseBlock` — Land use blocks
- `vGeoServerOnlandVisualTrashAssessmentArea` — OVTA areas
- `vGeoServerRegionalSubbasin` — Regional subbasins
- `vGeoServerWaterQualityManagementPlan` — WQMPs
- `vGeoServerStormwaterNetwork` — Stormwater network

---

## 6. Map Component Configuration Reference

### Input Properties

| Input | Type | Default | Description |
|-------|------|---------|-------------|
| `mapHeight` | string | `'500px'` | CSS height of map container |
| `selectedTileLayer` | string | `'Terrain'` | Default base layer (`'Terrain'`, `'Street'`, `'Satellite'`) |
| `boundingBox` | `BoundingBoxDto` | Orange County, CA | Initial bounds |
| `showLegend` | boolean | `false` | Show legend control |
| `disableMapInteraction` | boolean | `false` | Lock map (no pan/zoom) |
| `collapseLayerControlOnLoad` | boolean | `false` | Start with layers control collapsed |

### Output Events

| Output | Type | Description |
|--------|------|-------------|
| `onMapLoad` | `MapInitEvent` | Fired when map is ready; provides `map` and `layerControl` |

---

## 7. Map Layout Patterns

### Side-by-side with details (6-6 grid)

```html
<div class="grid-12">
    <div class="g-col-6">
        <!-- Map card -->
    </div>
    <div class="g-col-6">
        <!-- Details card -->
    </div>
</div>
```

### Full-width map above grid

```html
<div class="grid-12">
    <div class="g-col-12">
        <!-- Map card -->
    </div>
    <div class="g-col-12">
        <!-- Grid card -->
    </div>
</div>
```

### Map with "No Location" fallback

```html
@if (entity.HasBoundary) {
<div class="card">
    <div class="card-header"><span class="card-title">Location</span></div>
    <div class="card-body">
        <neptune-map ...>
            <!-- layers -->
        </neptune-map>
    </div>
</div>
} @else {
<div class="card">
    <div class="card-header"><span class="card-title">Location</span></div>
    <div class="card-body">
        <p class="text-muted">No location data available for this entity.</p>
    </div>
</div>
}
```

---

## 8. Migration Checklist

- [ ] Identified all map layers from legacy implementation
- [ ] Created API endpoints for custom GeoJSON data (if needed)
- [ ] Added `HasBoundary` / spatial flags to detail DTO
- [ ] Added `BoundingBox` to detail DTO (if applicable)
- [ ] Added `neptune-map` component to component imports
- [ ] Added appropriate layer components to imports
- [ ] Implemented `handleMapReady` method
- [ ] Added map template with conditional rendering
- [ ] Verified map displays correctly (default bounds: Orange County, CA)
- [ ] Verified layers toggle correctly
- [ ] Verified zoom/bounds behavior
- [ ] Ran `npm run gen-model` after API changes

---

## Common Issues and Solutions

### Map doesn't display
- Ensure `handleMapReady` is called and sets `mapIsReady = true`
- Layer components need `@if (mapIsReady)` guard
- Check that `map` and `layerControl` are passed to layer components

### Features don't appear
- Check browser console for GeoJSON parsing errors
- Verify API endpoint returns valid GeoJSON structure
- Ensure observable is subscribed (use `| async`)

### Map is wrong size
- Set explicit `mapHeight` input
- Ensure parent container has defined height

### Wrong initial bounds
- Pass `boundingBox` input to map component
- Default bounds are Orange County, CA
