import { Component } from "@angular/core";
import { Observable, tap } from "rxjs";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { ColDef } from "ag-grid-community";
import { AsyncPipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentGridDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-grid-dto";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { environment } from "src/environments/environment";
import { IconComponent } from "../../../../shared/components/icon/icon.component";
import { RouterLink } from "@angular/router";

@Component({
    selector: "trash-ovta-index",
    standalone: true,
    imports: [NeptuneGridComponent, PageHeaderComponent, AlertDisplayComponent, AsyncPipe, NgIf, LoadingDirective, IconComponent, RouterLink],
    templateUrl: "./trash-ovta-index.component.html",
    styleUrl: "./trash-ovta-index.component.scss",
})
export class TrashOvtaIndexComponent {
    public onlandVisualTrashAssessments$: Observable<OnlandVisualTrashAssessmentGridDto[]>;
    public ovtaColumnDefs: ColDef[];
    public customRichTextID = NeptunePageTypeEnum.OVTAIndex;
    public isLoading: boolean = true;
    public url = environment.ocStormwaterToolsBaseUrl;

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit(): void {
        this.ovtaColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment ID", "OnlandVisualTrashAssessmentID", "OnlandVisualTrashAssessmentID"),
            this.utilityFunctionsService.createLinkColumnDef("Assessment Area Name", "OnlandVisualTrashAssessmentAreaName", "OnlandVisualTrashAssessmentAreaID", {
                InRouterLink: "../onland-visual-trash-assessment-area/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Score", "OnlandVisualTrashAssessmentScoreName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentScoreName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Type", "IsProgressAssessment", { CustomDropdownFilterField: "IsProgressAssessment" }),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "CompletedDate", "short"),
            this.utilityFunctionsService.createBasicColumnDef("Status", "OnlandVisualTrashAssessmentStatusName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentStatusName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Created By", "CreatedByPersonFullName"),
            this.utilityFunctionsService.createDateColumnDef("Created On", "CreatedDate", "short"),
        ];
        this.onlandVisualTrashAssessments$ = this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsGet().pipe(tap((x) => (this.isLoading = false)));
    }
}
