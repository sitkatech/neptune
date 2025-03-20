import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";
import { Observable, tap } from "rxjs";
import { LandUseBlockService } from "../../../shared/generated/api/land-use-block.service";
import { UtilityFunctionsService } from "../../../services/utility-functions.service";
import { ColDef } from "ag-grid-community";
import { NeptuneGridComponent } from "../../../shared/components/neptune-grid/neptune-grid.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { LandUseBlockGridDto } from "../../../shared/generated/model/land-use-block-grid-dto";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { IconComponent } from "../../../shared/components/icon/icon.component";
import { RouterLink } from "@angular/router";
import { environment } from "src/environments/environment";

@Component({
    selector: "trash-land-use-block-index",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NeptuneGridComponent, AsyncPipe, NgIf, LoadingDirective, IconComponent, RouterLink],
    templateUrl: "./trash-land-use-block-index.component.html",
    styleUrl: "./trash-land-use-block-index.component.scss",
})
export class TrashLandUseBlockIndexComponent {
    public landUseBlocks$: Observable<LandUseBlockGridDto[]>;
    public landUseBlockColumnDefs: ColDef[];
    public richTextID = NeptunePageTypeEnum.LandUseBlock;
    public isLoading: boolean = true;
    public url = environment.ocStormwaterToolsBaseUrl;

    constructor(private landUseBlockService: LandUseBlockService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit() {
        this.landUseBlockColumnDefs = [
            this.utilityFunctionsService.createBasicColumnDef("Block ID", "LandUseBlockID"),
            this.utilityFunctionsService.createBasicColumnDef("Land Use Type", "PriorityLandUseTypeName", {
                CustomDropdownFilterField: "PriorityLandUseTypeName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
            }),
            this.utilityFunctionsService.createDecimalColumnDef("Block Area", "Area"),
            this.utilityFunctionsService.createDecimalColumnDef("Trash Generation Rate", "TrashGenerationRate", {
                CustomDropdownFilterField: "TrashGenerationRate",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Land Use Description", "LandUseDescription", {
                CustomDropdownFilterField: "LandUseDescription",
            }),
            this.utilityFunctionsService.createDecimalColumnDef("Median Household Income Residential", "MedianHouseholdIncomeResidential"),
            this.utilityFunctionsService.createDecimalColumnDef("Median Household Income Retail", "MedianHouseholdIncomeRetail"),
            this.utilityFunctionsService.createDecimalColumnDef("Trash Results Area", "MedianHouseholdIncomeRetail"),
            this.utilityFunctionsService.createBasicColumnDef("Permit Type", "PermitTypeName"),
            this.utilityFunctionsService.createBasicColumnDef("Land Use for TGR", "LandUseForTGR"),
        ];
        this.landUseBlocks$ = this.landUseBlockService.landUseBlocksGet().pipe(tap((x) => (this.isLoading = false)));
    }
}
