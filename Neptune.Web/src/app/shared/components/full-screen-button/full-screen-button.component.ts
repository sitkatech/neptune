import { Component, EventEmitter, HostListener, Input, Output } from "@angular/core";
import { IconComponent } from "../icon/icon.component";

@Component({
    selector: "full-screen-button",
    imports: [IconComponent],
    templateUrl: "./full-screen-button.component.html",
    styleUrl: "./full-screen-button.component.scss"
})
export class FullScreenButtonComponent {
    @Input() elementRef: HTMLElement;
    @Input() enterFullScreenTitleText: string = "Make element full screen";
    @Output() screenSizeChangedEvent = new EventEmitter();
    exitFullScreenTitleText: string = "Exit full screen";
    isFullScreen: boolean = false;

    //Detect fullscreen mode changes
    //Because we can exit via button click or 'esc' press, listen to the event to handle exiting
    @HostListener("document:fullscreenchange")
    handleFullscreenChange() {
        if (!document.fullscreenElement) {
            this.elementRef.classList.remove("full-screen");
            this.isFullScreen = false;
            this.screenSizeChangedEvent.emit();
        }
    }

    public enterFullScreen() {
        if (this.elementRef.requestFullscreen) {
            this.elementRef.requestFullscreen().then(() => {
                this.elementRef.classList.add("full-screen");
                this.isFullScreen = true;
                this.screenSizeChangedEvent.emit();
            });
        }
    }

    public exitFullScreen() {
        document.exitFullscreen();
    }
}
