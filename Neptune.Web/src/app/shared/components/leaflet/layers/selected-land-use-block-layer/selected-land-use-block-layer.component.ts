import { AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChange } from "@angular/core";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { CommonModule } from "@angular/common";
import { WfsService } from "src/app/shared/services/wfs.service";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";

@Component({
    selector: "selected-land-use-block-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./selected-land-use-block-layer.component.html",
    styleUrl: "./selected-land-use-block-layer.component.scss",
})
export class SelectedLandUseBlockLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    @Input() selectedLandUseBlockID: number;

    @Output() layerBoundsCalculated = new EventEmitter();
    @Output() landUseBlockSelected = new EventEmitter<number>();

    public isLoading: boolean = false;

    public layer: L.featureGroup;

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    constructor(private wfsService: WfsService, private groupByPipe: GroupByPipe) {
        super();
    }

    ngOnChanges(changes: any): void {
        if (changes.selectedLandUseBlockID) {
            if (changes.selectedLandUseBlockID.previousValue == changes.selectedLandUseBlockID.currentValue) return;
            this.selectedLandUseBlockID = changes.selectedLandUseBlockID.currentValue;
            this.highlightSelectedLandUseBlock();
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

        this.addLandUseBlocksToLayer();

        this.layer.addTo(this.map);
        this.isLoading = false;
    }

    private addLandUseBlocksToLayer() {
        let cql_filter = ``;

        this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:LandUseBlocks", cql_filter, "LandUseBlockID").subscribe((response) => {
            if (response.length == 0) return;

            const featuresGroupedByLandUseBlockID = this.groupByPipe.transform(response, "properties.LandUseBlockID");

            Object.keys(featuresGroupedByLandUseBlockID).forEach((landUseBlockID) => {
                const geoJson = L.geoJSON(featuresGroupedByLandUseBlockID[landUseBlockID]);
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.1 });
                });

                geoJson.on("click", (e) => {
                    this.onLandUseBlockSelected(Number(landUseBlockID));
                });

                geoJson.addTo(this.layer);
            });
        });
    }

    private onLandUseBlockSelected(landUseBlockID: number) {
        this.selectedLandUseBlockID = landUseBlockID;
        this.highlightSelectedLandUseBlock();

        this.landUseBlockSelected.emit(landUseBlockID);
    }

    private highlightSelectedLandUseBlock() {
        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.LandUseBlockID == this.selectedLandUseBlockID) {
                layer.setStyle(this.highlightStyle);
                this.map.fitBounds(layer.getBounds());
            } else {
                layer.setStyle(null);
            }
        });
    }

    private setupLayer() {
        this.layer = L.geoJSON();
        this.initLayer();
    }
}
