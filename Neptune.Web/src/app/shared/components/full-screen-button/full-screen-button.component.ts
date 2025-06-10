import { Component, HostListener, Input } from "@angular/core";

@Component({
    selector: "full-screen-button",
    standalone: true,
    imports: [],
    templateUrl: "./full-screen-button.component.html",
    styleUrl: "./full-screen-button.component.scss",
})
export class FullScreenButtonComponent {
    @Input() elementRef: HTMLElement;

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
