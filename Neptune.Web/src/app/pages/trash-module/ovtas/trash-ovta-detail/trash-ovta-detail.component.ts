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
import { ObservationsMapComponent } from "../observations-map/observations-map.component";
import { OnlandVisualTrashAssessmentObservationWithPhotoDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";
import { OnlandVisualTrashAssessmentObservationService } from "src/app/shared/generated/api/onland-visual-trash-assessment-observation.service";
import { PreliminarySourceIdentificationCategories } from "src/app/shared/generated/enum/preliminary-source-identification-category-enum";
import { OnlandVisualTrashAssessmentStatusEnum } from "src/app/shared/generated/enum/onland-visual-trash-assessment-status-enum";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";

@Component({
    selector: "trash-ovta-detail",
    standalone: true,
    imports: [NgIf, AsyncPipe, AlertDisplayComponent, PageHeaderComponent, FieldDefinitionComponent, DatePipe, NgFor, ObservationsMapComponent, RouterLink],
    templateUrl: "./trash-ovta-detail.component.html",
    styleUrl: "./trash-ovta-detail.component.scss",
})
export class TrashOvtaDetailComponent {
    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;
    public onlandVisualTrashAssessmentObservations$: Observable<OnlandVisualTrashAssessmentObservationWithPhotoDto[]>;
    public PreliminarySourceIdentificationCategories = PreliminarySourceIdentificationCategories;

    public ovtaID: number;

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentObservationService: OnlandVisualTrashAssessmentObservationService,
        private route: ActivatedRoute,
        private alertService: AlertService,
        private confirmService: ConfirmService,
        private router: Router,
        private datePipe: DatePipe
    ) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
        this.onlandVisualTrashAssessmentObservations$ = this.onlandVisualTrashAssessment$.pipe(
            switchMap((onlandVisualTrashAssessment) => {
                return this.onlandVisualTrashAssessmentObservationService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsGet(
                    onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID
                );
            })
        );
    }

    public confirmEditOVTA(onlandVisualTrashAssessmentID: number, onlandVisualTrashAssessmentStatusID: number, completedDate: string) {
        if (onlandVisualTrashAssessmentStatusID == OnlandVisualTrashAssessmentStatusEnum.Complete) {
            const modalContents = `<p>This OVTA was finalized on ${this.datePipe.transform(
                completedDate,
                "MM/dd/yyyy"
            )}. Are you sure you want to revert this OVTA to the \"In Progress\" status?</p>`;
            this.confirmService
                .confirm({ buttonClassYes: "btn-primary", buttonTextYes: "Continue", buttonTextNo: "Cancel", title: "Return OVTA to Edit", message: modalContents })
                .then((confirmed) => {
                    if (confirmed) {
                        this.onlandVisualTrashAssessmentService
                            .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDReturnToEditPost(onlandVisualTrashAssessmentID)
                            .subscribe((response) => {
                                this.alertService.clearAlerts();
                                this.alertService.pushAlert(new Alert('The OVTA was successfully returned to the "In Progress" status.', AlertContext.Success));
                                this.router.navigateByUrl(`/trash/onland-visual-trash-assessments/edit/${onlandVisualTrashAssessmentID}/record-observations`);
                            });
                    }
                });
        } else {
            this.router.navigateByUrl(`/trash/onland-visual-trash-assessments/edit/${onlandVisualTrashAssessmentID}/record-observations`);
        }
    }
}
