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
import { environment } from "src/environments/environment.qa";
import { ModalService, ModalSizeEnum } from "src/app/shared/services/modal/modal.service";
import { FieldSourceModalComponent } from "./field-source-modal/field-source-modal.component";

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
    public currentExtractingTextIndex: number = 0;
    public extractingTexts: string[] = [
        "Extracting water quality plan data",
        "Parsing report structure and contents",
        "Analyzing document text for key water quality details",
        "Identifying relevant sections and data fields",
        "Processing water management plan tables and figures",
        "Mapping extracted data to standardized schema",
        "Performing quality checks on extracted values",
        "Generating structured summary for review",
        "Scanning your PDF for water quality parameters and management data",
        "Looking for treatment details, monitoring plans, and compliance info",
        "Reviewing extracted sections for accuracy",
        "Sifting through sediment... I mean, text",
        "Filtering the noise to find clean data",
        "Analyzing flow... of text and tables",
        "Your plan is under the microscope — extracting key water data",
        "Performing a final consistency check before returning results",
    ];

    public isChatbotOpen: boolean = false;

    constructor(
        private waterQualityManagementPlanService: WaterQualityManagementPlanService,
        private aiService: AIService,
        private wfsService: WfsService,
        private modalService: ModalService
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
        this.activeChatbotDocument = null;
        this.extractionResult = null;
        this.isChatbotOpen = false;
    }

    onClickExtractData() {
        this.activeChatbotDocument = this.selectedDocument;
        this.isExtracting = true;
        this.extractionResult = null;

        this.currentExtractingTextIndex = 0;
        let interval = setInterval(() => {
            this.currentExtractingTextIndex++;
            if (this.currentExtractingTextIndex >= this.extractingTexts.length) {
                this.currentExtractingTextIndex = this.extractingTexts.length - 1;
            }
        }, 7500);

        this.aiService.extractAllAI(this.selectedDocument.WaterQualityManagementPlanDocumentID).subscribe({
            next: (result) => {
                this.extractionResult = result;

                this.finalOutputObject = JSON.parse(this.extractionResult.FinalOutput);

                this.isExtracting = false;
                clearInterval(interval);
            },
            error: () => {
                this.isExtracting = false;
                clearInterval(interval);
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

    toggleChatbot() {
        if (this.activeChatbotDocument) {
            this.isChatbotOpen = !this.isChatbotOpen;
        }
    }

    onViewSource(fieldLabel: string, field: any) {
        if (!field) return;
        this.modalService.open(FieldSourceModalComponent, null, { ModalSize: ModalSizeEnum.Medium, CloseOnClickOut: true }, { FieldLabel: fieldLabel, Field: field });
    }

    ocStormwaterToolsMainUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }
}
