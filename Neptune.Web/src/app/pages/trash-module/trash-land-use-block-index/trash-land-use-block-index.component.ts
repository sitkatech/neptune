import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";
import { Observable } from "rxjs";
import { LandUseBlockService } from "../../../shared/generated/api/land-use-block.service";
import { UtilityFunctionsService } from "../../../services/utility-functions.service";
import { ColDef } from "ag-grid-community";
import { NeptuneGridComponent } from "../../../shared/components/neptune-grid/neptune-grid.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { LandUseBlockGridDto } from "../../../shared/generated/model/land-use-block-grid-dto";

@Component({
    selector: "trash-land-use-block-index",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NeptuneGridComponent, AsyncPipe, NgIf],
    templateUrl: "./trash-land-use-block-index.component.html",
    styleUrl: "./trash-land-use-block-index.component.scss",
})
export class TrashLandUseBlockIndexComponent {
    public landUseBlocks$: Observable<LandUseBlockGridDto[]>;
    public landUseBlockColumnDefs: ColDef[];

    constructor(private landUseBlockService: LandUseBlockService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit() {
        this.landUseBlockColumnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Block ID", "LandUseBlockID"),
            this.utilityFunctionsService.createBasicColumnDef("Land Use Type", "PriorityLandUseTypeName", { FieldDefinitionType: "LandUseType" }),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
                FieldDefinitionType: "Jurisdiction",
            }),
            this.utilityFunctionsService.createDecimalColumnDef("Block Area", "Area"),
            this.utilityFunctionsService.createDecimalColumnDef("Trash Generation Rate", "TrashGenerationRate", { FieldDefinitionType: "TrashGenerationRate" }),
            this.utilityFunctionsService.createDecimalColumnDef("Median Household Income Residential", "MedianHouseholdIncomeResidential"),
            this.utilityFunctionsService.createDecimalColumnDef("Median Household Income Retail", "MedianHouseholdIncomeRetail"),
            this.utilityFunctionsService.createDecimalColumnDef("Trash Results Area", "MedianHouseholdIncomeRetail"),
            this.utilityFunctionsService.createBasicColumnDef("Permit Type", "PermitTypeName"),
            this.utilityFunctionsService.createBasicColumnDef("Land Use for TGR", "LandUseForTGR"),
        ];
        this.landUseBlocks$ = this.landUseBlockService.landUseBlocksGet();
    }
}
