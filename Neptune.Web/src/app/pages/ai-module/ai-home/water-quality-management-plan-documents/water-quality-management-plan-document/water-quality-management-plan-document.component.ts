import { Component, Input, Output, EventEmitter } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DatePipe, NgClass } from "@angular/common";
import { IconComponent } from "../../../../../shared/components/icon/icon.component";
import { WaterQualityManagementPlanDto } from "../../../../../shared/generated/model/water-quality-management-plan-dto";
import { WaterQualityManagementPlanDocumentDto } from "../../../../../shared/generated/model/water-quality-management-plan-document-dto";

@Component({
    selector: "water-quality-management-plan-document",
    templateUrl: "./water-quality-management-plan-document.component.html",
    styleUrls: ["./water-quality-management-plan-document.component.scss"],
    standalone: true,
    imports: [NgClass, IconComponent, RouterModule, DatePipe, NgClass],
})
export class WaterQualityManagementPlanDocumentComponent {
    @Input() disableExtract: boolean = false;
    @Input() keyDocument: WaterQualityManagementPlanDocumentDto;
    @Input() waterQualityManagementPlan: WaterQualityManagementPlanDto;
    @Input() isLoading: boolean = false;
    @Input() selected: boolean = false;

    @Output() download = new EventEmitter<WaterQualityManagementPlanDocumentDto>();

    onDownload() {
        this.download.emit(this.keyDocument);
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
