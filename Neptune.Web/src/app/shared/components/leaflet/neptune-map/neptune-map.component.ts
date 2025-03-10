import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Control, LeafletEvent, Map, MapOptions, DomUtil, ControlPosition } from "leaflet";
import * as L from "leaflet";
import "src/scripts/leaflet.groupedlayercontrol.js";
import "/node_modules/leaflet.fullscreen/Control.FullScreen.js";
import GestureHandling from "leaflet-gesture-handling";
import { LeafletHelperService } from "src/app/shared/services/leaflet-helper.service";
import { BoundingBoxDto } from "src/app/shared/generated/model/models";
import { IconComponent } from "../../icon/icon.component";
import { NominatimService } from "src/app/shared/services/nominatim.service";
import { Observable, debounce, of, switchMap, tap, timer } from "rxjs";
import { NgSelectComponent, NgSelectModule } from "@ng-select/ng-select";
import { FormControl, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from "@angular/forms";
import { LegendItem } from "src/app/shared/models/legend-item";
import { Feature, FeatureCollection } from "geojson";
import { DomSanitizer } from "@angular/platform-browser";

@Component({
    selector: "neptune-map",
    standalone: true,
    imports: [CommonModule, IconComponent, NgSelectModule, FormsModule, ReactiveFormsModule],
    templateUrl: "./neptune-map.component.html",
    styleUrls: ["./neptune-map.component.scss"],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: NeptuneMapComponent,
        },
    ],
})
export class NeptuneMapComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(NgSelectComponent) ngSelectComponent: NgSelectComponent;
    public mapID: string = crypto.randomUUID();
    public legendID: string = this.mapID + "Legend";
    public map: Map;
    public tileLayers: { [key: string]: any } = LeafletHelperService.GetDefaultTileLayers();
    public layerControl: L.groupedlayercontrol;
    @Input() boundingBox: BoundingBoxDto;
    @Input() mapHeight: string = "500px";
    @Input() selectedTileLayer: string = "Terrain";
    @Input() showLegend: boolean = true;
    @Input() legendPosition: ControlPosition = "topleft";
    @Output() onMapLoad: EventEmitter<NeptuneMapInitEvent> = new EventEmitter();
    @Output() onOverlayToggle: EventEmitter<L.LayersControlEvent> = new EventEmitter();

    public legendControl: Control;
    public legendItems: LegendItem[] = [];

    public allSearchResults: Feature[] = [];
    public searchString = new FormControl({ value: null, disabled: false });
    public searchResults$: Observable<FeatureCollection>;
    public isSearching: boolean = false;
    private searchCleared: boolean = false;

    constructor(public nominatimService: NominatimService, public leafletHelperService: LeafletHelperService, private sanitizer: DomSanitizer) {}

    ngAfterViewInit(): void {
        const mapOptions: MapOptions = {
            minZoom: 6,
            maxZoom: 17,
            layers: [this.tileLayers[this.selectedTileLayer]],
            fullscreenControl: true,
            fullscreenControlOptions: {
                position: "topleft",
                forceSeparateButton: true,
            },
            gestureHandling: true,
            //            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        } as MapOptions;

        this.map = L.map(this.mapID, mapOptions);
        L.Map.addInitHook("addHandler", "gestureHandling", GestureHandling);
        var loadingControl = L.Control.loading({
            separate: true,
        });
        this.map.addControl(loadingControl);

        this.layerControl = new L.control.groupedLayers(this.tileLayers, null, { collapsed: false }).addTo(this.map);

        this.map.on("load", (event: LeafletEvent) => {
            this.onMapLoad.emit(new NeptuneMapInitEvent(this.map, this.layerControl));
        });

        this.map.on("overlayadd", (event: L.LayersControlEvent) => {
            this.legendItems = this.createLegendItems();
            this.onOverlayToggle.emit(event);
        });

        this.map.on("overlayremove", (event: L.LayersControlEvent) => {
            this.legendItems = this.createLegendItems();
            this.onOverlayToggle.emit(event);
        });

        if (this.boundingBox == null) {
            this.boundingBox = LeafletHelperService.defaultBoundingBox;
        }

        this.map.fitBounds(
            [
                [this.boundingBox.Bottom, this.boundingBox.Left],
                [this.boundingBox.Top, this.boundingBox.Right],
            ],
            null
        );

        if (this.showLegend) {
            const self = this;
            const legendControl = Control.extend({
                onAdd(map: Map) {
                    const domElement = DomUtil.get(self.mapID + "Legend");
                    L.DomEvent.disableClickPropagation(domElement);
                    return domElement;
                },
                onRemove(map: Map) {},
            });
            this.legendControl = new legendControl({
                position: this.legendPosition,
            }).addTo(this.map);
            this.map["showLegend"] = true;
        }
        this.map.fullscreenControl.getContainer().classList.add("leaflet-custom-controls");

        this.searchResults$ = this.searchString.valueChanges.pipe(
            debounce((x) => {
                // debounce search to 500ms when the user is typing in the search
                this.isSearching = true;
                if (this.searchString.value) {
                    return timer(800);
                } else {
                    // don't debounce when the user has cleared the search
                    return timer(800);
                }
            }),
            switchMap((searchString) => {
                this.isSearching = true;
                if (this.searchCleared && !searchString) {
                    return of({ features: [] });
                }
                this.searchCleared = false;
                return this.nominatimService.makeNominatimRequest(searchString);
            }),
            tap((x: FeatureCollection) => {
                this.isSearching = false;
                this.allSearchResults = x?.features ?? [];
            })
        );
    }

    clearSearch() {
        this.searchString.reset();
        this.searchCleared = true;
    }

    public selectCurrent(selectedFeature): void {
        this.map.fitBounds(
            [
                [selectedFeature.bbox[1], selectedFeature.bbox[0]],
                [selectedFeature.bbox[3], selectedFeature.bbox[2]],
            ],
            null
        );
        this.clearSearch();
    }

    private createLegendItems(): LegendItem[] {
        const legendItems = [];

        this.layerControl._layers.forEach((obj) => {
            // Check if it's an overlay and added to the map
            if (obj.overlay && this.map.hasLayer(obj.layer)) {
                const legendItem = new LegendItem();
                legendItem.Title = obj.group && obj.group.name ? obj.group.name : obj.name;
                if (obj.layer.legendHtml){
                    legendItem.LengendHtml = this.sanitizer.bypassSecurityTrustHtml(obj.layer.legendHtml);
                } else if (obj.layer._url) {
                    legendItem.WmsUrl = obj.layer._url;
                    legendItem.WmsLayerName = obj.layer.options.layers;
                    legendItem.WmsLayerStyle = obj.layer.wmsParams.styles;
                }
                
                if (legendItem.Title && (legendItem.LengendHtml || legendItem.WmsUrl) && !legendItems.some((item) => item.Title === legendItem.Title)) {
                    legendItems.push(legendItem);
                }
            }
        });
        return legendItems;
    }

    legendToggle(): void {
        if (this.legendControl._container.classList.contains("leaflet-control-layers-expanded")) {
            this.legendControl._container.className = this.legendControl._container.className.replace(" leaflet-control-layers-expanded", "");
        } else {
            this.legendControl._container.classList.add("leaflet-control-layers-expanded");
        }
    }

    ngOnDestroy(): void {
        console.warn("destroying map");
        if (this.map) {
            this.map.off();
            this.map.remove();
            this.map = null;
        }
    }

    ngOnInit(): void {}
}

export class NeptuneMapInitEvent {
    public map: Map;
    public layerControl: any;
    constructor(map: Map, layerControl: any) {
        this.map = map;
        this.layerControl = layerControl;
    }
}
