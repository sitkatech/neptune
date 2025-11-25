import { Component, OnInit, Input } from "@angular/core";
import { Router, RouterLink, RouterOutlet } from "@angular/router";
import { Observable, tap } from "rxjs";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import { AsyncPipe, DatePipe } from "@angular/common";
import { OnlandVisualTrashAssessmentWorkflowProgressDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-workflow-progress-dto";
import { OnlandVisualTrashAssessmentStatusEnum } from "src/app/shared/generated/enum/onland-visual-trash-assessment-status-enum";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { WorkflowNavComponent } from "../../../../shared/components/workflow-nav/workflow-nav.component";
import { WorkflowNavItemComponent } from "../../../../shared/components/workflow-nav/workflow-nav-item/workflow-nav-item.component";
import { IconComponent } from "../../../../shared/components/icon/icon.component";

@Component({
    selector: "trash-ovta-workflow-outlet",
    imports: [RouterOutlet, RouterLink, AsyncPipe, WorkflowNavComponent, WorkflowNavItemComponent, IconComponent],
    templateUrl: "./trash-ovta-workflow-outlet.component.html",
    styleUrl: "./trash-ovta-workflow-outlet.component.scss",
    standalone: true,
})
export class TrashOvtaWorkflowOutletComponent implements OnInit {
    public submitted: boolean = false;
    public progress$: Observable<OnlandVisualTrashAssessmentWorkflowProgressDto>;
    public OnlandVisualTrashAssessmentStatus = OnlandVisualTrashAssessmentStatusEnum;
    @Input() onlandVisualTrashAssessmentID: number | null = null;

    constructor(
        private router: Router,
        private ovtaProgressService: OvtaWorkflowProgressService,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private confirmService: ConfirmService,
        private alertService: AlertService,
        private datePipe: DatePipe
    ) {}

    ngOnInit() {
        this.progress$ = this.ovtaProgressService.progressObservable$.pipe(
            tap(() => {
                this.submitted = false;
            })
        );
        this.ovtaProgressService.getProgress(this.onlandVisualTrashAssessmentID);
    }

    public deleteOVTA(onlandVisualTrashAssessmentID: number, createdDate: string) {
        const modalContents = `<p>Are you sure you want to delete the assessment from ${this.datePipe.transform(createdDate, "MM/dd/yyyy")}?<\/p>`;
        this.confirmService
            .confirm({ buttonClassYes: "btn-primary", buttonTextYes: "Delete", buttonTextNo: "Cancel", title: "Delete OVTA", message: modalContents })
            .then((confirmed) => {
                if (confirmed) {
                    this.onlandVisualTrashAssessmentService.deleteOnlandVisualTrashAssessment(onlandVisualTrashAssessmentID).subscribe(() => {
                        this.alertService.clearAlerts();
                        this.alertService.pushAlert(new Alert("Your OVTA was successfully deleted.", AlertContext.Success));
                        this.router.navigate([`/trash/onland-visual-trash-assessments`]);
                    });
                }
            });
    }
}
