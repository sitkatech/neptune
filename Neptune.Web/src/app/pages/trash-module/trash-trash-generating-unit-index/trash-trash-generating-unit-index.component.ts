import { AsyncPipe, NgIf } from "@angular/common";
import { Component } from "@angular/core";
import { ColDef } from "ag-grid-community";
import { Observable } from "rxjs";
import { UtilityFunctionsService } from "../../../services/utility-functions.service";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";
import { NeptuneGridComponent } from "../../../shared/components/neptune-grid/neptune-grid.component";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";
import { TrashGeneratingUnitService } from "../../../shared/generated/api/trash-generating-unit.service";
import { TrashGeneratingUnitGridDto } from "../../../shared/generated/model/trash-generating-unit-grid-dto";

@Component({
    selector: "trash-trash-generating-unit-index",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NeptuneGridComponent, AsyncPipe, NgIf],
    templateUrl: "./trash-trash-generating-unit-index.component.html",
    styleUrl: "./trash-trash-generating-unit-index.component.scss",
})
export class TrashTrashGeneratingUnitIndexComponent {
    public trashGeneratingUnit$: Observable<TrashGeneratingUnitGridDto[]>;
    public trashGeneratingUnitsColumnDefs: ColDef[];

    constructor(private trashGeneratingUnitService: TrashGeneratingUnitService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit() {
        this.trashGeneratingUnitsColumnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Trash Generating Unit ID", "TrashGeneratingUnitID"),
            this.utilityFunctionsService.createBasicColumnDef("Land Use Type", "PriorityLandUseTypeDisplayName", { FieldDefinitionType: "LandUseType" }),
            this.utilityFunctionsService.createLinkColumnDef("Governing OVTA Area", "OnlandVisualTrashAssessmentAreaName", "OnlandVisualTrashAssessmentAreaID", {
                InRouterLink: "../onland-visual-trash-assessment-area",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Governing OVTA Area Baseline Score", "OnlandVisualTrashAssessmentAreaBaselineScore"),
            this.utilityFunctionsService.createBasicColumnDef("Governing Treatment BMP", ""),
            this.utilityFunctionsService.createBasicColumnDef("Governing WQMP", "WaterQualityManagementPlanName"),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
                FieldDefinitionType: "Jurisdiction",
            }),
            this.utilityFunctionsService.createDecimalColumnDef("Area", "Area"),
            this.utilityFunctionsService.createDecimalColumnDef("Baseline Loading Rate", "BaselineLoadingRate"),
            this.utilityFunctionsService.createDecimalColumnDef("Current Loading Rate", "CurrentLoadingRate"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Status via BMP", "TrashCaptureStatusBMP"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Status via WQMP", "TrashCaptureStatusWQMP"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Effectiveness via BMP", "TrashCaptureStatusBMP"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Effectiveness via WQMP", "TrashCaptureStatusWQMP"),
            this.utilityFunctionsService.createDecimalColumnDef("Median Household Income (Residential)", "MedianHouseholdIncomeResidential"),
            this.utilityFunctionsService.createDecimalColumnDef("Median Household Income (Retail)", "MedianHouseholdIncomeRetail"),
            this.utilityFunctionsService.createBasicColumnDef("Permit Class", "PermitTypeName"),
            this.utilityFunctionsService.createBasicColumnDef("Land Use for TGR", "LandUseForTGR"),
            this.utilityFunctionsService.createBasicColumnDef("Land Use Default TGR", "TrashGenerationRate"),
            this.utilityFunctionsService.createDateColumnDef("Last Updated Date", "LastUpdateDate", "MM/dd/yyyy"),
        ];
        this.trashGeneratingUnit$ = this.trashGeneratingUnitService.trashGeneratingUnitsGet();
    }
}
