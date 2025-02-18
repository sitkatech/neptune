import { Injectable } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject, ReplaySubject, Observable, Subscription } from "rxjs";
import { OnlandVisualTrashAssessmentService } from "../generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentWorkflowProgressDto } from "../generated/model/onland-visual-trash-assessment-workflow-progress-dto";

@Injectable({
    providedIn: "root",
})
export class OvtaWorkflowProgressService {
    private progressSubject: Subject<OnlandVisualTrashAssessmentWorkflowProgressDto> = new ReplaySubject();
    public progressObservable$: Observable<OnlandVisualTrashAssessmentWorkflowProgressDto> = this.progressSubject.asObservable();

    private progressSubscription = Subscription.EMPTY;

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private route: ActivatedRoute, private router: Router) {}

    updateProgress(projectID: number): void {
        this.progressSubscription.unsubscribe();
        this.getProgress(projectID);
    }

    getProgress(projectID: number) {
        console.log(projectID);
        if (projectID) {
            this.progressSubscription = this.onlandVisualTrashAssessmentService
                .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet(projectID)
                .subscribe((response) => {
                    console.log(response);
                    this.progressSubject.next(response);
                });
        } else {
            this.progressSubject.next(
                new OnlandVisualTrashAssessmentWorkflowProgressDto({
                    Steps: {
                        Instructions: { Completed: true, Disabled: false },
                        InitiateOvta: { Completed: true, Disabled: false },
                        RecordObservations: { Completed: true, Disabled: false },
                        AddOrRemoveParcels: { Completed: true, Disabled: false },
                        RefineAssessmentArea: { Completed: true, Disabled: false },
                        ReviewAndFinalize: { Completed: true, Disabled: false },
                    },
                })
            );
        }
    }
}
