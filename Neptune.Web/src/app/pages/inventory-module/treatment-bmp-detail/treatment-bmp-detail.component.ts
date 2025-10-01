import { Component, OnInit, ViewChild, TemplateRef, Input } from "@angular/core";
import { Router, RouterLink } from "@angular/router";
import { DatePipe, AsyncPipe, CommonModule } from "@angular/common";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
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
import { environment } from "src/environments/environment";
import { ButtonLoadingDirective } from "src/app/shared/directives/button-loading.directive";
import { ModeledBmpPerformanceComponent } from "src/app/shared/components/modeled-bmp-performance/modeled-bmp-performance.component";
import { WaterQualityManagementPlanModelingApproachEnum } from "src/app/shared/generated/enum/water-quality-management-plan-modeling-approach-enum";
import { LandUseTableComponent } from "src/app/shared/components/land-use-table/land-use-table.component";

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
    ],
})
export class TreatmentBmpDetailComponent implements OnInit {
    customModelingAttributes: any[] = [];
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
    // TODO: Add more properties as needed

    public TreatmentBMPLifespanTypeEnum = TreatmentBMPLifespanTypeEnum;
    /**
     * Stub for HRU characteristics summaries used in the land use panel template.
     * Replace with actual data wiring when available.
     */
    hruCharacteristicsSummaries: any[] = [];
    public WaterQualityManagementPlanModelingApproachEnum = WaterQualityManagementPlanModelingApproachEnum;

    constructor(private router: Router, private treatmentBMPService: TreatmentBMPService, private treatmentBMPImageByTreatmentBMPService: TreatmentBMPImageByTreatmentBMPService) {}

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
                // Extract modeling attributes from CustomAttributes
                // TODO: Filter custom attributes by modeling purpose when mapping is available
                if (bmp && bmp.CustomAttributes) {
                    this.customModelingAttributes = bmp.CustomAttributes.map((attr) => ({
                        CustomAttributeTypeName: attr.CustomAttributeTypeID,
                        CustomAttributeTypeDescription: attr.CustomAttributeTypeID,
                        CustomAttributeValueWithUnits: attr.CustomAttributeValues?.join(", "),
                    }));
                } else {
                    this.customModelingAttributes = [];
                }
            })
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

    public getFileResourceUrl(fileResourceGUID: string) {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
    }

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
