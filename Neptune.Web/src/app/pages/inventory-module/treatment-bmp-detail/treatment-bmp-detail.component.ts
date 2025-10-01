import { Component, OnInit, ViewChild, TemplateRef, Input } from "@angular/core";
import { Router, RouterLink } from "@angular/router";
import { DatePipe, AsyncPipe, CommonModule } from "@angular/common";
import { Observable } from "rxjs";
import { switchMap, tap } from "rxjs/operators";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import * as L from "leaflet";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPImageByTreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp-image-by-treatment-bmp.service";
import { TreatmentBMPTypeCustomAttributeTypeService } from "src/app/shared/generated/api/treatment-bmp-type-custom-attribute-type.service";
import { TreatmentBMPDto } from "src/app/shared/generated/model/treatment-bmp-dto";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { NeptuneMapComponent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { InventoriedBMPsLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-layer/inventoried-bmps-layer.component";
import { TreatmentBMPLifespanTypeEnum } from "src/app/shared/generated/enum/treatment-b-m-p-lifespan-type-enum";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { ImageCarouselComponent } from "src/app/shared/components/image-carousel/image-carousel.component";
import { ButtonLoadingDirective } from "src/app/shared/directives/button-loading.directive";
import { ModeledBmpPerformanceComponent } from "src/app/shared/components/modeled-bmp-performance/modeled-bmp-performance.component";
import { WaterQualityManagementPlanModelingApproachEnum } from "src/app/shared/generated/enum/water-quality-management-plan-modeling-approach-enum";
import { LandUseTableComponent } from "src/app/shared/components/land-use-table/land-use-table.component";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { CustomAttributesDisplayComponent } from "src/app/shared/components/custom-attributes-display/custom-attributes-display.component";
import { CustomAttributeDto, TreatmentBMPTypeCustomAttributeTypeDto } from "src/app/shared/generated/model/models";
import { CustomAttributeTypePurposeEnum, CustomAttributeTypePurposes } from "src/app/shared/generated/enum/custom-attribute-type-purpose-enum";
import { TreatmentBMPTypeService } from "src/app/shared/generated/api/treatment-bmp-type.service";

@Component({
    selector: "treatment-bmp-detail",
    templateUrl: "./treatment-bmp-detail.component.html",
    styleUrls: ["./treatment-bmp-detail.component.scss"],
    standalone: true,
    imports: [
        CommonModule,
        RouterLink,
        DatePipe,
        AsyncPipe,
        PageHeaderComponent,
        AlertDisplayComponent,
        FieldDefinitionComponent,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        StormwaterNetworkLayerComponent,
        JurisdictionsLayerComponent,
        WqmpsLayerComponent,
        DelineationsLayerComponent,
        InventoriedBMPsLayerComponent,
        LoadingDirective,
        ImageCarouselComponent,
        ButtonLoadingDirective,
        ModeledBmpPerformanceComponent,
        LandUseTableComponent,
        NeptuneGridComponent,
        CustomAttributesDisplayComponent,
    ],
})
export class TreatmentBmpDetailComponent implements OnInit {
    /**
     * Stub: Edit URL for Modeling Attributes
     */
    editTreatmentBMPModelingAttributesUrl = "";

    /**
     * Stub: Edit URL for Other Design Attributes
     */
    editTreatmentBMPOtherDesignAttributesUrl = "";
    // Carousel state
    currentImageIndex = 0;

    setImageIndex(idx: number, images: any[]): void {
        if (images && images.length > 0) {
            this.currentImageIndex = (idx + images.length) % images.length;
        }
    }
    // Neptune map integration
    boundingBox: any;
    map: any;
    layerControl: any;
    mapIsReady = false;
    @ViewChild("templateRight", { static: true }) templateRight!: TemplateRef<any>;
    @ViewChild("templateAbove", { static: true }) templateAbove!: TemplateRef<any>;
    @Input() treatmentBMPID!: number;

    // Observables for async pipe
    treatmentBMP$!: Observable<TreatmentBMPDto>;
    customAttributes$: Observable<CustomAttributeDto[]>;
    treatmentBMPTypeCustomAttributeTypes$: Observable<TreatmentBMPTypeCustomAttributeTypeDto[]>;
    attachments$!: Observable<any[]>; // TODO: Replace 'any' with ProjectDocumentDto
    treatmentBMPImages$!: Observable<any[]>;
    imagesLoading = true;

    // Placeholder properties for template bindings
    isAnonymousOrUnassigned = false;
    delineationErrors: string[] = [];
    parameterizationErrors: string[] = [];
    openRevisionRequest: any = null;
    openRevisionRequestDetailUrl = "";
    upstreamestBMP: any = null;
    isUpstreamestBMPAnalyzedInModelingModule = false;
    currentPersonCanManage = false;
    canEditStormwaterJurisdiction = false;
    isAnalyzedInModelingModule = true;
    isSitkaAdmin = false;
    otherTreatmentBmpsExistInSubbasin = false;
    upstreamestBMPDetailUrl = "";
    upstreamBMPDetailUrl = "";
    delineationMapUrl = "";
    editUpstreamBMPUrl = "";
    hasModelingAttributes = false;
    // TODO: Add more properties as needed

    public TreatmentBMPLifespanTypeEnum = TreatmentBMPLifespanTypeEnum;
    /**
     * Stub for HRU characteristics summaries used in the land use panel template.
     * Replace with actual data wiring when available.
     */
    hruCharacteristicsSummaries: any[] = [];
    public WaterQualityManagementPlanModelingApproachEnum = WaterQualityManagementPlanModelingApproachEnum;
    public CustomAttributeTypePurposeEnum = CustomAttributeTypePurposeEnum;

    constructor(
        private router: Router,
        private treatmentBMPService: TreatmentBMPService,
        private treatmentBMPImageByTreatmentBMPService: TreatmentBMPImageByTreatmentBMPService,
        private treatmentBMPTypeService: TreatmentBMPTypeService
    ) {}

    ngOnInit(): void {
        // treatmentBMPID will be set via input binding from the route param (withComponentInputBinding)
        // Example: Fetch detail data using the treatmentBMPID
        this.treatmentBMP$ = this.treatmentBMPService.treatmentBmpsTreatmentBMPIDGet(this.treatmentBMPID).pipe(
            tap((bmp) => {
                if (bmp && bmp.Latitude && bmp.Longitude) {
                    this.boundingBox = new BoundingBoxDto({
                        Left: bmp.Longitude - 0.001,
                        Right: bmp.Longitude + 0.001,
                        Bottom: bmp.Latitude - 0.001,
                        Top: bmp.Latitude + 0.001,
                    });
                }
            })
        );

        this.customAttributes$ = this.treatmentBMP$.pipe(switchMap((bmp) => this.treatmentBMPService.treatmentBmpsTreatmentBMPIDCustomAttributesGet(bmp.TreatmentBMPID)));

        // Wire up async calls for custom attribute types and attributes
        this.treatmentBMPTypeCustomAttributeTypes$ = this.treatmentBMP$.pipe(
            switchMap((bmp) =>
                this.treatmentBMPTypeService.treatmentBmpTypesTreatmentBMPTypeIDCustomAttributeTypesGet(bmp.TreatmentBMPTypeID).pipe(
                    tap((attributes) => {
                        console.log(attributes);
                        this.hasModelingAttributes = attributes.some((attr) => attr.CustomAttributeType.CustomAttributeTypePurposeID === CustomAttributeTypePurposeEnum.Modeling);
                    })
                )
            )
        );
        // Fetch images for this BMP
        this.treatmentBMPImages$ = this.treatmentBMPImageByTreatmentBMPService.treatmentBmpsTreatmentBMPIDTreatmentBmpImagesGet(this.treatmentBMPID).pipe(
            tap({
                next: () => {
                    this.imagesLoading = false;
                },
                error: () => {
                    this.imagesLoading = false;
                },
                complete: () => {
                    this.imagesLoading = false;
                },
            })
        );
        // this.attachments$ = this.treatmentBMP$.pipe(
        //     switchMap(bmp => this.treatmentBMPService.treatmentBmpsTreatmentBMPIDAttachmentsGet(bmp.TreatmentBMPID))
        // );
        // Fetch bounding box for map display (replace with actual service if needed)
    }

    /**
     * Stub: Row data for Field Visits grid
     */
    fieldVisitRowData: any[] = [];

    /**
     * Stub: Column definitions for Field Visits grid
     */
    fieldVisitColumnDefs: any[] = [
        // Action columns (delete, view/continue)
        { headerName: "", field: "actions", cellRenderer: "actionCellRenderer", width: 60, suppressMenu: true, sortable: false, filter: false },
        // BMP Name (if not detail page)
        { headerName: "BMP Name", field: "TreatmentBMPName", width: 120 },
        {
            headerName: "Visit Date",
            field: "VisitDate",
            width: 130,
            filter: "agDateColumnFilter",
            valueFormatter: (params) => (params.value ? new Date(params.value).toLocaleDateString() : ""),
        },
        { headerName: "Jurisdiction", field: "OrganizationName", width: 140 },
        { headerName: "Water Quality Management Plan", field: "WaterQualityManagementPlanName", width: 105 },
        { headerName: "Performed By", field: "PerformedByPersonName", width: 105 },
        { headerName: "Field Visit Verified", field: "IsFieldVisitVerified", width: 105, valueFormatter: (params) => (params.value ? "Yes" : "No") },
        { headerName: "Field Visit Status", field: "FieldVisitStatusDisplayName", width: 85 },
        { headerName: "Field Visit Type", field: "FieldVisitTypeDisplayName", width: 125 },
        { headerName: "Inventory Updated?", field: "InventoryUpdated", width: 100, valueFormatter: (params) => (params.value ? "Yes" : "No") },
        { headerName: "Required Attributes Entered?", field: "RequiredAttributesEntered", width: 100 },
        { headerName: "Initial Assessment?", field: "InitialAssessmentStatus", width: 95 },
        { headerName: "Initial Assessment Score", field: "AssessmentScoreInitial", width: 95 },
        { headerName: "Maintenance Occurred?", field: "MaintenanceOccurred", width: 95 },
        { headerName: "Post-Maintenance Assessment?", field: "PostMaintenanceAssessmentStatus", width: 120 },
        { headerName: "Post-Maintenance Assessment Score", field: "AssessmentScorePM", width: 95 },
    ];

    /**
     * Stub: Whether the current user can edit benchmark and thresholds
     */
    canEditBenchmarkAndThresholds = false;

    /**
     * Stub: URL for adding benchmark and threshold values
     */
    addBenchmarkAndThresholdUrl = "";

    /**
     * Stub: Whether this BMP type has settable benchmark and threshold values
     */
    hasSettableBenchmarkAndThresholdValues = false;

    // NOTE: TreatmentBMPTypeIsAnalyzedInModelingModule is expected on treatmentBMP DTO. If missing, add to DTO or adjust template logic.

    // Handler for Neptune map load event
    handleMapReady(event: any): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        // Add marker for BMP location
        this.treatmentBMP$.subscribe((bmp) => {
            if (bmp && bmp.Latitude && bmp.Longitude && this.map) {
                const marker = L.marker([bmp.Latitude, bmp.Longitude], {
                    icon: MarkerHelper.treatmentBMPMarker,
                });
                marker.addTo(this.map);
                marker.bindPopup(bmp.TreatmentBMPName || "BMP Location");
            }
        });
    }

    getEditLink(treatmentBMP: any): string {
        // TODO: Return the correct edit route for this BMP
        return `/inventory/treatment-bmps/edit/${treatmentBMP.TreatmentBMPID}`;
    }

    removeUpstreamBMP(): void {
        // Implement the logic to remove the upstream BMP association
        // This might involve calling a service method to update the backend
        // and then updating the local state accordingly
    }
}
