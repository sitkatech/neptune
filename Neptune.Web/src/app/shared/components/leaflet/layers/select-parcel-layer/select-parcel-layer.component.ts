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
    public isPerformingDrawAction: boolean = false;
    public drawMapClicked: boolean = false;

    public layer: L.featureGroup;
    public drawnItems: L.featureGroup;
    public drawControl: L.Control;
    public editableDelineationFeatureGroup: L.FeatureGroup = new L.FeatureGroup();

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };
    private testStyle = {
        color: "#000",
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

    private defaultDrawControlSpec: L.Control.DrawConstructorOptions = {
        polyline: false,
        rectangle: false,
        circle: false,
        marker: false,
        circlemarker: false,
        polygon: {
            allowIntersection: false, // Restricts shapes to simple polygons
            drawError: {
                color: "#E1E100", // Color the shape will turn when intersects
                message: "Self-intersecting polygons are not allowed.", // Message that will show when intersect
            },
        },
    };
    private defaultEditControlSpec: L.Control.DrawConstructorOptions = {
        featureGroup: this.editableDelineationFeatureGroup,
        remove: true,
        poly: {
            allowIntersection: false, // Restricts shapes to simple polygons
            drawError: {
                color: "#E1E100", // Color the shape will turn when intersects
                message: "Self-intersecting polygons are not allowed.", // Message that will show when intersect
            },
        },
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
        this.updateLayer();
    }

    public setControl(): void {
        this.drawnItems = L.geoJSON();
        this.drawnItems.addTo(this.map);
        L.EditToolbar.Delete.include({
            removeAllLayers: false,
        });

        this.map
            .on(L.Draw.Event.CREATED, (event) => {
                this.isPerformingDrawAction = false;
                const layer = (event as L.DrawEvents.Created).layer;
                console.log("created");
                this.drawnItems.addLayer(layer);
                this.selectFeatureImpl();
            })
            .on(L.Draw.Event.EDITED, (event) => {
                this.isPerformingDrawAction = false;
                const layers = (event as L.DrawEvents.Edited).layers;
                console.log("edited");
                this.drawnItems.addLayer(layers);
                this.selectFeatureImpl();
            })
            .on(L.Draw.Event.DELETED, (event) => {
                this.isPerformingDrawAction = false;
                const layers = (event as L.DrawEvents.Deleted).layers;
                // layers.eachLayer((layer) => {
                //     var delineationUpsertDto = this.delineations.find((x) => layer.feature.properties.TreatmentBMPID == x.TreatmentBMPID);
                //     delineationUpsertDto.Geometry = null;
                //     delineationUpsertDto.DelineationArea = null;
                // });
                // this.resetDelineationFeatureGroups();
                this.selectFeatureImpl();
            })
            .on(L.Draw.Event.DRAWSTART, () => {
                // if (this.selectedDelineation != null && this.selectedDelineation.DelineationTypeID == DelineationTypeEnum.Centralized) {
                this.editableDelineationFeatureGroup.clearLayers();
                console.log("drawstart");
                // }
            })
            .on(L.Draw.Event.TOOLBAROPENED, () => {
                this.isPerformingDrawAction = true;
                console.log("toolbar opened");
                // this.preStartEditingEditableDelineationFeatureGroup = JSON.stringify(this.editableDelineationFeatureGroup.getLayers().map((x) => x.getLatLngs()));
            })
            .on(L.Draw.Event.TOOLBARCLOSED, () => {
                this.isPerformingDrawAction = false;
                console.log("toolbar closed");
                // this.preStartEditingEditableDelineationFeatureGroup = "";
            });
        this.addOrRemoveDrawControl(true);
    }

    public selectFeatureImpl() {
        if (this.isPerformingDrawAction) {
            return;
        }

        this.drawControl.remove();
        this.drawnItems.setStyle(this.testStyle).bringToFront();
        this.addOrRemoveDrawControl(true);
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
                this.setControl();
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

    public addOrRemoveDrawControl(turnOn: boolean) {
        if (turnOn) {
            var drawOptions = {
                draw: Object.assign({}, this.defaultDrawControlSpec),
                edit: Object.assign({}, this.defaultEditControlSpec),
            };
            console.log(this.selectedParcelIDs.length);
            if (this.selectedParcelIDs.length > 0 || this.drawMapClicked) {
                drawOptions.edit = false;
            } else {
                drawOptions.draw = false;
                drawOptions.edit.edit = false;
            }
            this.drawControl = new L.Control.Draw(drawOptions);
            this.drawControl.addTo(this.map);
            return;
        }
        this.drawControl.remove();
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
}
