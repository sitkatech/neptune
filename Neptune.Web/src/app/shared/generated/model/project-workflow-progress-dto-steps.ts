/**
 * Neptune.API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { WorkflowStepStatus } from '././workflow-step-status';

import { FormControl, FormControlOptions, FormControlState, Validators } from "@angular/forms";
export class ProjectWorkflowProgressDtoSteps { 
    Instructions?: WorkflowStepStatus;
    BasicInfo?: WorkflowStepStatus;
    TreatmentBMPs?: WorkflowStepStatus;
    Delineations?: WorkflowStepStatus;
    ModeledPerformanceAndGrantMetrics?: WorkflowStepStatus;
    Attachments?: WorkflowStepStatus;
    ReviewAndShare?: WorkflowStepStatus;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}

export interface ProjectWorkflowProgressDtoStepsForm { 
    Instructions?: FormControl<WorkflowStepStatus>;
    BasicInfo?: FormControl<WorkflowStepStatus>;
    TreatmentBMPs?: FormControl<WorkflowStepStatus>;
    Delineations?: FormControl<WorkflowStepStatus>;
    ModeledPerformanceAndGrantMetrics?: FormControl<WorkflowStepStatus>;
    Attachments?: FormControl<WorkflowStepStatus>;
    ReviewAndShare?: FormControl<WorkflowStepStatus>;
}

export class ProjectWorkflowProgressDtoStepsFormControls { 
    public static Instructions = (value: FormControlState<WorkflowStepStatus> | WorkflowStepStatus = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<WorkflowStepStatus>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static BasicInfo = (value: FormControlState<WorkflowStepStatus> | WorkflowStepStatus = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<WorkflowStepStatus>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static TreatmentBMPs = (value: FormControlState<WorkflowStepStatus> | WorkflowStepStatus = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<WorkflowStepStatus>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Delineations = (value: FormControlState<WorkflowStepStatus> | WorkflowStepStatus = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<WorkflowStepStatus>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ModeledPerformanceAndGrantMetrics = (value: FormControlState<WorkflowStepStatus> | WorkflowStepStatus = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<WorkflowStepStatus>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static Attachments = (value: FormControlState<WorkflowStepStatus> | WorkflowStepStatus = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<WorkflowStepStatus>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
    public static ReviewAndShare = (value: FormControlState<WorkflowStepStatus> | WorkflowStepStatus = undefined, formControlOptions?: FormControlOptions | null) => new FormControl<WorkflowStepStatus>(
        value,
        formControlOptions ?? 
        {
            nonNullable: false,
            validators: 
            [
            ],
        }
    );
}
