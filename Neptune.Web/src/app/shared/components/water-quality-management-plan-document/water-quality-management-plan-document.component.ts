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
    @Output() openDocumentChatBot = new EventEmitter<{ waterQualityManagementPlan: WaterQualityManagementPlanDto; keyDocument: WaterQualityManagementPlanDocumentDto }>();
    @Output() download = new EventEmitter<WaterQualityManagementPlanDocumentDto>();

    onVote(isAccurate: boolean) {
        this.vote.emit({ keyDocument: this.keyDocument, isAccurate });
    }

    onClickOpenProjectSummary() {
        this.openDocumentChatBot.emit({ waterQualityManagementPlan: this.waterQualityManagementPlan, keyDocument: this.keyDocument });
    }

    onDownload() {
        this.download.emit(this.keyDocument);
    }
}
