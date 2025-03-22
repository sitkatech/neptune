import { Component, ComponentRef } from "@angular/core";
import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import { ModalService } from "src/app/shared/services/modal/modal.service";
@Component({
    selector: "score-descriptions",
    templateUrl: "./score-descriptions.component.html",
    styleUrls: ["./score-descriptions.component.scss"],
    standalone: true,
    imports: [],
})
export class ScoreDescriptionsComponent {
    modalComponentRef: ComponentRef<ModalComponent>;

    constructor(private modalService: ModalService) {}

    ngOnInit(): void {}

    close() {
        this.modalService.close(this.modalComponentRef, null);
    }
}
