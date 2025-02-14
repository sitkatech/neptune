import { AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChange } from "@angular/core";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { WfsService } from "src/app/shared/services/wfs.service";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";

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
        //this.layer.clearLayers();

        this.addOVTAAreasToLayer();

        this.layer.addTo(this.map);
        this.isLoading = false;
    }

    private addOVTAAreasToLayer() {
        console.log(this.boundingBox);
        const bbox = this.boundingBox != null ? `bbox(ParcelGeometry,${this.boundingBox.Left},${this.boundingBox.Top},${this.boundingBox.Right},${this.boundingBox.Bottom})` : null;
        console.log(bbox);
        this.wfsService.getGeoserverWFSLayer("OCStormwater:Parcels", "ParcelID in (1,2)", "ParcelID").subscribe((response) => {
            // console.log(response);
            if (response.length == 0) return;
            const featuresGroupedByOVTAAreaID = this.groupByPipe.transform(response, "properties.ParcelID");
            Object.keys(featuresGroupedByOVTAAreaID).forEach((ovtaAreaID) => {
                const geoJson = L.geoJSON(featuresGroupedByOVTAAreaID[ovtaAreaID], {
                    style: this.highlightStyle, //this.styleDictionary[featuresGroupedByOVTAAreaID[ovtaAreaID][0].properties.Score],
                });
                console.log(geoJson);
                // geoJson.on("mouseover", (e) => {
                //     geoJson.setStyle({ fillOpacity: 0.5 });
                // });
                // geoJson.on("mouseout", (e) => {
                //     geoJson.setStyle({ fillOpacity: 0.1 });
                // });
                // geoJson.on("click", (e) => {
                //     this.onOVTAAreaSelected(Number(ovtaAreaID));
                // });
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
        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.OVTAAreaID == this.selectedOVTAAreaID) {
                layer.setStyle(this.highlightStyle);
                this.map.fitBounds(layer.getBounds());
            } else {
                //layer.setStyle(this.styleDictionary[geoJsonLayers[0].feature.properties.Score]);
            }
        });
    }

    private setupLayer() {
        this.layer = L.geoJSON();
        this.initLayer();
    }
}
