import { Component, Input, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { filter, Observable, tap } from "rxjs";
import { environment } from "src/environments/environment";
import { WaterQualityManagementPlanService } from "../../../../shared/generated/api/water-quality-management-plan.service";
import { AIService } from "../../../../shared/generated/api/ai.service";
import { WaterQualityManagementPlanDocumentComponent } from "./water-quality-management-plan-document/water-quality-management-plan-document.component";
import { WaterQualityManagementPlanDocumentTypes } from "../../../../shared/generated/enum/water-quality-management-plan-document-type-enum";
import { AsyncPipe } from "@angular/common";
import { WaterQualityManagementPlanDocumentDto } from "../../../../shared/generated/model/water-quality-management-plan-document-dto";
import { WaterQualityManagementPlanDocumentExtractionResultDto } from "../../../../shared/generated/model/water-quality-management-plan-document-extraction-result-dto";
import { WaterQualityManagementPlanDto } from "../../../../shared/generated/model/water-quality-management-plan-dto";

@Component({
    selector: "water-quality-management-plan-documents",
    templateUrl: "./water-quality-management-plan-documents.component.html",
    styleUrls: ["./water-quality-management-plan-documents.component.scss"],
    imports: [WaterQualityManagementPlanDocumentComponent, AsyncPipe],
})
export class WaterQualityManagementPlanKeyDocumentsComponent implements OnInit, OnChanges {
    @Input() waterQualityManagementPlanID!: number;
    @Input() activeChatbotDocument: WaterQualityManagementPlanDocumentDto = null;

    @Output() documentSelectedChange = new EventEmitter<WaterQualityManagementPlanDocumentDto>();
    @Output() extractingChange = new EventEmitter<boolean>();

    public waterQualityManagementPlan$: Observable<WaterQualityManagementPlanDto>;
    public waterQualityManagementPlanDocuments$: Observable<WaterQualityManagementPlanDocumentDto[]>;
    public isLoading: boolean = false;
    public extractionResult: WaterQualityManagementPlanDocumentExtractionResultDto = null;
    public isExtracting: boolean = false;
    public selectedDocument: WaterQualityManagementPlanDocumentDto = null;

    constructor(
        private waterQualityManagementPlanService: WaterQualityManagementPlanService,
        private aiService: AIService
    ) {}

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
            filter((docs) => !!docs),
            filter((docs) =>
                docs.some((doc) => doc.WaterQualityManagementPlanDocumentTypeID === WaterQualityManagementPlanDocumentTypes.find((x) => x.Name === "FinalWQMP").Value)
            ),
            tap((finalWQMPs) => {
                this.isLoading = false;
                if (finalWQMPs.length == 1) {
                    this.selectedDocument = finalWQMPs[0];
                    this.documentSelectedChange.emit(this.selectedDocument);
                }
            })
        );
    }

    onDocumentClicked(waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto) {
        this.selectedDocument = waterQualityManagementPlanDocument;
        this.documentSelectedChange.emit(this.selectedDocument);
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
}
