import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { WaterQualityManagementPlanChatbotComponent } from "../water-quality-management-plan-chatbot/water-quality-management-plan-chatbot.component";
import { Observable, tap } from "rxjs";
import { environment } from "src/environments/environment";
import { WaterQualityManagementPlanService } from "../../generated/api/water-quality-management-plan.service";
import { AIService } from "../../generated/api/ai.service";
import { WaterQualityManagementPlanDocumentComponent } from "../water-quality-management-plan-document/water-quality-management-plan-document.component";
import { WaterQualityManagementPlanDocumentTypes } from "../../generated/enum/water-quality-management-plan-document-type-enum";
import { AsyncPipe } from "@angular/common";
import { WaterQualityManagementPlanDocumentDto } from "../../generated/model/water-quality-management-plan-document-dto";
import { WaterQualityManagementPlanDocumentExtractionResultDto } from "../../generated/model/water-quality-management-plan-document-extraction-result-dto";
import { WaterQualityManagementPlanDto } from "../../generated/model/water-quality-management-plan-dto";
import { LookupTableEntry } from "../../models/lookup-table-entry";
import { LoadingDirective } from "../../directives/loading.directive";

@Component({
    selector: "water-quality-management-plan-documents",
    templateUrl: "./water-quality-management-plan-documents.component.html",
    styleUrls: ["./water-quality-management-plan-documents.component.scss"],
    imports: [WaterQualityManagementPlanDocumentComponent, AsyncPipe, WaterQualityManagementPlanChatbotComponent, LoadingDirective],
})
export class WaterQualityManagementPlanKeyDocumentsComponent implements OnInit, OnChanges {
    // Notify parent when extraction state changes
    @Output() extractingChange = new EventEmitter<boolean>();
    @Input() waterQualityManagementPlanID!: number;
    public waterQualityManagementPlan$: Observable<WaterQualityManagementPlanDto>;
    public waterQualityManagementPlanDocuments$: Observable<WaterQualityManagementPlanDocumentDto[]>;
    public isLoading: boolean = false;
    public activeChatbotDocument: WaterQualityManagementPlanDocumentDto = null;
    public extractionResult: WaterQualityManagementPlanDocumentExtractionResultDto = null;
    public isExtracting: boolean = false;

    constructor(private waterQualityManagementPlanService: WaterQualityManagementPlanService, private aiService: AIService) {}

    ngOnInit(): void {
        this.loadPlanData();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes["waterQualityManagementPlanID"] && !changes["waterQualityManagementPlanID"].firstChange) {
            this.loadPlanData();
            this.activeChatbotDocument = null; // Clear chatbot when switching WQMP
        }
    }

    private loadPlanData(): void {
        if (this.waterQualityManagementPlanID) {
            this.waterQualityManagementPlan$ = this.waterQualityManagementPlanService.getWaterQualityManagementPlan(this.waterQualityManagementPlanID).pipe(
                tap(() => {
                    this.refreshData();
                })
            );
        }
    }

    objectKeys(obj: any): string[] {
        return Object.keys(obj);
    }

    refreshData() {
        this.isLoading = true;

        this.waterQualityManagementPlanDocuments$ = this.waterQualityManagementPlanService.listDocumentsWaterQualityManagementPlan(this.waterQualityManagementPlanID).pipe(
            tap(() => {
                this.isLoading = false;
            })
        );
    }

    /**
     * Returns WaterQualityManagementPlanDocumentTypes sorted by display name
     */
    public getWaterQualityManagementPlanDocumentTypes(): Array<LookupTableEntry> {
        return WaterQualityManagementPlanDocumentTypes.filter((x) => x.Name === "FinalWQMP").sort((a, b) => a.Name.localeCompare(b.Name));
    }

    /**
     * Returns documents for a given WaterQualityManagementPlanDocumentTypeID
     */
    public getDocumentsByType(documents: WaterQualityManagementPlanDocumentDto[], waterQualityManagementPlanDocumentTypeID: number): WaterQualityManagementPlanDocumentDto[] {
        return documents
            .filter((doc) => doc.WaterQualityManagementPlanDocumentTypeID === waterQualityManagementPlanDocumentTypeID)
            .sort((a, b) => a.DisplayName.localeCompare(b.DisplayName));
    }

    downloadFile(waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto) {
        const url = `${environment.mainAppApiUrl}/file-resources/${waterQualityManagementPlanDocument.FileResource.FileResourceGUID}`;
        const anchor = document.createElement("a");
        anchor.href = url;
        anchor.target = "_blank";
        anchor.download = waterQualityManagementPlanDocument.FileResource.OriginalFilename || "document.pdf";
        document.body.appendChild(anchor);
        anchor.click();
        document.body.removeChild(anchor);
    }

    onClickExtractData(waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto) {
        this.activeChatbotDocument = waterQualityManagementPlanDocument;
        this.isExtracting = true;
        this.extractingChange.emit(true);
        this.extractionResult = null;
        this.aiService.extractAllAI(waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentID).subscribe({
            next: (result) => {
                this.extractionResult = result;
                this.isExtracting = false;
                this.extractingChange.emit(false);
            },
            error: () => {
                this.isExtracting = false;
                this.extractingChange.emit(false);
            },
        });
    }
}
