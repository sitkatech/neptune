import { Component } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";
import { ActivatedRoute, Router } from "@angular/router";
import { routeParams } from "src/app/app.routes";

@Component({
    selector: "trash-ovta-instructions",
    standalone: true,
    imports: [PageHeaderComponent, CustomRichTextComponent],
    templateUrl: "./trash-ovta-instructions.component.html",
    styleUrl: "./trash-ovta-instructions.component.scss",
})
export class TrashOvtaInstructionsComponent {
    public customRichTextTypeID = NeptunePageTypeEnum.OVTAInstructions;
    constructor(private router: Router, private route: ActivatedRoute) {}

    continueToNextStep() {
        var ovtaID = this.route.snapshot.paramMap.get(routeParams.onlandVisualTrashAssessmentID)
            ? parseInt(this.route.snapshot.paramMap.get(routeParams.onlandVisualTrashAssessmentID))
            : null;
        if (ovtaID != null) {
            this.router.navigateByUrl(`/trash/onland-visual-trash-assessments/edit/${ovtaID}/initiate-ovta`);
        } else {
            this.router.navigateByUrl("/trash/onland-visual-trash-assessments/new/initiate-ovta");
        }
    }
}
