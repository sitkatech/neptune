import { DialogService } from "@ngneat/dialog";
import { FundingEventModalComponent, FundingEventModalContext } from "../funding-event-modal/funding-event-modal.component";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { Component, OnInit, OnChanges, SimpleChanges, ViewChild, TemplateRef, Input } from "@angular/core";
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
import { ModeledBmpPerformanceComponent } from "src/app/shared/components/modeled-bmp-performance/modeled-bmp-performance.component";
import { WaterQualityManagementPlanModelingApproachEnum } from "src/app/shared/generated/enum/water-quality-management-plan-modeling-approach-enum";
import { HRUCharacteristicDto } from "src/app/shared/generated/model/hru-characteristic-dto";
import { ColDef } from "ag-grid-community";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { LandUseTableComponent } from "src/app/shared/components/land-use-table/land-use-table.component";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { CustomAttributesDisplayComponent } from "src/app/shared/components/custom-attributes-display/custom-attributes-display.component";
import { CustomAttributeDto, TreatmentBMPHRUCharacteristicsSummarySimpleDto, TreatmentBMPTypeCustomAttributeTypeDto } from "src/app/shared/generated/model/models";
import { FieldVisitDto } from "src/app/shared/generated/model/field-visit-dto";
import { FundingEventByTreatmentBMPIDService } from "src/app/shared/generated/api/funding-event-by-treatment-bmpid.service";
import { FundingEventDto } from "src/app/shared/generated/model/funding-event-dto";
import { CustomAttributeTypePurposeEnum } from "src/app/shared/generated/enum/custom-attribute-type-purpose-enum";
import { TreatmentBMPTypeService } from "src/app/shared/generated/api/treatment-bmp-type.service";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { SumPipe } from "src/app/shared/pipes/sum.pipe";

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
        ModeledBmpPerformanceComponent,
        LandUseTableComponent,
        NeptuneGridComponent,
        CustomAttributesDisplayComponent,
    ],
})
export class TreatmentBmpDetailComponent implements OnInit, OnChanges {
    openAddFundingEventModal(): void {
        this.dialogService
            .open(FundingEventModalComponent, {
                data: { treatmentBMPID: this.treatmentBMPID } as FundingEventModalContext,
            })
            .afterClosed$.subscribe((result) => {
                if (result) this.loadData();
            });
    }

    openEditFundingEventModal(fundingEvent: FundingEventDto): void {
        this.dialogService
            .open(FundingEventModalComponent, {
                data: { treatmentBMPID: this.treatmentBMPID, editData: fundingEvent } as FundingEventModalContext,
            })
            .afterClosed$.subscribe((result) => {
                if (result) this.loadData();
            });
    }

    confirmDeleteFundingEvent(fundingEvent: FundingEventDto): void {
        this.confirmService
            .confirm({
                buttonClassYes: "btn-danger",
                buttonTextYes: "Delete",
                buttonTextNo: "Cancel",
                title: "Delete Funding Event",
                message: `<p>Are you sure you want to delete the Funding Event '<strong>${fundingEvent.DisplayName}</strong>'?</p>`,
            })
            .then((confirmed) => {
                if (confirmed) {
                    this.fundingEventByTreatmentBMPIDService.deleteFundingEventByTreatmentBMPID(this.treatmentBMPID, fundingEvent.FundingEventID).subscribe(() => this.loadData());
                }
            });
    }
    fundingEvents$: Observable<FundingEventDto[]>;
    ngOnChanges(changes: SimpleChanges): void {
        if (changes["treatmentBMPID"] && !changes["treatmentBMPID"].firstChange) {
            this.loadData();
        }
    }

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
    hruCharacteristics$: Observable<HRUCharacteristicDto[]>;
    hruCharacteristicsColumnDefs: Array<ColDef>;
    fieldVisits$: Observable<FieldVisitDto[]>;
    fieldVisitColumnDefs: Array<ColDef>;

    imagesLoading = true;

    // Placeholder properties for template bindings
    isAnonymousOrUnassigned = false;
    delineationErrors: string[] = [];
    parameterizationErrors: string[] = [];
    openRevisionRequest: any = null;
    openRevisionRequestDetailUrl = "";
    upstreamestBMP: any = null;
    isUpstreamestBMPAnalyzedInModelingModule = false;
    currentPersonCanManage = true;
    canEditStormwaterJurisdiction = false;
    isAnalyzedInModelingModule = true;
    isSitkaAdmin = true;
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
    hruCharacteristicsSummaries: TreatmentBMPHRUCharacteristicsSummarySimpleDto[] = [];
    public WaterQualityManagementPlanModelingApproachEnum = WaterQualityManagementPlanModelingApproachEnum;
    public CustomAttributeTypePurposeEnum = CustomAttributeTypePurposeEnum;

    constructor(
        private router: Router,
        private treatmentBMPService: TreatmentBMPService,
        private treatmentBMPImageByTreatmentBMPService: TreatmentBMPImageByTreatmentBMPService,
        private treatmentBMPTypeService: TreatmentBMPTypeService,
        private utilityFunctionsService: UtilityFunctionsService,
        private groupByPipe: GroupByPipe,
        private sumPipe: SumPipe,
        private fundingEventByTreatmentBMPIDService: FundingEventByTreatmentBMPIDService,
        private dialogService: DialogService,
        private confirmService: ConfirmService
    ) {}

    ngOnInit(): void {
        // Wire up columns using utilityFunctionsService
        this.hruCharacteristicsColumnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Type of HRU Entity", "HRUEntity"),
            this.utilityFunctionsService.createBasicColumnDef("LGU ID", "LoadGeneratingUnitID"),
            this.utilityFunctionsService.createBasicColumnDef("Model Basin Land Use Description", "HRUCharacteristicLandUseCodeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Baseline Model Basin Land Use Description", "BaselineHRUCharacteristicLandUseCodeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Hydrologic Soil Group", "HydrologicSoilGroup"),
            this.utilityFunctionsService.createDecimalColumnDef("Slope %", "SlopePercentage"),
            this.utilityFunctionsService.createDecimalColumnDef("Impervious Acres", "ImperviousAcres"),
            this.utilityFunctionsService.createDecimalColumnDef("Baseline Impervious Acres", "BaselineImperviousAcres"),
            this.utilityFunctionsService.createDecimalColumnDef("Total Acres", "Area"),
            this.utilityFunctionsService.createLinkColumnDef("Treatment BMP", "TreatmentBMPName", "TreatmentBMPID", {
                InRouterLink: "../",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Water Quality Management Plan", "WaterQualityManagementPlanName"),
            this.utilityFunctionsService.createBasicColumnDef("Regional Subbasin", "RegionalSubbasinName"),
            this.utilityFunctionsService.createDateColumnDef("Last Updated", "LastUpdated", "short"),
        ];

        this.fieldVisitColumnDefs = [
            // todo: implement action cell renderer, make initial assessment, maintenance occurred, and post-maintenance assessment into link columns
            // Action columns (delete, view/continue) - implement cellRenderer as needed
            { headerName: "", field: "actions", cellRenderer: "actionCellRenderer", width: 60, suppressMenu: true, sortable: false, filter: false },
            this.utilityFunctionsService.createDateColumnDef("Visit Date", "VisitDate", "MM/dd/yyyy"),
            this.utilityFunctionsService.createBasicColumnDef("Performed By", "PerformedByPersonName"),
            this.utilityFunctionsService.createBooleanColumnDef("Field Visit Verified", "IsFieldVisitVerified"),
            this.utilityFunctionsService.createBasicColumnDef("Field Visit Status", "FieldVisitStatusDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Field Visit Type", "FieldVisitTypeDisplayName"),
            this.utilityFunctionsService.createBooleanColumnDef("Inventory Updated?", "InventoryUpdated"),
            this.utilityFunctionsService.createBooleanColumnDef("Required Attributes Entered?", "RequiredAttributesEntered"),
            this.utilityFunctionsService.createBasicColumnDef("Initial Assessment?", "InitialAssessmentStatus"),
            this.utilityFunctionsService.createDecimalColumnDef("Initial Assessment Score", "AssessmentScoreInitial"),
            this.utilityFunctionsService.createBasicColumnDef("Maintenance Occurred?", "MaintenanceOccurred"),
            this.utilityFunctionsService.createBasicColumnDef("Post-Maintenance Assessment?", "PostMaintenanceAssessmentStatus"),
            this.utilityFunctionsService.createDecimalColumnDef("Post-Maintenance Assessment Score", "AssessmentScorePM"),
        ];
        this.loadData();
    }

    private loadData(): void {
        // Wire up funding events grid as observable
        this.fundingEvents$ = this.fundingEventByTreatmentBMPIDService.listFundingEventByTreatmentBMPID(this.treatmentBMPID);
        this.hruCharacteristics$ = this.treatmentBMPService.listHRUCharacteristicsTreatmentBMP(this.treatmentBMPID).pipe(
            tap((hruCharacteristics) => {
                const grouped = this.groupByPipe.transform(hruCharacteristics, "HRUCharacteristicLandUseCodeDisplayName");
                this.hruCharacteristicsSummaries = Object.entries(grouped).map(
                    ([key, value]) =>
                        new TreatmentBMPHRUCharacteristicsSummarySimpleDto({
                            Area: this.sumPipe.transform(value, "Area"),
                            ImperviousCover: this.sumPipe.transform(value, "ImperviousAcres"),
                            LandUse: key,
                        })
                );
            })
        );
        // Wire up field visits grid as observable
        this.fieldVisits$ = this.treatmentBMPService.fieldVisitGridJsonDataTreatmentBMP(this.treatmentBMPID);
        this.treatmentBMP$ = this.treatmentBMPService.getByIDTreatmentBMP(this.treatmentBMPID).pipe(
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
        this.customAttributes$ = this.treatmentBMP$.pipe(switchMap((bmp) => this.treatmentBMPService.listCustomAttributesTreatmentBMP(bmp.TreatmentBMPID)));
        this.treatmentBMPTypeCustomAttributeTypes$ = this.treatmentBMP$.pipe(
            switchMap((bmp) =>
                this.treatmentBMPTypeService.listCustomAttributeTypesTreatmentBMPType(bmp.TreatmentBMPTypeID).pipe(
                    tap((attributes) => {
                        this.hasModelingAttributes =
                            Array.isArray(attributes) &&
                            attributes.some((attr) => attr.CustomAttributeType?.CustomAttributeTypePurposeID === CustomAttributeTypePurposeEnum.Modeling);
                    })
                )
            )
        );
        this.treatmentBMPImages$ = this.treatmentBMPImageByTreatmentBMPService.listTreatmentBMPImageByTreatmentBMP(this.treatmentBMPID).pipe(
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
    }

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

    getFundingEventTotal(fundingEvent: FundingEventDto): number {
        if (!fundingEvent.FundingEventFundingSources) return 0;
        return fundingEvent.FundingEventFundingSources.reduce((acc, s) => acc + (s.Amount || 0), 0);
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
