import { Component, OnInit } from "@angular/core";
import { filter, map, Observable, of, switchMap, tap, catchError, shareReplay } from "rxjs";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule, FormControl } from "@angular/forms";
import { WaterQualityManagementPlanService } from "src/app/shared/generated/api/water-quality-management-plan.service";
import { WaterQualityManagementPlanKeyDocumentsComponent } from "src/app/pages/ai-module/ai-home/water-quality-management-plan-documents/water-quality-management-plan-documents.component";
import { FormFieldComponent, FormFieldType, SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component";
import { WaterQualityManagementPlanDocumentDto, WaterQualityManagementPlanDocumentExtractionResultDto, WaterQualityManagementPlanDto } from "src/app/shared/generated/model/models";
import { RouterModule } from "@angular/router";
import { WfsService } from "src/app/shared/services/wfs.service";
import { IconComponent } from "src/app/shared/components/icon/icon.component";
import { AIService } from "src/app/shared/generated/api/ai.service";
import { WaterQualityManagementPlanChatbotComponent } from "./water-quality-management-plan-chatbot/water-quality-management-plan-chatbot.component";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";

@Component({
    selector: "ai-home",
    templateUrl: "./ai-home.component.html",
    styleUrls: ["./ai-home.component.scss"],
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        WaterQualityManagementPlanKeyDocumentsComponent,
        FormFieldComponent,
        RouterModule,
        IconComponent,
        WaterQualityManagementPlanChatbotComponent,
        LoadingDirective,
    ],
})
export class AiHomeComponent implements OnInit {
    public waterQualityManagementPlans$: Observable<SelectDropdownOption[]>;
    public waterQualityManagementPlans: WaterQualityManagementPlanDto[];
    public selectedPlan$: Observable<WaterQualityManagementPlanDto | null>;

    public selectedDocument: WaterQualityManagementPlanDocumentDto = null;
    public activeChatbotDocument: WaterQualityManagementPlanDocumentDto = null;

    public extractionResult: WaterQualityManagementPlanDocumentExtractionResultDto = null;
    public finalOutputObject: any = null;

    public imagePreview$: Observable<string>;

    public planControl = new FormControl<number | null>(null);
    public FormFieldType = FormFieldType;

    public isExtracting: boolean = false;

    constructor(
        private waterQualityManagementPlanService: WaterQualityManagementPlanService,
        private aiService: AIService,
        private wfsService: WfsService
    ) {}

    ngOnInit(): void {
        this.waterQualityManagementPlans$ = this.waterQualityManagementPlanService.listWithFinalWQMPDocumentWaterQualityManagementPlan().pipe(
            tap((plans) => {
                this.waterQualityManagementPlans = plans;
            }),
            map((list) => {
                let options = list.map((x) => ({ Value: x.WaterQualityManagementPlanID, Label: x.WaterQualityManagementPlanName }) as SelectDropdownOption);
                return options;
            })
        );

        this.selectedPlan$ = this.planControl.valueChanges.pipe(
            tap(() => {
                this.isExtracting = false;
                this.extractionResult = null;
                this.selectedDocument = null;
                this.activeChatbotDocument = null;
            }),
            map((value) => {
                return this.waterQualityManagementPlans.find((plan) => plan.WaterQualityManagementPlanID === value) || null;
            }),
            shareReplay(1)
        );

        this.imagePreview$ = this.selectedPlan$.pipe(
            filter((plan) => !!plan),
            switchMap((plan) => {
                return this.wfsService.getWQMPPreviewImage(plan).pipe(
                    catchError((err) => {
                        console.error("Error loading preview image", err);
                        return of(null);
                    })
                );
            })
        );
    }

    onDocumentSelectedChange(document: WaterQualityManagementPlanDocumentDto) {
        this.selectedDocument = document;
    }

    onClickExtractData() {
        this.activeChatbotDocument = this.selectedDocument;
        this.isExtracting = true;
        this.extractionResult = null;
        this.aiService.extractAllAI(this.selectedDocument.WaterQualityManagementPlanDocumentID).subscribe({
            next: (result) => {
                this.extractionResult = result;

                this.finalOutputObject = JSON.parse(this.extractionResult.FinalOutput);

                this.isExtracting = false;
            },
            error: () => {
                this.isExtracting = false;
            },
        });
    }

    onExtractingChange(isExtracting: boolean) {
        this.isExtracting = isExtracting;
        if (isExtracting) {
            this.planControl.disable();
        } else {
            this.planControl.enable();
        }
    }
}
