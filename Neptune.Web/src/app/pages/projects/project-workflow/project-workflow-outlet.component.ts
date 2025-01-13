import { Component, OnInit } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { ProjectUpsertDto } from "src/app/shared/generated/model/project-upsert-dto";
import { ProjectWizardSidebarComponent } from "../../../shared/components/projects/project-wizard-sidebar/project-wizard-sidebar.component";
import { NgIf } from "@angular/common";

@Component({
    selector: "project-workflow-outlet",
    templateUrl: "./project-workflow-outlet.component.html",
    styleUrls: ["./project-workflow-outlet.component.scss"],
    standalone: true,
    imports: [NgIf, ProjectWizardSidebarComponent, RouterOutlet],
})
export class ProjectWorkflowOutletComponent implements OnInit {
    public projectModel: ProjectUpsertDto;

    constructor() {}

    ngOnInit(): void {}
}
