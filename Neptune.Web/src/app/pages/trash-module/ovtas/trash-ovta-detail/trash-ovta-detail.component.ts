import { AsyncPipe, DatePipe, KeyValuePipe, NgFor, NgIf } from "@angular/common";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-detail-dto";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { environment } from "src/environments/environment";

@Component({
    selector: "trash-ovta-detail",
    standalone: true,
    imports: [NgIf, AsyncPipe, AlertDisplayComponent, PageHeaderComponent, FieldDefinitionComponent, DatePipe, NgFor, KeyValuePipe],
    templateUrl: "./trash-ovta-detail.component.html",
    styleUrl: "./trash-ovta-detail.component.scss",
})
export class TrashOvtaDetailComponent {
    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private route: ActivatedRoute, private router: Router) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
    }

    getUrl() {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/2e1fd388-ab1e-45aa-950d-a47ea1bcb7f8";
    }
}
