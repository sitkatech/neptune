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
import { Observable, Subject, catchError, debounceTime, distinctUntilChanged, filter, map, of, switchMap, tap } from "rxjs";
import { NgSelectComponent, NgSelectModule } from "@ng-select/ng-select";
import { FormsModule } from "@angular/forms";
import { LegendItem } from "src/app/shared/models/legend-item";

@Component({
    selector: "neptune-map",
    standalone: true,
    imports: [CommonModule, IconComponent, NgSelectModule, FormsModule],
    templateUrl: "./neptune-map.component.html",
    styleUrls: ["./neptune-map.component.scss"],
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

    public searchString$: Observable<any>;
    public searchResults$ = new Subject<string>();
    public searchString: string = null;
    public isSearching: boolean;
    public searchLoading = false;

    constructor(public nominatimService: NominatimService, public leafletHelperService: LeafletHelperService) {}

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
        }
        this.map.fullscreenControl.getContainer().classList.add("leaflet-custom-controls");

        this.searchString$ = this.searchResults$.pipe(
            filter((searchTerm) => searchTerm != null),
            distinctUntilChanged(),
            tap((searchTerm) => {
                this.searchLoading = true;
                this.searchString = searchTerm;
            }),
            debounceTime(800),
            switchMap((searchTerm) =>
                this.nominatimService.makeNominatimRequest(searchTerm).pipe(
                    map((x) => x.features.map((y) => y.properties.display_name)),
                    catchError(() => of([])),
                    tap(() => (this.searchLoading = false))
                )
            )
        );
    }

    public makeNominatimRequest(searchValue) {
        this.nominatimService.makeNominatimRequest(searchValue).subscribe((response) => {
            this.map.fitBounds(
                [
                    [response.features[0].bbox[1], response.features[0].bbox[0]],
                    [response.features[0].bbox[3], response.features[0].bbox[2]],
                ],
                null
            );
        });
    }

    private createLegendItems(): LegendItem[] {
        const legendItems = [];

        this.layerControl._layers.forEach((obj) => {
            // Check if it's an overlay and added to the map
            if (obj.overlay && this.map.hasLayer(obj.layer)) {
                const legendItem = new LegendItem();
                legendItem.Title = obj.group ? obj.group.name : obj.name;
                legendItem.WmsUrl = obj.layer._url;
                legendItem.WmsLayerName = obj.layer.options.layers;
                if (!legendItems.some((item) => item.Title === legendItem.Title)) {
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
