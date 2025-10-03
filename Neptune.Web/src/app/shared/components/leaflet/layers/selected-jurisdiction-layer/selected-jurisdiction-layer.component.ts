import { AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChange } from "@angular/core";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { WfsService } from "src/app/shared/services/wfs.service";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";

@Component({
    selector: "selected-jurisdiction-layer",
    imports: [],
    templateUrl: "./selected-jurisdiction-layer.component.html",
    styleUrl: "./selected-jurisdiction-layer.component.scss",
})
export class SelectedJurisdictionLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    @Input() selectedJurisdictionID: number;

    @Output() layerBoundsCalculated = new EventEmitter();
    @Output() jurisdictionSelected = new EventEmitter<number>();

    public isLoading: boolean = false;
    public layer: L.FeatureGroup;

    private styleDictionary = {
        Default: {
            color: "#FF6C2D",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
        Highlight: {
            color: "#fcfc12",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
    };

    constructor(private wfsService: WfsService, private groupByPipe: GroupByPipe) {
        super();
    }

    ngOnChanges(changes: any): void {
        if (changes.selectedJurisdictionID) {
            if (changes.selectedJurisdictionID.previousValue == changes.selectedJurisdictionID.currentValue) return;
            this.selectedJurisdictionID = changes.selectedJurisdictionID.currentValue;
            this.highlightSelectedJurisdiction();
        } else if (Object.values(changes).some((x: SimpleChange) => x.firstChange === false)) {
            this.updateLayer();
        }
    }

    ngAfterViewInit(): void {
        this.setupLayer();
        this.updateLayer();
    }

    private updateLayer() {
        this.isLoading = true;
        this.layer.clearLayers();
        this.addJurisdictionsToLayer();
        this.layer.addTo(this.map);
        this.isLoading = false;
    }

    private addJurisdictionsToLayer() {
        let cql_filter = ``;
        this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:Jurisdictions", cql_filter, "StormwaterJurisdictionID").subscribe((response) => {
            if (response.length == 0) return;
            const featuresGroupedByJurisdictionID = this.groupByPipe.transform(response, "properties.StormwaterJurisdictionID");
            Object.keys(featuresGroupedByJurisdictionID).forEach((jurisdictionID) => {
                const geoJson = L.geoJSON(featuresGroupedByJurisdictionID[jurisdictionID], {
                    style: this.styleDictionary["Default"],
                });
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.1 });
                });
                geoJson.on("click", (e) => {
                    this.onJurisdictionSelected(Number(jurisdictionID));
                });
                geoJson.addTo(this.layer);
            });
        });
    }

    private onJurisdictionSelected(jurisdictionID: number) {
        this.selectedJurisdictionID = jurisdictionID;
        this.highlightSelectedJurisdiction();
        this.jurisdictionSelected.emit(jurisdictionID);
    }

    private highlightSelectedJurisdiction() {
        this.layer.eachLayer((layer) => {
            if (layer instanceof L.Marker && layer.options.icon) {
                return;
            }
            if (layer instanceof L.GeoJSON) {
                const geoJsonLayers = layer.getLayers() as (L.Path & { feature?: GeoJSON.Feature })[];
                if (geoJsonLayers[0].feature.properties.StormwaterJurisdictionID == this.selectedJurisdictionID) {
                    layer.setStyle(this.styleDictionary["Highlight"]);
                    if ("getBounds" in layer && typeof layer.getBounds === "function") {
                        this.map.fitBounds(layer.getBounds());
                    }
                } else {
                    layer.setStyle(this.styleDictionary["Default"]);
                }
            }
        });
    }

    private setupLayer() {
        this.layer = L.geoJSON();
        this.initLayer();
    }
}
