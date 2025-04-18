import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { Router, ActivatedRoute } from "@angular/router";
import { Observable, of, switchMap, tap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { NeptuneMapInitEvent, NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
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
import { LandUseBlockLayerComponent } from "../../../../shared/components/leaflet/layers/land-use-block-layer/land-use-block-layer.component";
import { WorkflowBodyComponent } from "../../../../shared/components/workflow-body/workflow-body.component";
import { TransectLineLayerComponent } from "../../../../shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";
import { ParcelLayerComponent } from "../../../../shared/components/leaflet/layers/parcel-layer/parcel-layer.component";

@Component({
    selector: "trash-ovta-add-remove-parcels",
    standalone: true,
    imports: [
        PageHeaderComponent,
        AlertDisplayComponent,
        NeptuneMapComponent,
        AsyncPipe,
        NgIf,
        OvtaObservationLayerComponent,
        LandUseBlockLayerComponent,
        WorkflowBodyComponent,
        TransectLineLayerComponent,
        ParcelLayerComponent,
    ],
    templateUrl: "./trash-ovta-add-remove-parcels.component.html",
    styleUrl: "./trash-ovta-add-remove-parcels.component.scss",
})
export class TrashOvtaAddRemoveParcelsComponent {
    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentAddRemoveParcelsDto>;
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public ovtaID: number;
    public isLoadingSubmit = false;
    public isLoading: boolean = false;

    public selectedParcelIDs: number[] = [];
    public selectedParcelsLayer: L.GeoJSON<any>;

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    constructor(
        private router: Router,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private route: ActivatedRoute,
        private wfsService: WfsService,
        private alertService: AlertService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService
    ) {}

    ngOnInit(): void {
        this.isLoading = true;
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDParcelsGet(
                    params[routeParams.onlandVisualTrashAssessmentID]
                );
            }),
            tap((ovta) => {
                this.ovtaID = ovta.OnlandVisualTrashAssessmentID;
                this.selectedParcelIDs = ovta.SelectedParcelIDs;
                this.isLoading = false;
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent, onlandVisualTrashAssessment: OnlandVisualTrashAssessmentAddRemoveParcelsDto): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.addSelectedParcelsToMap();
        this.enableDisableParcelClickEvent(onlandVisualTrashAssessment);
        this.mapIsReady = true;
    }

    private addSelectedParcelsToMap() {
        if (this.selectedParcelsLayer) {
            this.map.removeLayer(this.selectedParcelsLayer);
        }
        if (this.selectedParcelIDs.length > 0) {
            this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:Parcels", `ParcelID in (${this.selectedParcelIDs.join(",")})`, "ParcelID").subscribe((response) => {
                this.selectedParcelsLayer = L.geoJSON(response, { style: this.highlightStyle });
                this.selectedParcelsLayer.addTo(this.map);
                this.map.fitBounds(this.selectedParcelsLayer.getBounds());
            });
        }
    }

    public save(andContinue: boolean = false) {
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDParcelsPost(this.ovtaID, this.selectedParcelIDs).subscribe(() => {
            this.alertService.clearAlerts();
            this.alertService.pushAlert(new Alert("Your observations were successfully updated.", AlertContext.Success));
            this.ovtaWorkflowProgressService.updateProgress(this.ovtaID);
            if (andContinue) {
                this.router.navigate([`../../${this.ovtaID}/refine-assessment-area`], { relativeTo: this.route });
            }
        });
    }

    public refreshParcels() {
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDRefreshParcelsPost(this.ovtaID).subscribe((ovta) => {
            this.selectedParcelIDs = ovta.SelectedParcelIDs;
            this.addSelectedParcelsToMap();
            this.enableDisableParcelClickEvent(ovta);
            this.onlandVisualTrashAssessment$ = of(ovta);
        });
    }

    private enableDisableParcelClickEvent(ovta: OnlandVisualTrashAssessmentAddRemoveParcelsDto) {
        if (ovta.IsDraftGeometryManuallyRefined) {
            this.map.off("click");
        } else {
            this.map.on("click", (event: L.LeafletMouseEvent): void => {
                this.wfsService.getParcelByCoordinate(event.latlng.lng, event.latlng.lat).subscribe((parcelsFeatureCollection: L.FeatureCollection) => {
                    parcelsFeatureCollection.features.forEach((feature: L.Feature) => {
                        const parcelID = feature.properties.ParcelID;
                        if (this.selectedParcelIDs.includes(parcelID)) {
                            this.selectedParcelIDs.splice(this.selectedParcelIDs.indexOf(parcelID), 1);
                        } else {
                            this.selectedParcelIDs.push(parcelID);
                        }
                    });

                    this.addSelectedParcelsToMap();
                });
            });
        }
    }
}
