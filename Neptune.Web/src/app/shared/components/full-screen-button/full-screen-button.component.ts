import { Component, HostListener, Input } from "@angular/core";
import { IconComponent } from "../icon/icon.component";

@Component({
    selector: "full-screen-button",
    standalone: true,
    imports: [IconComponent],
    templateUrl: "./full-screen-button.component.html",
    styleUrl: "./full-screen-button.component.scss",
})
export class FullScreenButtonComponent {
    @Input() elementRef: HTMLElement;
    @Input() titleText: string = "Make element full screen";

    public triggerFullscreen() {
        if (this.elementRef.requestFullscreen) {
            this.elementRef.requestFullscreen();
            this.elementRef.classList.add("fullscreen");
        }
    }

    //Detect fullscreen mode changes
    @HostListener("document:fullscreenchange")
    handleFullscreen() {
        if (!document.fullscreenElement) {
            this.elementRef.classList.remove("fullscreen");
        }
    }
}
