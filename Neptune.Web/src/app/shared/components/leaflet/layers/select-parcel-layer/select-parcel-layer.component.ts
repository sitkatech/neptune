import { AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChange } from "@angular/core";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { WfsService } from "src/app/shared/services/wfs.service";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";

@Component({
    selector: "select-parcel-layer",
    standalone: true,
    imports: [],
    templateUrl: "./select-parcel-layer.component.html",
    styleUrl: "./select-parcel-layer.component.scss",
})
export class SelectParcelLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    @Input() boundingBox: BoundingBoxDto;
    @Input() ovtaAreaID: number;

    @Output() layerBoundsCalculated = new EventEmitter();
    @Output() parcelsSelected = new EventEmitter<number[]>();

    public isLoading: boolean = false;
    public wmsOptions: L.WMSOptions;
    selectedParcelIDs: number[] = [];

    public layer: L.featureGroup;
    public drawnItems: L.featureGroup;
    public drawControl: L.Control;

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

    constructor(private wfsService: WfsService, private groupByPipe: GroupByPipe, private ovtaAreaService: OnlandVisualTrashAssessmentAreaService) {
        super();
    }

    // ngOnChanges(changes: any): void {
    //     if (changes.selectedOVTAAreaID) {
    //         if (changes.selectedOVTAAreaID.previousValue == changes.selectedOVTAAreaID.currentValue) return;
    //         this.selectedParcelIDs = changes.selectedOVTAAreaID.currentValue;
    //         this.highlightSelectedParcel();
    //     } else if (Object.values(changes).some((x: SimpleChange) => x.firstChange === false)) {
    //         this.updateLayer();
    //     }
    // }

    ngAfterViewInit(): void {
        this.setupLayer();
        // this.drawnItems = new L.FeatureGroup();
        // var drawOptions = this.getDrawOptions(this.layer);
        // this.drawControl = new L.Control.Draw(drawOptions);
        // this.map.addControl(this.drawControl);

        // // this.map.on("draw:created", function (e) {
        //     console.log("Hello");
        //     console.log(e);
        // });
        this.updateLayer();
    }

    private updateLayer() {
        this.isLoading = true;
        this.layer.clearLayers();

        this.addOVTAAreaToLayer();
        this.addParcelsToLayer();

        this.layer.addTo(this.map);
        this.isLoading = false;
    }

    private addOVTAAreaToLayer() {
        this.ovtaAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDParcelGeometriesGet(this.ovtaAreaID).subscribe((parcels) => {
            this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:Parcels", `ParcelID in (${parcels.map((x) => x.ParcelID)})`, "ParcelID").subscribe((response) => {
                const geoJson = L.geoJSON(response, { style: this.highlightStyle });
                geoJson.addTo(this.layer);
                this.selectedParcelIDs = parcels.map((x) => x.ParcelID);
                const bounds = this.layer.getBounds();

                this.map.fitBounds(bounds);
                this.layerBoundsCalculated.emit(bounds);
            });
        });
    }

    private addParcelsToLayer() {
        const bbox = this.boundingBox != null ? `${this.boundingBox.Bottom},${this.boundingBox.Right},${this.boundingBox.Top},${this.boundingBox.Left}` : null;
        this.wfsService.getGeoserverWFSLayer("OCStormwater:Parcels", "ParcelID", bbox).subscribe((response) => {
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
            // const bounds = this.layer.getBounds();
            // this.map.fitBounds(bounds);
            // this.layerBoundsCalculated.emit(bounds);
        });
    }

    private onParcelSelected(parcelID: number) {
        if (this.selectedParcelIDs.length > 0 && this.selectedParcelIDs.find((x) => x == parcelID)) {
            this.selectedParcelIDs = this.selectedParcelIDs.filter((x) => x != parcelID);
        } else {
            this.selectedParcelIDs.push(parcelID);
        }
        this.highlightSelectedParcel(parcelID);

        this.parcelsSelected.emit(this.selectedParcelIDs);
    }

    private highlightSelectedParcel(parcelID) {
        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.ParcelID == parcelID) {
                if (geoJsonLayers[0].options.color == this.highlightStyle.color) {
                    layer.setStyle(geoJsonLayers[0].feature.properties.WQMPCount > 0 ? this.wqmpStyle : this.noWQMPsStyle);
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

    private getDrawOptions(editableFeatureGroup) {
        var options = {
            position: "topleft",
            draw: {
                polyline: false,
                polygon: {
                    allowIntersection: false, // Restricts shapes to simple polygons
                    drawError: {
                        color: "#e1e100", // Color the shape will turn when intersects
                        message: "Self-intersecting polygons are not allowed.", // Message that will show when intersect
                    },
                },
                circle: false, // Turns off this drawing tool
                rectangle: false,
                marker: false,
            },
            edit: {
                featureGroup: editableFeatureGroup, //REQUIRED!!
                edit: {
                    maintainColor: true,
                    opacity: 0.3,
                },
                remove: true,
            },
        };
        return options;
    }
}
