import { AsyncPipe, DatePipe, KeyValuePipe, NgClass, NgFor, NgIf } from "@angular/common";
import { Component } from "@angular/core";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-detail-dto";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { environment } from "src/environments/environment";
import { ObservationsMapComponent } from "../observations-map/observations-map.component";

@Component({
    selector: "trash-ovta-detail",
    standalone: true,
    imports: [NgIf, AsyncPipe, AlertDisplayComponent, PageHeaderComponent, FieldDefinitionComponent, DatePipe, NgFor, KeyValuePipe, ObservationsMapComponent, NgClass, RouterLink],
    templateUrl: "./trash-ovta-detail.component.html",
    styleUrl: "./trash-ovta-detail.component.scss",
})
export class TrashOvtaDetailComponent {
    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;

    public ovtaID: number;

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private route: ActivatedRoute, private router: Router) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
    }
}
