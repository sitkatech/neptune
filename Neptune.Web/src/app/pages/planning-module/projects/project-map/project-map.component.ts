import { ApplicationRef, Component, DestroyRef, EventEmitter, Input, OnInit, Output, inject } from "@angular/core";
import * as L from "leaflet";
import { BehaviorSubject, combineLatest, distinctUntilChanged, filter, forkJoin, map, shareReplay, switchMap } from "rxjs";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { FieldDefinitionTypeEnum } from "src/app/shared/generated/enum/field-definition-type-enum";
import { TreatmentBMPTypeWithModelingAttributesDto } from "src/app/shared/generated/model/treatment-bmp-type-with-modeling-attributes-dto";
import { TreatmentBMPDisplayDto } from "src/app/shared/generated/model/treatment-bmp-display-dto";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { CommonModule, DecimalPipe } from "@angular/common";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { InventoriedBMPsLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-layer/inventoried-bmps-layer.component";
import { TreatmentBMPTypeService } from "src/app/shared/generated/api/treatment-bmp-type.service";
import { TreatmentBMPTypeCustomAttributeTypeDto } from "src/app/shared/generated/model/treatment-bmp-type-custom-attribute-type-dto";
import { TreatmentBMPTypeCustomAttributeTypeService } from "src/app/shared/generated/api/treatment-bmp-type-custom-attribute-type.service";
import { CustomAttributeTypePurposeEnum } from "src/app/shared/generated/enum/custom-attribute-type-purpose-enum";
import { CustomAttributeDataTypeEnum } from "src/app/shared/generated/enum/custom-attribute-data-type-enum";
import { CustomAttributeTypeDto } from "src/app/shared/generated/model/custom-attribute-type-dto";
import { PopperDirective } from "src/app/shared/directives/popper.directive";
import { OverlayMode } from "src/app/shared/components/leaflet/layers/generic-wms-wfs-layer/overlay-mode.enum";

//This component could use a fair amount of cleanup. It should likely be sent in the treatment bmps and delineations instead of grabbing them itself
@Component({
    selector: "project-map",
    templateUrl: "./project-map.component.html",
    styleUrls: ["./project-map.component.scss"],
    imports: [
        CommonModule,
        FieldDefinitionComponent,
        DecimalPipe,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        DelineationsLayerComponent,
        JurisdictionsLayerComponent,
        WqmpsLayerComponent,
        StormwaterNetworkLayerComponent,
        InventoriedBMPsLayerComponent,
        PopperDirective,
    ],
})
export class ProjectMapComponent implements OnInit {
    public OverlayMode = OverlayMode;

    private readonly destroyRef = inject(DestroyRef);

    private readonly projectIDSubject = new BehaviorSubject<number | null>(null);
    public readonly projectID$ = this.projectIDSubject.asObservable().pipe(
        filter((id): id is number => id != null && Number.isFinite(id)),
        distinctUntilChanged()
    );

    private _projectID: number;
    @Input("projectID")
    set projectID(value: number) {
        this._projectID = value;
        this.projectIDSubject.next(value);
    }
    get projectID(): number {
        return this._projectID;
    }

    public mapIsReady: boolean = false;
    public visibleTreatmentBMPStyle: string = "treatmentBMP_purple_outline_only";
    public selectedTreatmentBMPStyle: string = "treatmentBMP_yellow";
    public mapHeight: string = "750px";

    @Output()
    public afterSetControl: EventEmitter<L.Control.Layers> = new EventEmitter();

    @Output()
    public afterLoadMap: EventEmitter<L.LeafletEvent> = new EventEmitter();

    @Output()
    public onMapMoveEnd: EventEmitter<L.LeafletEvent> = new EventEmitter();

    public map: L.Map;
    public layerControl: L.Control.Layers;
    public boundingBox: BoundingBoxDto;
    public selectedListItem: number;
    public selectedListItemDetails: { [key: string]: any } = {};
    public selectedObjectMarker: L.Layer;
    public selectedTreatmentBMP: TreatmentBMPDisplayDto;
    public treatmentBMPsLayer: L.GeoJSON<any>;
    public delineationsLayer: L.GeoJSON<any>;

    private delineationDefaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };
    private delineationSelectedStyle = {
        color: "yellow",
        fillOpacity: 0.2,
        opacity: 1,
    };

    public fieldDefinitionTypeEnum = FieldDefinitionTypeEnum;
    public treatmentBMPTypes: Array<TreatmentBMPTypeWithModelingAttributesDto>;

    public delineations: DelineationUpsertDto[];

    public projectTreatmentBMPs: Array<TreatmentBMPDisplayDto>;
    public treatmentBMPTypeCustomAttributeTypes: TreatmentBMPTypeCustomAttributeTypeDto[];

    private readonly selectedTreatmentBMPSubject = new BehaviorSubject<TreatmentBMPDisplayDto | null>(null);
    public readonly selectedTreatmentBMP$ = this.selectedTreatmentBMPSubject.asObservable();
    public readonly selectedTreatmentBMPID$ = this.selectedTreatmentBMP$.pipe(
        map((bmp) => bmp?.TreatmentBMPID ?? null),
        distinctUntilChanged()
    );

    private readonly mapReadySubject = new BehaviorSubject<NeptuneMapInitEvent | null>(null);
    public readonly mapReady$ = this.mapReadySubject.asObservable().pipe(filter((x): x is NeptuneMapInitEvent => x != null));

    private readonly data$ = this.projectID$.pipe(
        switchMap((projectID) =>
            forkJoin({
                treatmentBMPs: this.projectService.listTreatmentBMPsByProjectIDProject(projectID),
                delineations: this.projectService.listDelineationsByProjectIDProject(projectID),
                boundingBox: this.projectService.getBoundingBoxByProjectIDProject(projectID),
                treatmentBMPTypes: this.treatmentBMPTypeService.listTreatmentBMPType(),
                treatmentBMPTypeCustomAttributeTypes:
                    this.treatmentBMPTypeCustomAttributeTypeService.getTreatmentBMPTypeCustomAttributeTypeByCustomAttributePurposeIDTreatmentBMPTypeCustomAttributeType(
                        CustomAttributeTypePurposeEnum.Modeling
                    ),
            })
        ),
        shareReplay(1)
    );

    public readonly projectTreatmentBMPs$ = this.data$.pipe(
        map((x) => x.treatmentBMPs ?? []),
        shareReplay(1)
    );
    public readonly boundingBox$ = this.data$.pipe(
        map((x) => x.boundingBox),
        shareReplay(1)
    );

    constructor(
        private projectService: ProjectService,
        private treatmentBMPTypeService: TreatmentBMPTypeService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService,
        private treatmentBMPTypeCustomAttributeTypeService: TreatmentBMPTypeCustomAttributeTypeService
    ) {}

    public ngOnInit(): void {
        // Single subscription for Leaflet side-effects + backing field updates.
        // This keeps imperative work contained and ensures we clean up on destroy.
        combineLatest([this.mapReady$, this.data$])
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe(([mapReady, data]) => {
                // Keep existing fields in sync for helper methods / imperative map code.
                this.map = mapReady.map;
                this.layerControl = mapReady.layerControl;
                this.mapIsReady = true;

                this.projectTreatmentBMPs = data.treatmentBMPs ?? [];
                this.delineations = data.delineations ?? [];
                this.boundingBox = data.boundingBox;
                this.treatmentBMPTypes = data.treatmentBMPTypes ?? [];
                this.treatmentBMPTypeCustomAttributeTypes = data.treatmentBMPTypeCustomAttributeTypes ?? [];

                this.updateMapLayers();
            });

        this.compileService.configure(this.appRef);
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        // Keep a synchronous flag/fields for templates that should render immediately on map init.
        // In zoneless mode, relying on async-pipe emissions that happen during child lifecycle can be flaky.
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;

        this.mapReadySubject.next(event);

        // Ensure the view updates immediately in zoneless mode.
        // (Output emissions and Leaflet callbacks don't always schedule a render on their own.)
        Promise.resolve().then(() => this.appRef.tick());
    }

    public updateMapLayers(): void {
        if (!this.mapIsReady || !this.map) {
            return;
        }

        if (!Array.isArray(this.projectTreatmentBMPs) || !Array.isArray(this.delineations)) {
            return;
        }

        this.updateTreatmentBMPsLayer();

        if (this.projectTreatmentBMPs.length > 0) {
            this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
        }
    }

    public updateTreatmentBMPsLayer() {
        if (!this.mapIsReady || !this.map) {
            return;
        }

        const delineations = this.delineations ?? [];
        const projectTreatmentBMPs = this.projectTreatmentBMPs ?? [];

        if (this.treatmentBMPsLayer) {
            this.map.removeLayer(this.treatmentBMPsLayer);
            this.treatmentBMPsLayer = null;
        }

        if (this.delineationsLayer) {
            this.map.removeLayer(this.delineationsLayer);
            this.delineationsLayer = null;
        }
        let hasFlownToSelectedObject = false;

        const delineationGeoJson = this.mapDelineationsToGeoJson(delineations);
        this.delineationsLayer = new L.GeoJSON(delineationGeoJson as any, {
            onEachFeature: (feature, layer: L.Polygon) => {
                if (this.selectedTreatmentBMP != null) {
                    if (layer.feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID) {
                        layer.setStyle(this.delineationDefaultStyle);
                        return;
                    }
                    layer.setStyle(this.delineationSelectedStyle).bringToFront();
                    this.map.flyToBounds(layer.getBounds(), { padding: new L.Point(50, 50) });
                    hasFlownToSelectedObject = true;
                }
            },
        });
        this.delineationsLayer.addTo(this.map);

        this.delineationsLayer.on("click", (event: L.LeafletEvent) => {
            this.selectTreatmentBMP(event.propagatedFrom.feature.properties.TreatmentBMPID);
        });

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson as any, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            filter: (feature) => {
                return this.selectedTreatmentBMP == null || feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID;
            },
            onEachFeature: (feature, layer: L.Marker) => {
                if (this.selectedTreatmentBMP != null && hasFlownToSelectedObject) {
                    if (layer.feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID) {
                        return;
                    }
                    this.map.flyTo(layer.getLatLng(), 18);
                }
            },
        });
        this.treatmentBMPsLayer.addTo(this.map);

        this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
            this.selectTreatmentBMP(event.propagatedFrom.feature.properties.TreatmentBMPID);
        });
    }

    private mapTreatmentBMPsToGeoJson(treatmentBMPs: TreatmentBMPDisplayDto[]) {
        return {
            type: "FeatureCollection",
            features: treatmentBMPs.map((x) => {
                let treatmentBMPGeoJson = {
                    type: "Feature",
                    geometry: {
                        type: "Point",
                        coordinates: [x.Longitude ?? 0, x.Latitude ?? 0],
                    },
                    properties: {
                        TreatmentBMPID: x.TreatmentBMPID,
                        TreatmentBMPName: x.TreatmentBMPName,
                        TreatmentBMPTypeName: x.TreatmentBMPTypeName,
                        Latitude: x.Latitude,
                        Longitude: x.Longitude,
                    },
                };
                return treatmentBMPGeoJson;
            }),
        };
    }

    private mapDelineationsToGeoJson(delineations: DelineationUpsertDto[]) {
        return delineations.map((x) => JSON.parse(x.Geometry));
    }

    public selectTreatmentBMP(treatmentBMPID: number) {
        this.selectTreatmentBMPImpl(treatmentBMPID);
        this.updateTreatmentBMPsLayer();
    }

    private selectTreatmentBMPImpl(treatmentBMPID: number) {
        this.clearSelectedItem();

        this.selectedListItem = treatmentBMPID;
        let selectedNumber = null;
        let selectedAttributes = null;
        this.selectedTreatmentBMP = this.projectTreatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
        this.selectedTreatmentBMPSubject.next(this.selectedTreatmentBMP ?? null);

        if (!this.selectedTreatmentBMP) {
            return;
        }
        selectedAttributes = [
            `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
            `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
            `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`,
        ];

        if (this.selectedTreatmentBMP && this.selectedTreatmentBMP.Latitude && this.selectedTreatmentBMP.Longitude) {
            this.selectedObjectMarker = L.marker(
                { lat: this.selectedTreatmentBMP.Latitude, lng: this.selectedTreatmentBMP.Longitude },
                { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 }
            );

            this.selectedObjectMarker.addTo(this.map);
            this.selectedListItemDetails.title = `${selectedNumber}`;
            this.selectedListItemDetails.attributes = selectedAttributes;
        }
    }

    public getTreatmentBMPTypeCustomAttributeTypesForTreatmentBMPType(treatmentBMPTypeID: number) {
        return this.treatmentBMPTypeCustomAttributeTypes.filter((x) => x.TreatmentBMPTypeID == treatmentBMPTypeID);
    }

    public getCustomAttributeFieldsToDisplay(treatmentBMPTypeID: number): Array<CustomAttributeTypeDto> {
        return this.getTreatmentBMPTypeCustomAttributeTypesForTreatmentBMPType(treatmentBMPTypeID)
            .sort((a, b) => a.SortOrder - b.SortOrder)
            .map((x) => x.CustomAttributeType);
    }

    public isFieldWithDropdown(customAttributeDataTypeID: number): boolean {
        return customAttributeDataTypeID == CustomAttributeDataTypeEnum.PickFromList;
    }

    public getIndexOfCustomAttribute(customAttributeTypeID: number): number {
        let value = this.selectedTreatmentBMP.CustomAttributes.findIndex((x) => x.CustomAttributeTypeID == customAttributeTypeID);
        return value;
    }

    private clearSelectedItem() {
        if (this.selectedListItem) {
            this.selectedListItem = null;
            this.selectedListItemDetails = {};
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = null;
        }
    }

    public treatmentBMPHasDelineation(treatmentBMPID: number) {
        return this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID) != null;
    }

    public getDelineationAreaForTreatmentBMP(treatmentBMPID: number) {
        let delineation = this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID);

        if (delineation?.DelineationArea == null) {
            return "Not provided yet";
        }

        return `${delineation.DelineationArea} ac`;
    }
}
