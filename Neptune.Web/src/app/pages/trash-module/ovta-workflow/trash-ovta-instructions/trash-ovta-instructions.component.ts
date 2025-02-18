import { Component } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";

@Component({
    selector: "trash-ovta-instructions",
    standalone: true,
    imports: [PageHeaderComponent, CustomRichTextComponent],
    templateUrl: "./trash-ovta-instructions.component.html",
    styleUrl: "./trash-ovta-instructions.component.scss",
})
export class TrashOvtaInstructionsComponent {
    public rteID = NeptunePageTypeEnum.OVTAInstructions;
}
