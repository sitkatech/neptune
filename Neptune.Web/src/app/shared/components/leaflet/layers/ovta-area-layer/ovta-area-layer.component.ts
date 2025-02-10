import { AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChange } from "@angular/core";
import * as L from "leaflet";
import { environment } from "src/environments/environment";
import { MapLayerBase } from "../map-layer-base.component";
import { CommonModule } from "@angular/common";
import { WfsService } from "src/app/shared/services/wfs.service";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";

@Component({
    selector: "ovta-area-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./ovta-area-layer.component.html",
    styleUrl: "./ovta-area-layer.component.scss",
})
export class OvtaAreaLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    @Input() selectedOVTAAreaID: number;
    @Input() ovtaAreaID: number;

    @Output() layerBoundsCalculated = new EventEmitter();
    @Output() ovtaAreaSelected = new EventEmitter<number>();

    public isLoading: boolean = false;

    public layer: L.featureGroup;

    private defaultStyle = {
        color: "#3388ff",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

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
        if (changes.selectedOVTAAreaID) {
            if (changes.selectedOVTAAreaID.previousValue == changes.selectedOVTAAreaID.currentValue) return;
            this.selectedOVTAAreaID = changes.selectedOVTAAreaID.currentValue;
            this.highlightSelectedOVTAArea();
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

        this.addOVTAAreasToLayer();

        this.layer.addTo(this.map);
        this.isLoading = false;
    }

    private addOVTAAreasToLayer() {
        let cql_filter = ``;
        if (this.ovtaAreaID) {
            cql_filter += `OVTAAreaID == ${this.ovtaAreaID}`;
        }

        this.wfsService.getGeoserverWFSLayer("OCStormwater:AssessmentAreaExport", cql_filter, "OVTAAreaID").subscribe((response) => {
            if (response.length == 0) return;

            const featuresGroupedByOVTAAreaID = this.groupByPipe.transform(response, "properties.OVTAAreaID");

            Object.keys(featuresGroupedByOVTAAreaID).forEach((ovtaAreaID) => {
                const geoJson = L.geoJSON(featuresGroupedByOVTAAreaID[ovtaAreaID], {
                    style: this.defaultStyle,
                });
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.1 });
                });

                geoJson.on("click", (e) => {
                    this.onOVTAAreaSelected(Number(ovtaAreaID));
                });

                geoJson.addTo(this.layer);
            });

            const bounds = this.layer.getBounds();
            this.map.fitBounds(bounds);
            this.layerBoundsCalculated.emit(bounds);
        });
    }

    private onOVTAAreaSelected(ovtaAreaID: number) {
        this.selectedOVTAAreaID = ovtaAreaID;
        this.highlightSelectedOVTAArea();

        this.ovtaAreaSelected.emit(ovtaAreaID);
    }

    private highlightSelectedOVTAArea() {
        // clear styles
        this.layer.setStyle(this.defaultStyle);

        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.OVTAAreaID == this.selectedOVTAAreaID) {
                layer.setStyle(this.highlightStyle);
                this.map.fitBounds(layer.getBounds());
            }
        });
    }

    private setupLayer() {
        this.layer = L.geoJSON();
        this.initLayer();
    }
}
