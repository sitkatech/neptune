import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { HybridMapGridComponent } from "src/app/shared/components/hybrid-map-grid/hybrid-map-grid.component";
import "leaflet.markercluster";
import * as L from "leaflet";
import { ColDef } from "ag-grid-community";
import { Observable, tap } from "rxjs";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPDisplayDto } from "src/app/shared/generated/model/treatment-bmp-display-dto";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { AsyncPipe } from "@angular/common";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { TreatmentBMPGridDto } from "src/app/shared/generated/model/treatment-bmp-grid-dto";

@Component({
    selector: "treatment-bmps",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, HybridMapGridComponent, AsyncPipe, LoadingDirective],
    templateUrl: "./treatment-bmps.component.html",
})
export class TreatmentBmpsComponent {
    public map: any;
    public layerControl: any;
    private markerClusterLayer: any;
    private markerMap: Map<number, any> = new Map();
    public treatmentBmps$: Observable<TreatmentBMPGridDto[]>;
    public columnDefs: ColDef[];
    public isLoading = true;
    public selectedTreatmentBMPID: number;
    public selectionFromMap: boolean;
    public boundingBox$: Observable<BoundingBoxDto>;
    public customRichTextTypeID = NeptunePageTypeEnum.TreatmentBMP;

    constructor(
        private treatmentBMPService: TreatmentBMPService,
        private utilityFunctionsService: UtilityFunctionsService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private router: Router,
        private confirmService: ConfirmService,
        private alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctionsService.createActionsColumnDef((params: any) => [
                {
                    ActionName: "View",
                    ActionHandler: () => this.router.navigate(["/inventory/treatment-bmp-detail", params.data.TreatmentBMPID]),
                },
                {
                    ActionName: "Delete",
                    ActionIcon: "fa fa-trash text-danger",
                    ActionHandler: () => this.deleteModal(params),
                },
            ]),
            this.utilityFunctionsService.createLinkColumnDef("Name", "TreatmentBMPName", "TreatmentBMPID", {
                InRouterLink: "/inventory/treatment-bmps/",
                FieldDefinitionType: "TreatmentBMP",
                FieldDefinitionLabelOverride: "Name",
            }),
            this.utilityFunctionsService.createLinkColumnDef("Jurisdiction", "StormwaterJurisdictionName", "StormwaterJurisdictionID", {
                InRouterLink: "/inventory/jurisdictions/",
                FieldDefinitionType: "Jurisdiction",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Owner Organization", "OwnerOrganizationName"),
            this.utilityFunctionsService.createBasicColumnDef("Type", "TreatmentBMPTypeName", {
                FieldDefinitionType: "TreatmentBMPType",
                FieldDefinitionLabelOverride: "Type",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Year Built", "YearBuilt"),
            this.utilityFunctionsService.createBasicColumnDef("Notes", "Notes"),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "LatestAssessmentDate", "MM/dd/yyyy"),
            this.utilityFunctionsService.createBasicColumnDef("Last Assessed Score", "LatestAssessmentScore"),
            this.utilityFunctionsService.createBasicColumnDef("# of Assessments", "NumberOfAssessments"),
            this.utilityFunctionsService.createDateColumnDef("Last Maintenance Date", "LatestMaintenanceDate", "MM/dd/yyyy"),
            this.utilityFunctionsService.createBasicColumnDef("# of Maintenance Events", "NumberOfMaintenanceRecords"),
            this.utilityFunctionsService.createBasicColumnDef("Benchmark and Threshold Set?", "BenchmarkAndThresholdSet"),
            this.utilityFunctionsService.createBasicColumnDef("Required Lifespan of Installation", "TreatmentBMPLifespanTypeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Lifespan End Date (if Fixed End Date)", "TreatmentBMPLifespanEndDate"),
            this.utilityFunctionsService.createBasicColumnDef("Required Field Visits/Year", "RequiredFieldVisitsPerYear"),
            this.utilityFunctionsService.createBasicColumnDef("Required Post-Storm Field Visits/Year", "RequiredPostStormFieldVisitsPerYear"),
            this.utilityFunctionsService.createBasicColumnDef("Sizing Basis", "SizingBasisTypeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Status", "TrashCaptureStatusTypeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Effectiveness (%)", "TrashCaptureEffectiveness"),
            this.utilityFunctionsService.createBasicColumnDef("Delineation Type", "DelineationTypeDisplayName"),
        ];
        this.treatmentBmps$ = this.treatmentBMPService.listTreatmentBMP().pipe(tap(() => (this.isLoading = false)));
        this.boundingBox$ = this.stormwaterJurisdictionService.getBoundingBoxStormwaterJurisdiction();
    }

    handleMapReady(event: any) {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.addOrUpdateClusterLayer();
    }

    private addOrUpdateClusterLayer() {
        if (!this.map || !this.treatmentBmps$) return;
        this.treatmentBmps$.subscribe((bmps) => {
            if (this.markerClusterLayer) {
                this.map.removeLayer(this.markerClusterLayer);
                this.layerControl.removeLayer(this.markerClusterLayer);
            }
            this.markerMap.clear();
            this.markerClusterLayer = L.markerClusterGroup({
                iconCreateFunction: function (cluster) {
                    var childCount = cluster.getChildCount();
                    return L.divIcon({
                        html: "<div><span>" + childCount + "</span></div>",
                        className: "treatment-bmp-cluster",
                        iconSize: L.point(40, 40),
                    });
                },
            });
            bmps.forEach((bmp) => {
                if (bmp.Latitude && bmp.Longitude) {
                    const marker = L.marker([bmp.Latitude, bmp.Longitude], {
                        icon: this.getMarkerIcon(bmp.TreatmentBMPID === this.selectedTreatmentBMPID),
                    });
                    marker.bindPopup(`<b>Name:</b> ${bmp.TreatmentBMPName}<br><b>Type:</b> ${bmp.TreatmentBMPTypeName}`);
                    marker.on("click", () => {
                        this.selectionFromMap = true;
                        this.selectedTreatmentBMPID = bmp.TreatmentBMPID;
                        this.highlightAndZoomToMarker(bmp.TreatmentBMPID);
                    });
                    this.markerMap.set(bmp.TreatmentBMPID, marker);
                    this.markerClusterLayer.addLayer(marker);
                }
            });
            this.layerControl.addOverlay(this.markerClusterLayer, "Treatment BMPs");
            this.map.addLayer(this.markerClusterLayer);
        });
    }

    private getMarkerIcon(isSelected: boolean) {
        return L.icon({
            iconUrl: isSelected ? "assets/main/map-icons/marker-icon-blue.png" : "assets/main/map-icons/marker-icon-orange.png",
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowUrl: "assets/main/map-icons/marker-shadow.png",
            shadowSize: [41, 41],
        });
    }

    private highlightAndZoomToMarker(treatmentBMPID: number) {
        this.markerMap.forEach((marker, id) => {
            marker.setIcon(this.getMarkerIcon(id === treatmentBMPID));
        });
        const marker = this.markerMap.get(treatmentBMPID);
        if (marker) {
            marker.openPopup();
            // Zoom to max zoom level to ensure marker is not clustered
            const maxZoom = this.map.getMaxZoom ? this.map.getMaxZoom() : 18;
            this.map.setView(marker.getLatLng(), maxZoom, { animate: true });
            // Optionally, spiderfy the cluster if needed
            if (this.markerClusterLayer && this.markerClusterLayer.spiderfy) {
                const cluster = this.markerClusterLayer.getVisibleParent(marker);
                if (cluster && cluster.spiderfy) {
                    cluster.spiderfy();
                }
            }
        }
    }

    private deleteModal(params: any) {
        const confirmOptions = {
            title: "Delete BMP",
            message: `<p>You are about to delete ${params.data.TreatmentBMPName ?? "this BMP"}.</p><p>Are you sure you wish to proceed?</p>`,
            buttonClassYes: "btn btn-danger",
            buttonTextYes: "Delete",
            buttonTextNo: "Cancel",
        };
        this.confirmService.confirm(confirmOptions).then((confirmed) => {
            if (confirmed) {
                this.treatmentBMPService.deleteTreatmentBMP(params.data.TreatmentBMPID).subscribe(() => {
                    this.alertService.pushAlert(new Alert("Successfully deleted BMP", AlertContext.Success));
                    params.api.applyTransaction({ remove: [params.data] });
                });
            }
        });
    }

    onSelectedTreatmentBMPChangedFromGrid(selectedTreatmentBMPID: number) {
        if (this.selectedTreatmentBMPID === selectedTreatmentBMPID) return;
        this.selectedTreatmentBMPID = selectedTreatmentBMPID;
        this.selectionFromMap = false;
        // Update all marker icons to reflect the new selection
        this.markerMap.forEach((marker, id) => {
            marker.setIcon(this.getMarkerIcon(id === selectedTreatmentBMPID));
        });
        this.highlightAndZoomToMarker(selectedTreatmentBMPID);
        return this.selectedTreatmentBMPID;
    }
}
