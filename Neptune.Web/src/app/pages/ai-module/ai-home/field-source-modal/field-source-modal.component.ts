import { Component, ComponentRef } from "@angular/core";

import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import { ModalService } from "src/app/shared/services/modal/modal.service";

@Component({
    selector: "ai-field-source-modal",
    templateUrl: "./field-source-modal.component.html",
    styleUrls: ["./field-source-modal.component.scss"],
    standalone: true,
    imports: [],
})
export class FieldSourceModalComponent {
    modalComponentRef: ComponentRef<ModalComponent>;
    modalContext: FieldSourceModalContext;

    constructor(private modalService: ModalService) {}

    close() {
        this.modalService.close(this.modalComponentRef, null);
    }
}

export class FieldSourceModalContext {
    FieldLabel: string;
    Field: any;
}
