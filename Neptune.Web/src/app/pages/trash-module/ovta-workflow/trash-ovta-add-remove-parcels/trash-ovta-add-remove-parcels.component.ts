import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { Router, ActivatedRoute } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { NeptuneMapInitEvent, NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { WfsService } from "src/app/shared/services/wfs.service";
import * as L from "leaflet";
import { AsyncPipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OvtaObservationLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-observation-layer/ovta-observation-layer.component";
import { OnlandVisualTrashAssessmentAddRemoveParcelsDto } from "src/app/shared/generated/model/models";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";

@Component({
    selector: "trash-ovta-add-remove-parcels",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NeptuneMapComponent, AsyncPipe, NgIf, OvtaObservationLayerComponent],
    templateUrl: "./trash-ovta-add-remove-parcels.component.html",
    styleUrl: "./trash-ovta-add-remove-parcels.component.scss",
})
export class TrashOvtaAddRemoveParcelsComponent {
    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentAddRemoveParcelsDto>;
    public mapHeight = window.innerHeight - window.innerHeight * 0.4 + "px";
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public bounds: any;
    public ovtaID: number;
    public isLoadingSubmit = false;

    public selectedParcelIDs: number[] = [];

    public layer: L.FeatureGroup = new L.FeatureGroup();
    public transectLineLayer: L.FeatureGroup = new L.FeatureGroup();

    private defaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    private transectLineStyle = {
        color: "#f70a0a",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    constructor(
        private router: Router,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private route: ActivatedRoute,
        private wfsService: WfsService,
        private groupByPipe: GroupByPipe,
        private alertService: AlertService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService
    ) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDAddOrRemoveParcelGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent, boundingBox, transectLineGeometry): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.addTransectLine(transectLineGeometry);
        this.addParcelsToLayer(boundingBox);

        this.layer.addTo(this.map);
        this.transectLineLayer.addTo(this.map);
        this.mapIsReady = true;
    }

    public handleLayerBoundsCalculated(bounds: any) {
        this.bounds = bounds;
    }

    public setSelectedParcels(event) {
        this.selectedParcelIDs = event;
    }

    public save(andContinue: boolean = false) {
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDParcelGeometriesPost(this.ovtaID, this.selectedParcelIDs).subscribe(() => {
            this.alertService.clearAlerts();
            this.alertService.pushAlert(new Alert("Your observations were successfully updated.", AlertContext.Success));
            this.ovtaWorkflowProgressService.updateProgress(this.ovtaID);
            if (andContinue) {
                this.router.navigate([`../../${this.ovtaID}/refine-assessment-area`], { relativeTo: this.route });
            }
        });
    }

    public resetZoom() {
        const bounds = this.layer.getBounds();
        this.map.fitBounds(bounds);
    }

    private addTransectLine(transectLine) {
        this.addFeatureCollectionToFeatureGroup(JSON.parse(transectLine), this.transectLineLayer);
    }

    private addParcelsToLayer(boundingBox) {
        const bbox = boundingBox != null ? `${boundingBox.Bottom},${boundingBox.Right},${boundingBox.Top},${boundingBox.Left}` : null;
        this.wfsService.getGeoserverWFSLayer("OCStormwater:Parcels", "ParcelID", bbox).subscribe((response) => {
            if (response.length == 0) return;
            const featuresGroupedByParcelID = this.groupByPipe.transform(response, "properties.ParcelID");
            Object.keys(featuresGroupedByParcelID).forEach((parcelID) => {
                const geoJson = L.geoJSON(featuresGroupedByParcelID[parcelID], {
                    style: this.defaultStyle,
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
        });
    }

    private onParcelSelected(parcelID: number) {
        if (this.selectedParcelIDs.length > 0 && this.selectedParcelIDs.find((x) => x == parcelID)) {
            this.selectedParcelIDs = this.selectedParcelIDs.filter((x) => x != parcelID);
        } else {
            this.selectedParcelIDs.push(parcelID);
        }
        this.highlightSelectedParcel(parcelID);
    }

    private highlightSelectedParcel(parcelID) {
        this.layer.eachLayer((layer) => {
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.ParcelID == parcelID) {
                if (geoJsonLayers[0].options.color == this.highlightStyle.color) {
                    layer.setStyle(this.defaultStyle);
                } else {
                    layer.setStyle(this.highlightStyle);
                }

                this.map.fitBounds(layer.getBounds());
            }
        });
    }

    public addFeatureCollectionToFeatureGroup(featureJsons: any, featureGroup: L.FeatureGroup) {
        L.geoJson(featureJsons, {
            onEachFeature: (feature, layer) => {
                layer.setStyle(this.transectLineStyle);
                if (layer.getLayers) {
                    layer.getLayers().forEach((l) => {
                        featureGroup.addLayer(l);
                    });
                } else {
                    featureGroup.addLayer(layer);
                }
            },
        });
    }
}
