import { Component, Input, OnInit, OnChanges, SimpleChanges } from "@angular/core";
import { DialogService } from "@ngneat/dialog";
import { WaterQualityManagementPlanChatbotComponent } from "../water-quality-management-plan-chatbot/water-quality-management-plan-chatbot.component";
import { Observable, tap } from "rxjs";
import { environment } from "src/environments/environment";
import { WaterQualityManagementPlanService } from "../../generated/api/water-quality-management-plan.service";
import { WaterQualityManagementPlanDocumentComponent } from "../water-quality-management-plan-document/water-quality-management-plan-document.component";
import { WaterQualityManagementPlanDocumentTypes } from "../../generated/enum/water-quality-management-plan-document-type-enum";
import { AsyncPipe } from "@angular/common";
import { WaterQualityManagementPlanDocumentDto } from "../../generated/model/water-quality-management-plan-document-dto";
import { WaterQualityManagementPlanDto } from "../../generated/model/water-quality-management-plan-dto";
import { LookupTableEntry } from "../../models/lookup-table-entry";

@Component({
    selector: "water-quality-management-plan-documents",
    templateUrl: "./water-quality-management-plan-documents.component.html",
    styleUrls: ["./water-quality-management-plan-documents.component.scss"],
    imports: [WaterQualityManagementPlanDocumentComponent, AsyncPipe],
})
export class WaterQualityManagementPlanKeyDocumentsComponent implements OnInit, OnChanges {
    @Input() waterQualityManagementPlanID!: number;
    public waterQualityManagementPlan$: Observable<WaterQualityManagementPlanDto>;
    public waterQualityManagementPlanDocuments$: Observable<WaterQualityManagementPlanDocumentDto[]>;
    public isLoading: boolean = false;

    constructor(private waterQualityManagementPlanService: WaterQualityManagementPlanService, private dialogService: DialogService) {}

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
        return WaterQualityManagementPlanDocumentTypes.sort((a, b) => a.Name.localeCompare(b.Name));
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
            height: "90vh",
            width: "1600px",
            // Optionally set width, height, etc. here
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
}
