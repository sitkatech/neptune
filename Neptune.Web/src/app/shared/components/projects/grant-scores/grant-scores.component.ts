import { Component, Input, OnInit } from "@angular/core";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { CustomRichTextComponent } from "../../custom-rich-text/custom-rich-text.component";
import { FieldDefinitionComponent } from "../../field-definition/field-definition.component";
import { NgIf, DecimalPipe, PercentPipe } from "@angular/common";
import { ProjectDto } from "src/app/shared/generated/model/project-dto";
import { BtnGroupRadioInputComponent } from "../../inputs/btn-group-radio-input/btn-group-radio-input.component";

@Component({
    selector: "grant-scores",
    templateUrl: "./grant-scores.component.html",
    styleUrls: ["./grant-scores.component.scss"],
    standalone: true,
    imports: [NgIf, FieldDefinitionComponent, CustomRichTextComponent, DecimalPipe, PercentPipe, BtnGroupRadioInputComponent],
})
export class GrantScoresComponent implements OnInit {
    @Input("project") project: ProjectDto;
    public OCTAM2Tier2RichTextTypeID = NeptunePageTypeEnum.OCTAM2Tier2GrantProgramMetrics;

    public activeTab: string = "OCTA M2 Tier 2 Grant Program";
    public tabs = [{ label: "OCTA M2 Tier 2 Grant Program", value: "OCTA M2 Tier 2 Grant Program" }];

    constructor() {}

    ngOnInit(): void {}

    public setActiveTab(event) {
        this.activeTab = event;
    }
}
