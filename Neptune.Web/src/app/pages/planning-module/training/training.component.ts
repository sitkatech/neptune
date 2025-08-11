import { Component, OnInit } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";

@Component({
    selector: "training",
    templateUrl: "./training.component.html",
    styleUrls: ["./training.component.scss"],
    imports: [PageHeaderComponent]
})
export class TrainingComponent implements OnInit {
    constructor() {}

    public customRichTextTypeID: number = NeptunePageTypeEnum.HippocampTraining;

    ngOnInit() {}
}
