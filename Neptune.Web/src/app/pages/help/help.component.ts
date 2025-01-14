import { Component, OnInit } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";

@Component({
    selector: "help",
    templateUrl: "./help.component.html",
    styleUrls: ["./help.component.scss"],
})
export class HelpComponent implements OnInit {
    public richTextTypeID: number = NeptunePageTypeEnum.Help;

    constructor() {}

    ngOnInit() {}
}
