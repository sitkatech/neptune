import { Component, Input } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";
import { Router } from "@angular/router";

@Component({
    selector: "trash-ovta-instructions",
    imports: [PageHeaderComponent, CustomRichTextComponent],
    templateUrl: "./trash-ovta-instructions.component.html",
    styleUrl: "./trash-ovta-instructions.component.scss",
})
export class TrashOvtaInstructionsComponent {
    public customRichTextTypeID = NeptunePageTypeEnum.OVTAInstructions;
    @Input() onlandVisualTrashAssessmentID!: number;
    constructor(private router: Router) {}

    continueToNextStep() {
        if (this.onlandVisualTrashAssessmentID != null && this.onlandVisualTrashAssessmentID !== undefined) {
            this.router.navigateByUrl(`/trash/onland-visual-trash-assessments/edit/${this.onlandVisualTrashAssessmentID}/record-observations`);
        } else {
            this.router.navigateByUrl("/trash/onland-visual-trash-assessments/new/initiate-ovta");
        }
    }

    get buttonLabel(): string {
        return this.onlandVisualTrashAssessmentID != null && this.onlandVisualTrashAssessmentID !== undefined ? "Continue" : "Begin OVTA";
    }
}
