import { Component, Input, OnInit } from "@angular/core";
import { NgbTooltip } from "@ng-bootstrap/ng-bootstrap";
import { IconComponent } from "../icon/icon.component";
import { NgIf } from "@angular/common";

@Component({
    selector: "progress-icon",
    templateUrl: "./progress-icon.component.html",
    styleUrls: ["./progress-icon.component.scss"],
    standalone: true,
    imports: [NgIf, IconComponent, NgbTooltip],
})
export class ProgressIconComponent implements OnInit {
    @Input() isComplete: boolean = false;
    @Input() isInformational: boolean = false;

    public tooltipCompleteText: string = "Completed Step";
    public tooltipIncompleteText: string = "Incomplete Step";

    constructor() {}

    ngOnInit(): void {}
}
