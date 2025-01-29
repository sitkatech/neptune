import { Component } from "@angular/core";
import { NeptuneGridComponent } from "../../../../shared/components/neptune-grid/neptune-grid.component";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { ColDef } from "ag-grid-community";
import { Observable, tap } from "rxjs";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentAreaGridDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-grid-dto";
import { AsyncPipe, NgIf } from "@angular/common";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";

@Component({
    selector: "trash-ovta-area-index",
    standalone: true,
    imports: [NeptuneGridComponent, PageHeaderComponent, AlertDisplayComponent, NgIf, AsyncPipe, LoadingDirective],
    templateUrl: "./trash-ovta-area-index.component.html",
    styleUrl: "./trash-ovta-area-index.component.scss",
})
export class TrashOvtaAreaIndexComponent {
    public onlandVisualTrashAssessmentAreas$: Observable<OnlandVisualTrashAssessmentAreaGridDto[]>;
    public ovtaAreaColumnDefs: ColDef[];
    public isLoading: boolean = true;

    constructor(private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit(): void {
        this.ovtaAreaColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment Area Name", "OnlandVisualTrashAssessmentAreaName", "OnlandVisualTrashAssessmentAreaID", {
                InRouterLink: "../onland-visual-trash-assessment-area/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Baseline Score", "OnlandVisualTrashAssessmentBaselineScoreName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentBaselineScoreName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Progress Score", "OnlandVisualTrashAssessmentProgressScoreName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentProgressScoreName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Number of Assessments Completed", "NumberOfAssessmentsCompleted"),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "LastAssessmentDate", "short"),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Description", "AssessmentAreaDescription"),
        ];
        this.onlandVisualTrashAssessmentAreas$ = this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasGet().pipe(tap((x) => (this.isLoading = false)));
    }
}
