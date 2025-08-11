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

    updateProgress(ovtaID: number): void {
        this.progressSubscription.unsubscribe();
        this.getProgress(ovtaID);
    }

    getProgress(ovtaID: number) {
        if (ovtaID) {
            this.progressSubscription = this.onlandVisualTrashAssessmentService
                .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDProgressGet(ovtaID)
                .subscribe((response) => {
                    this.progressSubject.next(response);
                });
        } else {
            this.progressSubject.next(
                new OnlandVisualTrashAssessmentWorkflowProgressDto({
                    Steps: {
                        Instructions: { Completed: true, Disabled: false },
                        InitiateOvta: { Completed: false, Disabled: false },
                        RecordObservations: { Completed: false, Disabled: true },
                        AddOrRemoveParcels: { Completed: false, Disabled: true },
                        RefineAssessmentArea: { Completed: false, Disabled: true },
                        ReviewAndFinalize: { Completed: false, Disabled: true },
                    },
                })
            );
        }
    }
}
