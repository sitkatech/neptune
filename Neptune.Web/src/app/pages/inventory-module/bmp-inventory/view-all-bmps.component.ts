import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";
import { HybridMapGridComponent } from "../../../shared/components/hybrid-map-grid/hybrid-map-grid.component";
import { ColDef } from "ag-grid-community";
import { Observable, tap } from "rxjs";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPDisplayDto } from "src/app/shared/generated/model/treatment-bmp-display-dto";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { AsyncPipe } from "@angular/common";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";

@Component({
    selector: "view-all-bmps",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, HybridMapGridComponent, AsyncPipe, LoadingDirective],
    templateUrl: "./view-all-bmps.component.html",
})
export class ViewAllBmpsComponent {
    public treatmentBmps$: Observable<TreatmentBMPDisplayDto[]>;
    public columnDefs: ColDef[];
    public isLoading = true;
    public selectedTreatmentBMPID: number;
    public selectionFromMap: boolean;
    public boundingBox$: Observable<BoundingBoxDto>;

    constructor(
        private treatmentBMPService: TreatmentBMPService,
        private utilityFunctionsService: UtilityFunctionsService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService
    ) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctionsService.createActionsColumnDef((params: any) => [
                {
                    ActionName: "View",
                    ActionHandler: () => this.handleView(params.data.TreatmentBMPID),
                },
            ]),
            this.utilityFunctionsService.createLinkColumnDef("Name", "TreatmentBMPName", "TreatmentBMPID", {
                InRouterLink: "../bmp-inventory/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Type", "TreatmentBMPTypeName"),
            this.utilityFunctionsService.createBasicColumnDef("Watershed", "WatershedName"),
            this.utilityFunctionsService.createBasicColumnDef("Notes", "Notes"),
            this.utilityFunctionsService.createBasicColumnDef("Verified?", "InventoryIsVerified"),
            this.utilityFunctionsService.createBasicColumnDef("Fully Parameterized?", "IsFullyParameterized"),
        ];
        this.treatmentBmps$ = this.treatmentBMPService.treatmentBmpsGet().pipe(tap(() => (this.isLoading = false)));
        this.boundingBox$ = this.stormwaterJurisdictionService.jurisdictionsBoundingBoxGet();
    }

    handleView(treatmentBMPID: number) {
        // TODO: Implement navigation to detail page
        // Example: this.router.navigate(["/inventory/bmp-inventory", treatmentBMPID]);
    }

    handleMapReady(event: any) {
        // Optionally handle map ready event
    }

    onSelectedTreatmentBMPChangedFromGrid(selectedTreatmentBMPID: number) {
        if (this.selectedTreatmentBMPID === selectedTreatmentBMPID) return;
        this.selectedTreatmentBMPID = selectedTreatmentBMPID;
        this.selectionFromMap = false;
        return this.selectedTreatmentBMPID;
    }
}
