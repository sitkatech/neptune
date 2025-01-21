import { Component, OnInit } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";

@Component({
    selector: "planning-about",
    templateUrl: "./planning-about.component.html",
    styleUrls: ["./planning-about.component.scss"],
    standalone: true,
    imports: [PageHeaderComponent],
})
export class PlanningAboutComponent implements OnInit {
    constructor() {}

    public customRichTextTypeID: number = NeptunePageTypeEnum.HippocampAbout;

    ngOnInit(): void {}
}
