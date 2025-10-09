import { Component, Input, OnInit, OnChanges, SimpleChanges } from "@angular/core";
import { DialogService } from "@ngneat/dialog";
import { WaterQualityManagementPlanChatbotComponent } from "../water-quality-management-plan-chatbot/water-quality-management-plan-chatbot.component";
import { Observable, tap } from "rxjs";
import { environment } from "src/environments/environment";
import { WaterQualityManagementPlanService } from "../../generated/api/water-quality-management-plan.service";
import { WaterQualityManagementPlanDocumentComponent } from "../water-quality-management-plan-document/water-quality-management-plan-document.component";
import { WaterQualityManagementPlanDocumentTypes } from "../../generated/enum/water-quality-management-plan-document-type-enum";
import { AsyncPipe, JsonPipe } from "@angular/common";
import { WaterQualityManagementPlanDocumentDto } from "../../generated/model/water-quality-management-plan-document-dto";
import { WaterQualityManagementPlanDto } from "../../generated/model/water-quality-management-plan-dto";
import { LookupTableEntry } from "../../models/lookup-table-entry";
import { AIService } from "../../generated/api/ai.service";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "water-quality-management-plan-documents",
    templateUrl: "./water-quality-management-plan-documents.component.html",
    styleUrls: ["./water-quality-management-plan-documents.component.scss"],
    imports: [WaterQualityManagementPlanDocumentComponent, AsyncPipe, JsonPipe],
})
export class WaterQualityManagementPlanKeyDocumentsComponent implements OnInit, OnChanges {
    @Input() waterQualityManagementPlanID!: number;
    public waterQualityManagementPlan$: Observable<WaterQualityManagementPlanDto>;
    public waterQualityManagementPlanDocuments$: Observable<WaterQualityManagementPlanDocumentDto[]>;
    public isLoading: boolean = false;
    public extractedDataResult: any = null;
    public isExtractingData: boolean = false;

    constructor(
        private waterQualityManagementPlanService: WaterQualityManagementPlanService,
        private dialogService: DialogService,
        private aiService: AIService,
        private http: HttpClient
    ) {}

    ngOnInit(): void {
        this.loadPlanData();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes["waterQualityManagementPlanID"] && !changes["waterQualityManagementPlanID"].firstChange) {
            this.loadPlanData();
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
            tap((results) => {
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

    onClickOpenWaterQualityManagementPlanSummary(
        waterQualityManagementPlan: WaterQualityManagementPlanDto,
        waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto
    ) {
        this.dialogService.open(WaterQualityManagementPlanChatbotComponent, {
            data: {
                WaterQualityManagementPlan: waterQualityManagementPlan,
                KeyDocument: waterQualityManagementPlanDocument,
            },
            minHeight: "400px",
            maxHeight: "90vh",
            width: "1600px",
            // Optionally set width, minHeight, maxHeight, etc. here
        });
    }

    formatFileSize(bytes: number, decimals = 2) {
        if (!+bytes) return "0 Bytes";

        const k = 1024;
        const dm = decimals < 0 ? 0 : decimals;
        const sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];

        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return `${parseFloat((bytes / Math.pow(k, i)).toFixed(dm))} ${sizes[i]}`;
    }

    downloadFile(waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto) {
        return `${environment.mainAppApiUrl}/FileResource/${waterQualityManagementPlanDocument.FileResource.FileResourceGUID}`;
    }

    onClickExtractData(waterQualityManagementPlan: any, keyDocument: any) {
        this.isExtractingData = true;
        this.extractedDataResult = null;
        const url = `/ai/water-quality-management-plans/${waterQualityManagementPlan.WaterQualityManagementPlanID}/documents/${keyDocument.WaterQualityManagementPlanDocumentID}/extract-data`;
        this.http.post(url, { responseType: "text" }).subscribe({
            next: (result) => {
                this.extractedDataResult = result;
                this.isExtractingData = false;
            },
            error: (err) => {
                this.extractedDataResult = { error: "Failed to extract data", details: err.error?.text || err.message || err };
                this.isExtractingData = false;
            },
        });
    }
}
