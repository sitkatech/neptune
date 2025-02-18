import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Observable, tap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import { WorkflowNavComponent } from "../../../../shared/components/workflow-nav/workflow-nav.component";
import { WorkflowNavItemComponent } from "../../../../shared/components/workflow-nav/workflow-nav-item/workflow-nav-item.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentWorkflowProgressDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-workflow-progress-dto";

@Component({
    selector: "trash-ovta-wizard-sidebar",
    standalone: true,
    imports: [WorkflowNavComponent, WorkflowNavItemComponent, NgIf, AsyncPipe],
    templateUrl: "./trash-ovta-wizard-sidebar.component.html",
    styleUrl: "./trash-ovta-wizard-sidebar.component.scss",
})
export class TrashOvtaWizardSidebarComponent {
    public submitted: boolean = false;
    public progress$: Observable<OnlandVisualTrashAssessmentWorkflowProgressDto>;
    public ovtaID: number;

    constructor(private route: ActivatedRoute, private ovtaProgressService: OvtaWorkflowProgressService) {}

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
}
