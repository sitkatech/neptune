import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentAreaDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { environment } from "src/environments/environment";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { AsyncPipe, DatePipe, NgIf } from "@angular/common";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { NeptuneGridComponent } from "../../../../shared/components/neptune-grid/neptune-grid.component";
import { ColDef } from "ag-grid-community";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";

@Component({
    selector: "trash-ovta-area-detail",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NgIf, AsyncPipe, DatePipe, FieldDefinitionComponent, NeptuneGridComponent],
    templateUrl: "./trash-ovta-area-detail.component.html",
    styleUrl: "./trash-ovta-area-detail.component.scss",
})
export class TrashOvtaAreaDetailComponent {
    public onlandVisualTrashAssessmentArea$: Observable<OnlandVisualTrashAssessmentAreaDetailDto>;
    public ovtaColumnDefs: ColDef[];

    constructor(
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService,
        private utilityFunctionsService: UtilityFunctionsService,
        private route: ActivatedRoute,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.ovtaColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment ID", "OnlandVisualTrashAssessmentID", "OnlandVisualTrashAssessmentID"),
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
        this.onlandVisualTrashAssessmentArea$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(
                    params[routeParams.onlandVisualTrashAssessmentAreaID]
                );
            })
        );
    }

    getUrl() {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/2e1fd388-ab1e-45aa-950d-a47ea1bcb7f8";
    }
}
