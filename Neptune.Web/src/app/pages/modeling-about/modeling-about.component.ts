import { Component, OnInit } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";

@Component({
    selector: "modeling-about",
    templateUrl: "./modeling-about.component.html",
    styleUrls: ["./modeling-about.component.scss"],
    imports: [PageHeaderComponent]
})
export class ModelingAboutComponent implements OnInit {
    constructor() {}

    public customRichTextTypeID: number = NeptunePageTypeEnum.ModelingHomePage;

    ngOnInit(): void {}
}
