import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterOutlet } from "@angular/router";
import { AuthenticationService } from "src/app/services/authentication.service";
import { ProjectUpsertDto } from "src/app/shared/generated/model/project-upsert-dto";
import { ProjectWorkflowService } from "src/app/services/project-workflow.service";
import { ProjectService } from "src/app/shared/generated/api/project.service";
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

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private cdr: ChangeDetectorRef,
        private authenticationService: AuthenticationService,
        private projectService: ProjectService,
        private projectWorkflowService: ProjectWorkflowService
    ) {}

    ngOnInit(): void {}
}
