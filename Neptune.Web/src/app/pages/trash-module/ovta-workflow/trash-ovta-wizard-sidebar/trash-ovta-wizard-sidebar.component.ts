import { Component } from "@angular/core";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { Observable, tap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import { WorkflowNavComponent } from "../../../../shared/components/workflow-nav/workflow-nav.component";
import { WorkflowNavItemComponent } from "../../../../shared/components/workflow-nav/workflow-nav-item/workflow-nav-item.component";
import { AsyncPipe, DatePipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentWorkflowProgressDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-workflow-progress-dto";
import { OnlandVisualTrashAssessmentStatusEnum } from "src/app/shared/generated/enum/onland-visual-trash-assessment-status-enum";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { IconComponent } from "../../../../shared/components/icon/icon.component";

@Component({
    selector: "trash-ovta-wizard-sidebar",
    standalone: true,
    imports: [WorkflowNavComponent, WorkflowNavItemComponent, NgIf, AsyncPipe, RouterLink, IconComponent],
    templateUrl: "./trash-ovta-wizard-sidebar.component.html",
    styleUrl: "./trash-ovta-wizard-sidebar.component.scss",
})
export class TrashOvtaWizardSidebarComponent {
    public submitted: boolean = false;
    public progress$: Observable<OnlandVisualTrashAssessmentWorkflowProgressDto>;
    public ovtaID: number;
    public OnlandVisualTrashAssessmentStatus = OnlandVisualTrashAssessmentStatusEnum;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private ovtaProgressService: OvtaWorkflowProgressService,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private confirmService: ConfirmService,
        private alertService: AlertService,
        private datePipe: DatePipe
    ) {}

    ngOnInit() {
        this.ovtaID = this.route.snapshot.paramMap.get(routeParams.onlandVisualTrashAssessmentID)
            ? parseInt(this.route.snapshot.paramMap.get(routeParams.onlandVisualTrashAssessmentID))
            : null;
        this.progress$ = this.ovtaProgressService.progressObservable$.pipe(
            tap((x) => {
                this.submitted = false;
            })
        );
        this.ovtaProgressService.getProgress(this.ovtaID);
    }

    public deleteOVTA(onlandVisualTrashAssessmentID: number, createdDate: string) {
        const modalContents = `<p>Are you sure you want to delete the assessment from ${this.datePipe.transform(createdDate, "MM/dd/yyyy")}?</p>`;
        this.confirmService
            .confirm({ buttonClassYes: "btn-primary", buttonTextYes: "Delete", buttonTextNo: "Cancel", title: "Delete OVTA", message: modalContents })
            .then((confirmed) => {
                if (confirmed) {
                    this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDDelete(onlandVisualTrashAssessmentID).subscribe((response) => {
                        this.alertService.clearAlerts();
                        this.alertService.pushAlert(new Alert("Your OVTA was successfully deleted.", AlertContext.Success));
                        this.router.navigate([`/trash/onland-visual-trash-assessments`]);
                    });
                }
            });
    }
}
