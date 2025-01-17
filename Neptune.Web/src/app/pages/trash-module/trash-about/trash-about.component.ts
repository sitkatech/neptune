import { Component, OnInit } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";

@Component({
    selector: "trash-about",
    templateUrl: "./trash-about.component.html",
    styleUrls: ["./trash-about.component.scss"],
    standalone: true,
    imports: [PageHeaderComponent],
})
export class TrashAboutComponent implements OnInit {
    constructor() {}

    public customRichTextTypeID: number = NeptunePageTypeEnum.TrashModuleProgramOverview;

    ngOnInit(): void {}
}
