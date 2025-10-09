import { Component, Input, Output, EventEmitter } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DatePipe } from "@angular/common";
import { IconComponent } from "../icon/icon.component";
import { WaterQualityManagementPlanDto } from "../../generated/model/water-quality-management-plan-dto";
import { WaterQualityManagementPlanDocumentDto } from "../../generated/model/water-quality-management-plan-document-dto";

@Component({
    selector: "water-quality-management-plan-document",
    templateUrl: "./water-quality-management-plan-document.component.html",
    styleUrls: ["./water-quality-management-plan-document.component.scss"],
    standalone: true,
    imports: [IconComponent, RouterModule, DatePipe],
})
export class WaterQualityManagementPlanDocumentComponent {
    @Input() keyDocument: WaterQualityManagementPlanDocumentDto;
    @Input() waterQualityManagementPlan: WaterQualityManagementPlanDto;
    @Input() isLoading: boolean = false;

    @Output() vote = new EventEmitter<{ keyDocument: WaterQualityManagementPlanDocumentDto; isAccurate: boolean }>();
    @Output() download = new EventEmitter<WaterQualityManagementPlanDocumentDto>();
    @Output() extractData = new EventEmitter<{ waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto }>();

    onVote(isAccurate: boolean) {
        this.vote.emit({ keyDocument: this.keyDocument, isAccurate });
    }

    onDownload() {
        this.download.emit(this.keyDocument);
    }

    onClickExtractData() {
        this.extractData.emit({ waterQualityManagementPlanDocument: this.keyDocument });
    }

    formatFileSize(bytes: number, decimals = 2) {
        if (!+bytes) return "0 Bytes";

        const k = 1024;
        const dm = decimals < 0 ? 0 : decimals;
        const sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];

        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return `${parseFloat((bytes / Math.pow(k, i)).toFixed(dm))} ${sizes[i]}`;
    }
}
