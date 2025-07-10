import { Component, OnInit } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { ProjectUpsertDto } from "src/app/shared/generated/model/project-upsert-dto";
import { ProjectWizardSidebarComponent } from "src/app/pages/planning-module/projects/project-wizard-sidebar/project-wizard-sidebar.component";

@Component({
    selector: "project-workflow-outlet",
    templateUrl: "./project-workflow-outlet.component.html",
    styleUrls: ["./project-workflow-outlet.component.scss"],
    imports: [ProjectWizardSidebarComponent, RouterOutlet]
})
export class ProjectWorkflowOutletComponent implements OnInit {
    public projectModel: ProjectUpsertDto;

    constructor() {}

    ngOnInit(): void {}
}
