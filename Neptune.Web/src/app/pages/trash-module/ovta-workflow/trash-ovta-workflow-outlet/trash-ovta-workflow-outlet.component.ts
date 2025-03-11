import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { TrashOvtaWizardSidebarComponent } from "../trash-ovta-wizard-sidebar/trash-ovta-wizard-sidebar.component";

@Component({
    selector: "trash-ovta-workflow-outlet",
    standalone: true,
    imports: [RouterOutlet, TrashOvtaWizardSidebarComponent],
    templateUrl: "./trash-ovta-workflow-outlet.component.html",
    styleUrl: "./trash-ovta-workflow-outlet.component.scss",
})
export class TrashOvtaWorkflowOutletComponent {}
