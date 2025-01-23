import { Component } from "@angular/core";
import { Observable } from "rxjs";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentSimpleDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-simple-dto";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { ColDef } from "ag-grid-community";
import { OnlandVisualTrashAssessmentAreaSimpleDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-simple-dto";
import { AsyncPipe, NgIf } from "@angular/common";

@Component({
    selector: "trash-ovta-index",
    standalone: true,
    imports: [NeptuneGridComponent, PageHeaderComponent, AlertDisplayComponent, AsyncPipe, NgIf],
    templateUrl: "./trash-ovta-index.component.html",
    styleUrl: "./trash-ovta-index.component.scss",
})
export class TrashOvtaIndexComponent {
    public onlandVisualTrashAssessments$: Observable<OnlandVisualTrashAssessmentSimpleDto[]>;
    public onlandVisualTrashAssessmentAreas$: Observable<OnlandVisualTrashAssessmentAreaSimpleDto[]>;
    public columnDefs: ColDef[];

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService,
        private utilityFunctionsService: UtilityFunctionsService
    ) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment Area Name", "ProjectName", "OnlandVisualTrashAssessmentAreaID", {
                InRouterLink: "/projects/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Score", "OnlandVisualTrashAssessmentScoreID"),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Type", "IsProgressAssessment"),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "CompletedDate", "short"),
            this.utilityFunctionsService.createBasicColumnDef("Status", "OnlandVisualTrashAssessmentStatusID"),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "OnlandVisualTrashAssessmentID"),
            this.utilityFunctionsService.createBasicColumnDef("Created By", "CreatedByPersonID"),
            this.utilityFunctionsService.createDateColumnDef("Created On", "CreatedDate", "short"),
        ];
        this.onlandVisualTrashAssessments$ = this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsGet();
        this.onlandVisualTrashAssessmentAreas$ = this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasGet();
    }
}
