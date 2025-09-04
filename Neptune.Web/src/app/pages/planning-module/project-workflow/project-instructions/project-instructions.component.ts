import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { CustomRichTextComponent } from "src/app/shared/components/custom-rich-text/custom-rich-text.component";
import { WorkflowBodyComponent } from "src/app/shared/components/workflow-body/workflow-body.component";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";

@Component({
    selector: "project-instructions",
    templateUrl: "./project-instructions.component.html",
    styleUrls: ["./project-instructions.component.scss"],
    imports: [CustomRichTextComponent, WorkflowBodyComponent, PageHeaderComponent, AlertDisplayComponent]
})
export class ProjectInstructionsComponent implements OnInit {
    public customRichTextTypeID: number = NeptunePageTypeEnum.HippocampProjectInstructions;
    public projectID: number;

    constructor(private route: ActivatedRoute, private router: Router) {}

    ngOnInit(): void {
        const projectID = this.route.snapshot.paramMap.get("projectID");
        if (projectID) {
            this.projectID = parseInt(projectID);
        }
    }

    continueToNextStep() {
        if (this.projectID) {
            this.router.navigateByUrl(`/planning/projects/edit/${this.projectID}/project-basics`);
        } else {
            this.router.navigateByUrl(`/planning/projects/new/project-basics`);
        }
    }
}
