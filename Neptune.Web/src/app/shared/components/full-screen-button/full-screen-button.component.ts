import { Component, EventEmitter, HostListener, Input, Output } from "@angular/core";
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
    @Input() enterFullScreenTitleText: string = "Make element full screen";
    @Output() screenSizeChangedEvent = new EventEmitter();
    exitFullScreenTitleText: string = "Exit full screen";
    isFullScreen: boolean = false;

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
        document.exitFullscreen().then(() => {
            this.elementRef.classList.remove("full-screen");
            this.isFullScreen = false;
            this.screenSizeChangedEvent.emit();
        });
    }
}
