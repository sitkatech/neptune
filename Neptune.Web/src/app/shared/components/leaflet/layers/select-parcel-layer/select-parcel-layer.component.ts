import { AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChange } from "@angular/core";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { WfsService } from "src/app/shared/services/wfs.service";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { environment } from "src/environments/environment";

@Component({
    selector: "select-parcel-layer",
    standalone: true,
    imports: [],
    templateUrl: "./select-parcel-layer.component.html",
    styleUrl: "./select-parcel-layer.component.scss",
})
export class SelectParcelLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    @Input() selectedOVTAAreaID: number;
    @Input() boundingBox: BoundingBoxDto;

    @Output() layerBoundsCalculated = new EventEmitter();
    @Output() ovtaAreaSelected = new EventEmitter<number>();

    public isLoading: boolean = false;
    public wmsOptions: L.WMSOptions;

    public layer: L.featureGroup;

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };
    private wqmpStyle = {
        color: "#ff6ba9",
        weight: 2,
        opacity: 0.9,
        fillOpacity: 0.1,
    };
    private noWQMPsStyle = {
        color: "#bbbbbb",
        weight: 1,
        opacity: 0.7,
    };

    constructor(private wfsService: WfsService, private groupByPipe: GroupByPipe) {
        super();
    }

    ngOnChanges(changes: any): void {
        if (changes.selectedOVTAAreaID) {
            if (changes.selectedOVTAAreaID.previousValue == changes.selectedOVTAAreaID.currentValue) return;
            this.selectedOVTAAreaID = changes.selectedOVTAAreaID.currentValue;
            this.highlightSelectedParcel();
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

        this.addParcelsToLayer();

        this.layer.addTo(this.map);
        this.isLoading = false;
    }

    private addParcelsToLayer() {
        const bbox = this.boundingBox != null ? `${this.boundingBox.Bottom},${this.boundingBox.Right},${this.boundingBox.Top},${this.boundingBox.Left}` : null;
        this.wfsService.getGeoserverWFSLayer("OCStormwater:Parcels", "", "ParcelID", bbox).subscribe((response) => {
            if (response.length == 0) return;
            const featuresGroupedByParcelID = this.groupByPipe.transform(response, "properties.ParcelID");
            Object.keys(featuresGroupedByParcelID).forEach((parcelID) => {
                const geoJson = L.geoJSON(featuresGroupedByParcelID[parcelID], {
                    style: featuresGroupedByParcelID[parcelID][0].properties.WQMPCount > 0 ? this.wqmpStyle : this.noWQMPsStyle,
                });
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.1 });
                });
                geoJson.on("click", (e) => {
                    this.onParcelSelected(Number(parcelID));
                });
                geoJson.addTo(this.layer);
            });
            const bounds = this.layer.getBounds();
            this.map.fitBounds(bounds);
            this.layerBoundsCalculated.emit(bounds);
        });
    }

    private onParcelSelected(parcelID: number) {
        this.selectedOVTAAreaID = parcelID;
        this.highlightSelectedParcel();

        this.ovtaAreaSelected.emit(parcelID);
    }

    private highlightSelectedParcel() {
        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.ParcelID == this.selectedOVTAAreaID) {
                if (geoJsonLayers[0].options.color == this.highlightStyle.color) {
                    layer.setStyle(geoJsonLayers[0].feature.properties.WQMPCount > 0 ? this.wqmpStyle : this.noWQMPsStyle);
                    console.log(geoJsonLayers[0].options.color);
                } else {
                    layer.setStyle(this.highlightStyle);
                }

                this.map.fitBounds(layer.getBounds());
            }
        });
    }

    private setupLayer() {
        this.layer = L.geoJSON();
        this.initLayer();
    }
}
