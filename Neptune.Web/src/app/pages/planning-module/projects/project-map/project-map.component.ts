import { ApplicationRef, Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import * as L from "leaflet";
import "leaflet.fullscreen";
import { forkJoin } from "rxjs";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { TreatmentBMPModelingAttributeDropdownItemDto } from "src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { FieldDefinitionTypeEnum } from "src/app/shared/generated/enum/field-definition-type-enum";
import { TreatmentBMPModelingTypeEnum } from "src/app/shared/generated/enum/treatment-b-m-p-modeling-type-enum";
import { TreatmentBMPTypeWithModelingAttributesDto } from "src/app/shared/generated/model/treatment-bmp-type-with-modeling-attributes-dto";
import { TreatmentBMPModelingAttributeDefinitionDto } from "src/app/shared/generated/model/treatment-bmp-modeling-attribute-definition-dto";
import { TreatmentBmpsComponent } from "src/app/pages/planning-module/projects/project-workflow/treatment-bmps/treatment-bmps.component";
import { TreatmentBMPDisplayDto } from "src/app/shared/generated/model/treatment-bmp-display-dto";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { NgIf, NgFor, DecimalPipe } from "@angular/common";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { InventoriedBMPsLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-layer/inventoried-bmps-layer.component";
import { TreatmentBMPTypeService } from "src/app/shared/generated/api/treatment-bmp-type.service";

//This component could use a fair amount of cleanup. It should likely be sent in the treatment bmps and delineations instead of grabbing them itself
@Component({
    selector: "project-map",
    templateUrl: "./project-map.component.html",
    styleUrls: ["./project-map.component.scss"],
    standalone: true,
    imports: [
        NgIf,
        NgFor,
        FieldDefinitionComponent,
        DecimalPipe,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        DelineationsLayerComponent,
        JurisdictionsLayerComponent,
        WqmpsLayerComponent,
        StormwaterNetworkLayerComponent,
        InventoriedBMPsLayerComponent,
    ],
})
export class ProjectMapComponent implements OnInit {
    @Input("projectID") projectID: number;

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
    public delineationsLayer: L.GeoJson<any>;

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

    public treatmentBMPModelingTypeEnum = TreatmentBMPModelingTypeEnum;
    public fieldDefinitionTypeEnum = FieldDefinitionTypeEnum;
    public modelingAttributeDropdownItems: Array<TreatmentBMPModelingAttributeDropdownItemDto>;
    public treatmentBMPTypes: Array<TreatmentBMPTypeWithModelingAttributesDto>;

    public delineations: DelineationUpsertDto[];

    public projectTreatmentBMPs: Array<TreatmentBMPDisplayDto>;

    constructor(
        private projectService: ProjectService,
        private treatmentBMPService: TreatmentBMPService,
        private treatmentBMPTypeService: TreatmentBMPTypeService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService
    ) {}

    public ngOnInit(): void {
        if (this.projectID) {
            forkJoin({
                treatmentBMPs: this.projectService.projectsProjectIDTreatmentBmpsGet(this.projectID),
                delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
                boundingBox: this.projectService.projectsProjectIDBoundingBoxGet(this.projectID),
                treatmentBMPTypes: this.treatmentBMPTypeService.treatmentBmpTypesGet(),
                modelingAttributeDropdownItems: this.treatmentBMPService.treatmentBmpsModelingAttributeDropdownItemsGet(),
            }).subscribe(({ treatmentBMPs, delineations, boundingBox, treatmentBMPTypes, modelingAttributeDropdownItems }) => {
                this.projectTreatmentBMPs = treatmentBMPs;
                this.delineations = delineations;
                this.boundingBox = boundingBox;
                this.treatmentBMPTypes = treatmentBMPTypes;
                this.modelingAttributeDropdownItems = modelingAttributeDropdownItems;
            });
        }

        this.compileService.configure(this.appRef);
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;

        this.updateMapLayers();
    }

    public updateMapLayers(): void {
        this.updateTreatmentBMPsLayer();

        if (this.projectTreatmentBMPs.length > 0) {
            this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
        }
    }

    public updateTreatmentBMPsLayer() {
        if (this.treatmentBMPsLayer) {
            this.map.removeLayer(this.treatmentBMPsLayer);
            this.treatmentBMPsLayer = null;
        }

        if (this.delineationsLayer) {
            this.map.removeLayer(this.delineationsLayer);
            this.delineationsLayer = null;
        }
        let hasFlownToSelectedObject = false;

        const delineationGeoJson = this.mapDelineationsToGeoJson(this.delineations);
        this.delineationsLayer = new L.GeoJSON(delineationGeoJson, {
            onEachFeature: (feature, layer) => {
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

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            filter: (feature) => {
                return this.selectedTreatmentBMP == null || feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID;
            },
            onEachFeature: (feature, layer) => {
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
        selectedAttributes = [
            `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
            `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
            `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`,
        ];

        if (this.selectedTreatmentBMP && this.selectedTreatmentBMP.Latitude && this.selectedTreatmentBMP.Longitude) {
            this.selectedObjectMarker = new L.marker(
                { lat: this.selectedTreatmentBMP.Latitude, lng: this.selectedTreatmentBMP.Longitude },
                { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 }
            );

            this.selectedObjectMarker.addTo(this.map);
            this.selectedListItemDetails.title = `${selectedNumber}`;
            this.selectedListItemDetails.attributes = selectedAttributes;
        }
    }

    public isFieldWithDropdown(fieldName: string): boolean {
        return TreatmentBmpsComponent.modelingAttributeFieldsWithDropdown.indexOf(fieldName) > -1;
    }

    public getModelingAttributeFieldsToDisplay(treatmentBMPTypeName: string): Array<TreatmentBMPModelingAttributeDefinitionDto> {
        return this.treatmentBMPTypes.find((x) => x.TreatmentBMPTypeName == treatmentBMPTypeName).TreatmentBMPModelingAttributes ?? [];
    }

    public getDropdownItemNameByFieldNameAndItemID(fieldName: string, itemID: number): string {
        const dropdownItem = this.modelingAttributeDropdownItems.find((x) => x.FieldName == fieldName && x.ItemID == itemID);
        return dropdownItem ? dropdownItem.ItemName : "";
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
