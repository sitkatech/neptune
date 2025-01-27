import { Component } from "@angular/core";
import { Observable } from "rxjs";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { ColDef } from "ag-grid-community";
import { AsyncPipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentGridDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-grid-dto";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { OnlandVisualTrashAssessmentAreaGridDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-grid-dto";

@Component({
    selector: "trash-ovta-index",
    standalone: true,
    imports: [NeptuneGridComponent, PageHeaderComponent, AlertDisplayComponent, AsyncPipe, NgIf],
    templateUrl: "./trash-ovta-index.component.html",
    styleUrl: "./trash-ovta-index.component.scss",
})
export class TrashOvtaIndexComponent {
    public onlandVisualTrashAssessments$: Observable<OnlandVisualTrashAssessmentGridDto[]>;
    public onlandVisualTrashAssessmentAreas$: Observable<OnlandVisualTrashAssessmentAreaGridDto[]>;
    public ovtaColumnDefs: ColDef[];
    public ovtaAreaColumnDefs: ColDef[];
    public customRichTextID = NeptunePageTypeEnum.OVTAIndex;

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService,
        private utilityFunctionsService: UtilityFunctionsService
    ) {}

    ngOnInit(): void {
        this.ovtaColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment ID", "OnlandVisualTrashAssessmentID", "OnlandVisualTrashAssessmentID"),
            this.utilityFunctionsService.createLinkColumnDef("Assessment Area Name", "OnlandVisualTrashAssessmentAreaName", "OnlandVisualTrashAssessmentAreaID", {
                InRouterLink: "../onland-visual-trash-assessment-area/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Score", "OnlandVisualTrashAssessmentScoreName", { FieldDefinitionType: "AssessmentScore" }),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Type", "IsProgressAssessment", { CustomDropdownFilterField: "IsProgressAssessment" }),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "CompletedDate", "short"),
            this.utilityFunctionsService.createBasicColumnDef("Status", "OnlandVisualTrashAssessmentStatusName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentStatusName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
                FieldDefinitionType: "Jurisdiction",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Created By", "CreatedByPersonFullName"),
            this.utilityFunctionsService.createDateColumnDef("Created On", "CreatedDate", "short"),
        ];
        this.ovtaAreaColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment Area Name", "OnlandVisualTrashAssessmentAreaName", "OnlandVisualTrashAssessmentAreaID", {
                InRouterLink: "../onland-visual-trash-assessment-area/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Baseline Score", "OnlandVisualTrashAssessmentBaselineScoreName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentBaselineScoreName",
                FieldDefinitionType: "BaselineScore",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Progress Score", "OnlandVisualTrashAssessmentProgressScoreName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentProgressScoreName",
                FieldDefinitionType: "ProgressScore",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Number of Assessments Completed", "NumberOfAssessmentsCompleted"),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "LastAssessmentDate", "short"),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
                FieldDefinitionType: "Jurisdiction",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Description", "AssessmentAreaDescription"),
        ];
        this.onlandVisualTrashAssessments$ = this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsGet();
        this.onlandVisualTrashAssessmentAreas$ = this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasGet();
    }
}
